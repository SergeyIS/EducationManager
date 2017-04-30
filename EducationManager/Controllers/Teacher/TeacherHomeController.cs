using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationManager.DataModels;
using EducationManager.Security;


namespace EducationManager.Controllers.Teachers
{
    //[CustomAuthorize(Roles = "teacher")]
    public class TeacherHomeController : Controller
    {
        DataStorage data_storage = new DataStorage();
        // GET: Teacher
        public ActionResult Index()
        {
            //ViewBag.About = UserSession.Uinform.Teacher;
            return View();
        }
    }
}
