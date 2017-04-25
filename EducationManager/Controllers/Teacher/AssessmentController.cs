using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationManager.Security;
using EducationManager.Models.DataModel;
using EducationManager.ViewModels.Teacher;

namespace EducationManager.Controllers.Teachers
{
    [CustomAuthorize(Roles = "teacher")]
    public class TeacherAssessmentController : Controller
    {
        DataStorage data_storage = new DataStorage();

        public ActionResult Put(int class_id, int course_id)
        {
            /*
            Здесь должна быть проверка id
            */
            var students = data_storage.Students.Where(s => s.ClassId.Equals(class_id)).ToList();
            List<GradesViewModel> model = new List<GradesViewModel>();

            foreach (var item in students)
            {
                model.Add(new GradesViewModel()
                {
                    StudentId = item.StudentId,
                    FirstName = item.FirstName,
                    MiddleName = item.MiddleName,
                    GradeValue = 0
                });
            }
            ViewBag.ClassId = class_id;
            ViewBag.CourseId = course_id;
            return View(model);
        }
        [HttpPost]
        public ActionResult Put(int class_id, int course_id, List<GradesViewModel> model)
        {
            /*
            Здесь должна быть проверка id
            */
            var studenstid = HttpContext.Request.Form.GetValues("StudentId");
            var gradevalues = HttpContext.Request.Form.GetValues("GradeValue");
            for (int i = 0; i < studenstid.Length; i++)
            {
                data_storage.Grades.Add(new Grade()
                {
                    CourseId = course_id,
                    StudentId = Convert.ToInt32(studenstid[i]),
                    GradeValue = Convert.ToInt32(gradevalues[i]),
                    Date = DateTime.Now.Date
                });
            }
            data_storage.SaveChangesAsync();
            return Redirect($"~/TeacherCourses/LookAt/{course_id}");
        }

        public ActionResult Show(int class_id, int course_id)
        {
            ViewBag.class_id = class_id;
            ViewBag.course_id = course_id;
            return View();
        }
        [HttpPost]
        public ActionResult FindGradesRusalt(ShowGradeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Content("Ничего не найдено");
            }
            var _CourseGrades = data_storage.Grades.Where(cc => cc.CourseId.Equals(model.CourseId) &&
            cc.Date >= model.From &&
            cc.Date <= model.To).ToList();

            var _Students = data_storage.Students.Where(s => s.ClassId.Equals(model.ClassId)).ToList();

            List<TeacherGeneralViewModel> retmodel = new List<TeacherGeneralViewModel>();
            foreach (var s in _Students)
            {
                retmodel.Add(new TeacherGeneralViewModel()
                {
                    FullName = s.LastName + " " + s.FirstName,
                    Grades = _CourseGrades.Where(c => c.StudentId.Equals(s.StudentId)).ToList()
                });
            }

            return View(retmodel);
        }
    }
}
