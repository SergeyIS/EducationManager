using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationManager.Security;
using EducationManager.DataModels;

namespace EducationManager.Controllers.Admin
{
    [CustomAuthorize(Roles = "admin")]
    public class AdminCoursesController : Controller
    {
        DataStorage data_storage = new DataStorage();
        // GET: Courses
        public ActionResult Index()
        {
            ViewBag.Courses = data_storage.Courses.Where(c => c.SchoolId.Equals(UserSession.Uinform.Admin.SchoolId));
            return View();
        }

        public ActionResult Edit(int id)
        {
            if (!(data_storage.Courses.Any(c => c.CourseId.Equals(id) && c.SchoolId.Equals(UserSession.Uinform.Admin.SchoolId))))
                return Redirect("~/Account/ErrorAccess");
            var model = data_storage.Courses.Where(c => c.CourseId.Equals(id)).First();
            ViewBag.CourseID = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Course cvm)
        {
            if (cvm.CourseName == null || !data_storage.Courses.Any(c => c.CourseId.Equals(cvm.CourseId) &&
             c.SchoolId.Equals(UserSession.Uinform.Admin.SchoolId)))
            {
                return View(cvm);
            }
            Course course = data_storage.Courses.Where(c => c.CourseId.Equals(cvm.CourseId)).First();
            course.CourseName = cvm.CourseName;
            course.SubjectId = cvm.SubjectId;
            course.TeacherId = cvm.TeacherId;

            data_storage.Entry(course).State = System.Data.Entity.EntityState.Modified;
            data_storage.SaveChangesAsync();
            return Redirect("~/AdminCourses/Index");
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Course cvm)
        {
            if (cvm.CourseName == null || !(data_storage.Subjects.Any(s => s.SubjectId.Equals(cvm.SchoolId) &&
            s.SchoolId.Equals(UserSession.Uinform.Admin.SchoolId)) ||
             data_storage.Teachers.Any(t => t.TeacherId.Equals(cvm.TeacherId) && t.SchoolId.Equals(UserSession.Uinform.Admin.SchoolId))))
            {
                return View(cvm);
            }

            data_storage.Courses.Add(new Course()
            {
                CourseName = cvm.CourseName,
                SubjectId = cvm.SubjectId,
                TeacherId = cvm.TeacherId,
                SchoolId = UserSession.Uinform.Admin.SchoolId
            });
            data_storage.SaveChangesAsync();
            return Redirect("~/AdminCourses/Index");
        }
        public ActionResult Delete(int id)
        {
            if (!(data_storage.Courses.Any(c => c.CourseId.Equals(id) && c.SchoolId.Equals(UserSession.Uinform.Admin.SchoolId))))
                return Redirect("~/Account/ErrorAccess/ошибка удаления курса");

            Course course = data_storage.Courses.Where(c => c.CourseId.Equals(id)).First();
            data_storage.Courses.Remove(course);
            data_storage.SaveChangesAsync();
            return Redirect("~/AdminCourses/Index");
        }
    }
}
