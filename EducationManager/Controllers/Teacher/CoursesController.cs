using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationManager.Security;
using EducationManager.Models.DataModel;

namespace EducationManager.Controllers.Teachers
{
    [CustomAuthorize(Roles = "teacher")]
    public class TeacherCoursesController : Controller
    {
        DataStorage data_storage = new DataStorage();

        public ActionResult Index()
        {
            List<Course> courses = new List<Course>();
            //запрос к базе данных.
            var _courses = data_storage.Courses.Where(c => c.TeacherId.Equals(UserSession.Uinform.Teacher.TeacherId));
            //если в базе нет курсов для данного учителя
            if (_courses == null)
            {
                ViewBag.Status = "У вас пока нет курсов";
                return View();
            }
            courses = _courses.ToList();
            return View(courses);
        }

        public ActionResult LookAt(int id)
        {
            //Существует ли этот курс, классы, прикрепленные к нему, и принадлежит ли курс учебновму заведению
            if (!(data_storage.Courses.Any(c => c.CourseId.Equals(id) &&
             c.SchoolId.Equals(UserSession.Uinform.Teacher.SchoolId))) &&
                data_storage.ClassCourses.Any(cc => cc.CourseId.Equals(id)))
            {
                return new HttpNotFoundResult("Такого курса еще несуществует");
            }

            var class_courses = data_storage.ClassCourses.Where(cc => cc.CourseId.Equals(id)).ToList();

            List<Class> classes = new List<Class>();
            Class _class = new Class();
            foreach (var cc in class_courses)
            {
                _class = data_storage.Classes.Where(c => c.ClassId.Equals(cc.ClassId)).First();
                classes.Add(_class);
            }
            ViewBag.CourseID = id;
            return View(classes);
        }
    }
}
