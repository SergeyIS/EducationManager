using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationManager.ViewModels.Admin;
using EducationManager.DataModels;
using EducationManager.Security;

namespace EducationManager.Controllers.Admin
{
    [CustomAuthorize(Roles = "admin")]
    public class AdminBidsController : Controller
    {
        DataStorage data_storage = new DataStorage();

        public ActionResult Index()
        {
            List<BidViewModel> bids = new List<BidViewModel>();
            foreach (var user in data_storage.TemporaryUsers.Where(b => b.SchoolId.Equals(UserSession.Uinform.Admin.SchoolId)))
            {
                bids.Add(new BidViewModel()
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    MiddleName = user.MiddleName,
                    Role = user.Role,
                    Gender = user.Gender,
                    DateOfBirth = user.DateOfBirth,
                    Addres = user.Addres,
                    ClassId = user.ClassId,
                    SchoolId = user.SchoolId,
                    IsConfirmed = false
                });
            }
            return View(bids);
        }

        [HttpPost]
        public ActionResult Index(List<BidViewModel> bids)
        {
            //Запустить обработчик формы
            bids = HandleForm();
            //Добавить подтвержденных пользователей в базу
            foreach (var bid in bids)
            {
                if (bid.IsConfirmed)
                {
                    data_storage.Addresses.Add(new Address()
                    {
                        AddresValue = bid.Addres
                    });
                    data_storage.SaveChanges();
                    int addresId = data_storage.Addresses.Where(a => a.AddresValue.Equals(bid.Addres)).First().AddresId;
                    if (bid.Role == "teacher")
                    {
                        data_storage.Teachers.Add(new Teacher()
                        {
                            FirstName = bid.FirstName,
                            LastName = bid.LastName,
                            MiddleName = bid.MiddleName,
                            Gender = bid.Gender,
                            SchoolId = bid.SchoolId,
                            UserId = bid.UserId,
                            AddresId = addresId,
                            DateOfBirth = bid.DateOfBirth
                        });
                        data_storage.SaveChanges();
                    }
                    else if (bid.Role == "student")
                    {
                        data_storage.Students.Add(new Student()
                        {
                            FirstName = bid.FirstName,
                            LastName = bid.LastName,
                            MiddleName = bid.MiddleName,
                            Gender = bid.Gender,
                            SchoolId = bid.SchoolId,
                            AddresId = addresId,
                            ClassId = bid.ClassId,
                            DateOfBirth = bid.DateOfBirth,
                            UserId = bid.UserId
                        });
                        data_storage.SaveChanges();
                    }
                    //Удалить заявку из бд
                    data_storage.TemporaryUsers.Remove(
                        data_storage.TemporaryUsers.Where(u => u.UserId.Equals(bid.UserId)).First());
                    data_storage.SaveChanges();
                }
            }
            return Redirect("~/AdminBids/Index");
        }

        private List<BidViewModel> HandleForm()
        {
            List<BidViewModel> bids = new List<BidViewModel>();
            string[] UserIdValues = HttpContext.Request.Form.GetValues("UserId");
            string[] FirstNameValues = HttpContext.Request.Form.GetValues("FirstName");
            string[] LastNameValues = HttpContext.Request.Form.GetValues("LastName");
            string[] MiddleNameValues = HttpContext.Request.Form.GetValues("MiddleName");
            string[] DateOfBirthValues = HttpContext.Request.Form.GetValues("DateOfBirth");
            string[] GenderValues = HttpContext.Request.Form.GetValues("Gender");
            string[] RoleValues = HttpContext.Request.Form.GetValues("Role");
            string[] ClassIdValues = HttpContext.Request.Form.GetValues("ClassId");
            string[] SchoolIdValues = HttpContext.Request.Form.GetValues("SchoolId");
            string[] AddresValues = HttpContext.Request.Form.GetValues("Addres");
            string[] IsConfirmedValues = HttpContext.Request.Form.GetValues("IsConfirmed");

            for (int i = 0; i < UserIdValues.Length; i++)
            {
                bids.Add(new BidViewModel()
                {
                    UserId = Convert.ToInt32(UserIdValues[i]),
                    FirstName = FirstNameValues[i],
                    LastName = LastNameValues[i],
                    MiddleName = MiddleNameValues[i],
                    DateOfBirth = Convert.ToDateTime(DateOfBirthValues[i]),
                    Gender = GenderValues[i],
                    Role = RoleValues[i],
                    ClassId = Convert.ToInt32(ClassIdValues[i]),
                    SchoolId = Convert.ToInt32(SchoolIdValues[i]),
                    Addres = AddresValues[i],
                    IsConfirmed = Convert.ToBoolean(IsConfirmedValues[i])
                });
            }

            return bids;
        }
    }
}
