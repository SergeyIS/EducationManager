using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationManager.Security;
using EducationManager.ViewModels.Admin;
using EducationManager.ViewModels.Student;
using EducationManager.Models.DataModel;

namespace EducationManager.Controllers.Admin
{


    [CustomAuthorize(Roles = "admin")]
    public class AdminClassesController : Controller
    {
        DataStorage data_storage = new DataStorage();
        //Отображает все классы в данной школе
        public ActionResult Index()
        {
            List<ClassViewModel> classes = new List<ClassViewModel>();
            foreach (var item in data_storage.Classes.Where(c => c.SchoolId.Equals(UserSession.Uinform.Admin.SchoolId)))
            {
                classes.Add(new ClassViewModel()
                {
                    ClassId = item.ClassId,
                    ClassName = item.ClassName,
                    DateFrom = item.DateFrom,
                    DateTo = item.DateTo
                });
            }
            return View(classes);
        }
        //Добавляет новый класс
        public ActionResult AddClass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddClass(ClassViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            data_storage.Classes.Add(new Class()
            {
                ClassName = model.ClassName,
                DateFrom = model.DateFrom,
                DateTo = model.DateTo,
                SchoolId = UserSession.Uinform.Admin.SchoolId
            });
            data_storage.SaveChanges();
            return Redirect("~/AdminClasses/Index");
        }
        //Редактирует класс
        public ActionResult EditClass(int id)
        {
            if (!(data_storage.Classes.Any(c => c.ClassId.Equals(id) && c.SchoolId.Equals(UserSession.Uinform.Admin.SchoolId))))
                return Redirect("/Account/ErrorAccess?str=такого%20класс%20не%20существует,%20либо%20вы%20не%20имеете%20к%20ниму%20доступа.");

            Class _class = data_storage.Classes.Where(c => c.ClassId.Equals(id)).First();
            List<StudentViewModel> students = new List<StudentViewModel>();
            //заполнить студентов
            foreach (var item in data_storage.Students.Where(s => s.ClassId.Equals(_class.ClassId)))
            {
                students.Add(new StudentViewModel()
                {
                    StudentId = item.StudentId,
                    AddresId = item.AddresId,
                    DateOfBirth = item.DateOfBirth,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    MiddleName = item.MiddleName,
                    Gender = item.Gender
                });
            }
            //составить editor
            ClassEditorViewModel editor = new ClassEditorViewModel()
            {

                AboutClass = new ClassViewModel()
                {
                    ClassId = _class.ClassId,
                    ClassName = _class.ClassName,
                    DateFrom = _class.DateFrom,
                    DateTo = _class.DateTo
                },
                Students = students
            };
            ViewBag.StudentClassId = id;
            ViewBag.ClassCourses = data_storage.ClassCourses.Where(c => c.ClassId.Equals(id));
            return View(editor);
        }

        //Добавляет студента к классу
        public ActionResult AddStudentToClass(int id)
        {
            if (!(data_storage.Classes.Any(c => c.ClassId.Equals(id) && c.SchoolId.Equals(UserSession.Uinform.Admin.AdminId))))
                return Redirect("/Account/ErrorAccess/такого%20класс%20не%20существует,%20либо%20вы%20не%20имеете%20к%20ниму%20доступа.");

            return View();
        }
        [HttpPost]
        public ActionResult AddStudentToClass(int id, StudentViewModel svm)
        {
            var val = data_storage.Students.Where(s => s.FirstName.Equals(svm.FirstName) &&
            s.MiddleName.Equals(svm.MiddleName) &&
            s.LastName.Equals(svm.LastName) && s.SchoolId.Equals(UserSession.Uinform.Admin.SchoolId)).ToList();

            if (val.Count == 0)
                return Redirect($"~/AdminClasses/EditClass?id={id}");

            data_storage.Students.Remove(val.First());

            Student student = new Student()
            {
                AddresId = val.First().AddresId,
                ClassId = id,
                DateOfBirth = val.First().DateOfBirth,
                FirstName = val.First().FirstName,
                Gender = val.First().Gender,
                LastName = val.First().LastName,
                MiddleName = val.First().MiddleName,
                SchoolId = val.First().SchoolId,
                UserId = val.First().UserId
            };
            data_storage.Students.Add(student);
            data_storage.SaveChanges();
            return Redirect($"~/AdminClasses/EditClass?id={id}");
        }

        public ActionResult AddCourseToClass(int class_id)
        {
            ClassCourseViewModel model = new ClassCourseViewModel();
            model.ClassId = class_id;

            return View(model); ;
        }
        [HttpPost]
        public ActionResult AddCourseToClass(ClassCourseViewModel model)
        {
            var course = data_storage.Courses.Where(c => c.CourseId.Equals(model.CourseId));
            if (course.Count() == 0)
                return View(model);
            string _coursename = course.First().CourseName;
            data_storage.ClassCourses.Add(new ClassCourses()
            {
                ClassId = model.ClassId,
                CourseId = model.ClassId,
                CourseName = _coursename
            });
            data_storage.SaveChangesAsync();
            return RedirectToAction($"EditClass/{model.ClassId}");
        }
        public ActionResult DeleteCourseFromClass(int class_id)
        {
            ClassCourseViewModel model = new ClassCourseViewModel();
            model.ClassId = class_id;
            return View(model);
        }
        [HttpPost]
        public ActionResult DeleteCourseFromClass(ClassCourseViewModel model)
        {
            ClassCourses cc = data_storage.ClassCourses.Where(c => c.ClassId.Equals(model.ClassId) && c.CourseId.Equals(model.CourseId)).First();
            data_storage.ClassCourses.Remove(cc);
            data_storage.SaveChangesAsync();
            return RedirectToAction($"EditClass/{model.ClassId}");
        }
    }
}
