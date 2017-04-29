using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationManager.DataModels;
using EducationManager.ViewModels.Student;
using EducationManager.Security;

namespace EducationManager.Controllers
{
    //[CustomAuthorize(Roles = "student")]
    public class StudentGradesController : Controller
    {
        DataStorage data_storage = new DataStorage();
        // GET: Student
        public ActionResult Index()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SelectListItem item;
            List<Course> courses = new List<Course>();

            var class_courses = data_storage.ClassCourses.Where(c => c.ClassId.Equals(UserSession.Uinform.Student.ClassId));
            if (class_courses == null)
                return HttpNotFound();

            var school_courses = data_storage.Courses.Where(c => c.SchoolId.Equals(UserSession.Uinform.Student.SchoolId)).ToArray();

            //Определяем набор курсов для ученика
            foreach (var cc in class_courses)
            {
                foreach (var sc in school_courses)
                {
                    if (cc.CourseId.Equals(sc.CourseId))
                    {
                        courses.Add(new Course()
                        {
                            CourseId = sc.CourseId,
                            CourseName = sc.CourseName,
                            SchoolId = sc.SchoolId,
                            SubjectId = sc.SubjectId,
                            TeacherId = sc.TeacherId
                        });
                    }
                }
            }

            //формируем SelectedList
            foreach (var c in courses)
            {
                item = new SelectListItem()
                {
                    Text = c.CourseName,
                    Value = c.CourseId.ToString(),
                };
                list.Add(item);
            }
            //Добавить значение по умолчанию
            list.Add(new SelectListItem()
            {
                Text = "Пусто",
                Value = ""
            });
            ViewBag.CourseList = list;
            return View();
        }


        public ActionResult Find(StudentGradesViewModel m)
        {
            if (m.From == null || m.To == null)
                return Content("Некорректные значения даты");


            List<StudentGeneralViewModel> gmodel = new List<StudentGeneralViewModel>();
            List<Grade> grades = new List<Grade>();
            List<Course> courses = new List<Course>();

            if (m.CourseId == null)
            {
                /*Определяем набор курсов для ученика, путем сравнения
                 *курсов из таблиц ClassCourses и Courses для получения
                 *доп информации о курсе, т.к в первой тб. имеется только его id*/
                var class_courses = data_storage.ClassCourses.Where(c => c.ClassId.Equals(UserSession.Uinform.Student.ClassId));
                if (class_courses == null)
                    return HttpNotFound();
                var school_courses = data_storage.Courses.Where(c => c.SchoolId.Equals(UserSession.Uinform.Student.SchoolId)).ToArray();

                //Долгая и некрасивая операция
                foreach (var cc in class_courses)
                {
                    foreach (var sc in school_courses)
                    {
                        if (cc.CourseId.Equals(sc.CourseId))
                        {
                            courses.Add(new Course()
                            {
                                CourseId = sc.CourseId,
                                CourseName = sc.CourseName,
                                SchoolId = sc.SchoolId,
                                SubjectId = sc.SubjectId,
                                TeacherId = sc.TeacherId
                            });
                        }
                    }
                }
            }
            else
            {
                int id = Convert.ToInt32(m.CourseId);
                courses = data_storage.Courses.Where(c => c.CourseId.Equals(id)).ToList();
            }

            foreach (var course in courses)
            {
                gmodel.Add(new StudentGeneralViewModel()
                {
                    CourseName = course.CourseName,
                    Grades = data_storage.Grades.Where(c => c.StudentId.Equals(UserSession.Uinform.Student.StudentId) && c.CourseId.Equals(course.CourseId) &&
                    c.Date >= m.From && c.Date <= m.To).ToList()
                });
            }
            ViewBag.from = m.From;
            ViewBag.to = m.To;
            return View(gmodel);
        }
    }
}
