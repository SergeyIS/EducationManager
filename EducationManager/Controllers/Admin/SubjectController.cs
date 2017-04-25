using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationManager.Security;
using EducationManager.Models.DataModel;

namespace EducationManager.Controllers.Admin
{
    [CustomAuthorize(Roles = "admin")]
    public class AdminSubjectsController : Controller
    {
        DataStorage data_storage = new DataStorage();
        // GET: Subject
        public ActionResult Index()
        {
            ViewBag.Subjects = data_storage.Subjects.Where(s => s.SchoolId.Equals(UserSession.Uinform.Admin.SchoolId));
            return View();
        }

        public ActionResult Edit(int id)
        {
            if (!(data_storage.Subjects.Any(s => s.SubjectId.Equals(id) && s.SchoolId.Equals(UserSession.Uinform.Admin.SchoolId))))
                return Redirect("Account/ErrorAccess/Нет%20доступа");

            ViewBag.SubjectId = id;
            return View();
        }
        [HttpPost]
        public ActionResult Edit(int id, Subject svm)
        {
            if (svm.SubjectName == null || !data_storage.Subjects.Any(s => s.SubjectId.Equals(id) && s.SchoolId.Equals(UserSession.Uinform.Admin.SchoolId)))
                return View(svm);

            Subject bufsub = data_storage.Subjects.Where(s => s.SubjectId.Equals(id)).First();
            bufsub.SubjectName = svm.SubjectName;
            data_storage.Entry(bufsub).State = System.Data.Entity.EntityState.Modified;
            data_storage.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (!data_storage.Subjects.Any(s => s.SubjectId.Equals(id) && s.SchoolId.Equals(UserSession.Uinform.Admin.SchoolId)))
                return Redirect("~/Account/ErrorAccess");

            Subject bufsub = data_storage.Subjects.Where(s => s.SubjectId.Equals(id)).First();
            data_storage.Subjects.Remove(bufsub);
            data_storage.SaveChangesAsync();
            return Redirect("~/AdminSubjects/Index");
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Subject svm)
        {
            if (svm.SubjectName == null)
                return View(svm);

            data_storage.Subjects.Add(new Subject()
            {
                SchoolId = UserSession.Uinform.Admin.SchoolId,
                SubjectName = svm.SubjectName
            });
            data_storage.SaveChangesAsync();
            return Redirect("~/AdminSubjects/Index");
        }
    }
}
