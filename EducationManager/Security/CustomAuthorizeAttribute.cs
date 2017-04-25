using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EducationManager.DataModels;

namespace EducationManager.Security
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (UserSession.Uinform == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new { controller = "Account", action = "Login" }));
            }
            else if (UserSession.Uinform.IsConfirmed == false)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new { controller = "Account", action = "ErrorAccess", id = "?str = Ваш аккаунт не подтвержден" }));
            }
            else
            {
                DataStorage account_storage = new DataStorage();
                CustomPrincipal mp = new CustomPrincipal(account_storage.UserAccounts.Where(u => u.UserId.Equals(UserSession.Uinform.UserId)).FirstOrDefault());
                if (!mp.IsInRole(Roles))
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "Account", action = "ErrorAccess" }));
            }
        }
    }
}
