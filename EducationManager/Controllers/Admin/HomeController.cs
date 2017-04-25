using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationManager.Security;
using EducationManager.ViewModels.Admin;

namespace EducationManager.Controllers.Admin
{
    [CustomAuthorize(Roles = "admin")]
    public class AdminHomeController : Controller
    {
        Models.DataModel.DataStorage data_storage = new Models.DataModel.DataStorage();
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.About = UserSession.Uinform.Admin;
            ViewBag.School = data_storage.Schools.Where(s => s.SchoolId.Equals(UserSession.Uinform.Admin.SchoolId)).First();
            return View();
        }

        [HttpPost]
        public ActionResult Find(string req)
        {

            string[] req_values = req.Split(' ');
            if (req_values.Length < 3)
                return null;
            string firstname = req_values[1];
            string middlename = req_values[2];
            string lastname = req_values[0];

            FindResultViewModel personeResult = new FindResultViewModel();//Найденный пользователь
            List<FindResultViewModel> findResult = new List<FindResultViewModel>();//Список найденных пользователей

            var teachersList = data_storage.Teachers.Where(u => u.FirstName.Equals(firstname) &&
            u.MiddleName.Equals(middlename) &&
            u.LastName.Equals(lastname));

            var studentsList = data_storage.Students.Where(u => u.FirstName.Equals(firstname) &&
            u.MiddleName.Equals(middlename) &&
            u.LastName.Equals(lastname));

            foreach (var teacher in teachersList)
            {
                personeResult.id = teacher.TeacherId;
                personeResult.role = "Учитель";
                personeResult.fname = teacher.FirstName;
                personeResult.lname = teacher.LastName;
                personeResult.mname = teacher.MiddleName;
                personeResult.dateofbirth = teacher.DateOfBirth;
                findResult.Add(personeResult);
            }
            foreach (var student in studentsList)
            {
                personeResult.id = student.StudentId;
                personeResult.role = "Ученик";
                personeResult.fname = student.FirstName;
                personeResult.lname = student.LastName;
                personeResult.mname = student.MiddleName;
                personeResult.dateofbirth = student.DateOfBirth;
                findResult.Add(personeResult);
            }
            return View(findResult);
        }
    }
}
