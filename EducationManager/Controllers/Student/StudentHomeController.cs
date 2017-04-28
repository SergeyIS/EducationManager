using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationManager.DataModels;
using EducationManager.Security;

namespace EducationManager.Controllers
{
    [CustomAuthorize(Roles = "student")]
    public class StudentHomeController : Controller
    {
        DataStorage data_storage = new DataStorage();
        // GET: Student
        public ActionResult Index()
        {
            var student = data_storage.Students.Where(s => s.StudentId.Equals(UserSession.Uinform.Student.StudentId)).First();
            ViewBag._fullname = student.FirstName + " " + student.MiddleName;
            ViewBag._school = data_storage.Schools.Where(s => s.SchoolId.Equals(student.SchoolId)).First().SchoolName;
            return View();
        }
    }
}
