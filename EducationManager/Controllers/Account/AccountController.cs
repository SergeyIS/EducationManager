using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationManager.ViewModels.Account;
using EducationManager.DataModels;
using EducationManager.Security;
using System.Net.Mail;
using System.Net;

namespace EducationManager.Controllers.Account
{
    public class AccountController : Controller
    {
        DataStorage data_storage = new DataStorage();


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AccountViewModel avm)
        {
            try
            {
                if (string.IsNullOrEmpty(avm.Username) || string.IsNullOrEmpty(avm.Password) ||
                !data_storage.UserAccounts.Any(u => u.Username.Equals(avm.Username) && u.Password.Equals(avm.Password)))
                {
                    ViewBag.Error = "Неверные данные";
                    return View(avm);
                }
            }
            catch
            {
                ViewBag.Error = "Непредвиденная ошибка";
                return View(avm);
            }
            //Аутентифицируем пользователя
            UserSession.Uinform = new UserInformation();
            UserSession.Uinform.UserId = data_storage.UserAccounts.Where(
                u => u.Username.Equals(avm.Username) && u.Password.Equals(avm.Password)).FirstOrDefault().UserId;

            //получили роль пользователя
            string role = data_storage.UserAccounts.Where(u => u.UserId.Equals(UserSession.Uinform.UserId)).FirstOrDefault().Role;
            UserSession.Uinform.IsConfirmed = true;//Устанавливается значение по умолчанию
            //Проверка подтверждения пользователя
            if (role == "admin" && data_storage.Admins.Any(u => u.UserId.Equals(UserSession.Uinform.UserId)))
            {
                UserSession.Uinform.Admin = data_storage.Admins.Where(a => a.UserId.Equals(UserSession.Uinform.UserId)).First();
                return Redirect("~/AdminHome/Index");
            }
            else if (role == "teacher" && data_storage.Teachers.Any(u => u.UserId.Equals(UserSession.Uinform.UserId)))
            {
                UserSession.Uinform.Teacher = data_storage.Teachers.Where(t => t.UserId.Equals(UserSession.Uinform.UserId)).First();
                return Redirect("~/TeacherHome/Index");
            }
            else if (role == "student" && data_storage.Students.Any(u => u.UserId.Equals(UserSession.Uinform.UserId)))
            {
                UserSession.Uinform.Student = data_storage.Students.Where(u => u.UserId.Equals(UserSession.Uinform.UserId)).First();
                return Redirect("~/StudentHome/Index");
            }
            //Если мы оказались в этой части кода, значит пользователь не подтвержден
            UserSession.Uinform.IsConfirmed = false;
            return Redirect("~/Account/ErrorAccess?str=Ваш%20аккаунт%20не%20подтвержден");
        }

        public ActionResult LogOut()
        {
            UserSession.Uinform = null;
            return Redirect("Login");
        }
        /// <summary>
        /// Метод CheckUsername() проверяет
        /// параметр username на предмет вхождения в базу.
        /// Возвращает true, если содержит.
        /// </summary>
        /// /// <param name="username">Имя пользователя</param>
        public bool CheckUsername(string username)
        {
            if (username == "")
                return false;
            return !data_storage.UserAccounts.Any(a => a.Username.Equals(username));
        }

        public ActionResult Registry()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registry(RegistryViewModel rvm)
        {
            //Проверка модели и username на предмет вхождения логина в базу
            if (!ModelState.IsValid || !CheckUsername(rvm.Username))
            {
                return View(rvm);
            }

            data_storage.UserAccounts.Add(new UserAccount()
            {
                Username = rvm.Username,
                Password = rvm.Password,
                Role = rvm.Role,
                Email = rvm.Email
            });
            data_storage.SaveChanges();

            data_storage.TemporaryUsers.Add(new OperationRegistryUser()
            {
                Addres = rvm.Addres,
                ClassId = rvm.ClassId,
                DateOfBirth = rvm.DateOfBirth,
                FirstName = rvm.FirstName,
                MiddleName = rvm.MiddleName,
                LastName = rvm.LastName,
                Gender = rvm.Gender,
                Role = rvm.Role,
                SchoolId = rvm.SchoolId,
                UserId = data_storage.UserAccounts.Where(u => u.Username.Equals(rvm.Username) && u.Password.Equals(rvm.Password)).First().UserId
            });
            data_storage.SaveChanges();

            return Redirect("~/Account/Login");
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordViewNodel fp)
        {
            if (!ModelState.IsValid)
                return View(fp);

            if (!data_storage.UserAccounts.Any(a => a.Username.Equals(fp.Username) &&
             a.Email.Equals(fp.Email)))
            {
                ModelState.AddModelError("emailerror", "Проверте введенные данные");
                return View();
            }
            UserAccount account = data_storage.UserAccounts.Where(a => a.Username.Equals(fp.Username)).First();
            account.Password = fp.NewPassword;
            data_storage.Entry(account).State = System.Data.Entity.EntityState.Modified;
            data_storage.SaveChangesAsync();
            return Redirect("../");
        }

        public ActionResult ErrorAccess(string str = "")
        {
            ViewBag.ErrorMessage = str;
            return View();
        }

    }
}
