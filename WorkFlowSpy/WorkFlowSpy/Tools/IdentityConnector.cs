using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WorkFlowSpy.Controllers;
using WorkFlowSpy.Models;
using Microsoft.Owin.Security;

namespace WorkFlowSpy.Tools
{
    public class IdentityConnector
    {
        public bool RegisterNewUser(RegisterViewModel model, HttpContext httpcontex)
        {
            bool res;

            using(UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
            {
                var user = new ApplicationUser() { UserName = model.UserName, FirstName = model.FirstName, LastName = model.LastName };
                var result = userManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    //httpcontex.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    //var identity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    //httpcontex.GetOwinContext().Authentication.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);
                    res = true;
                }
                else
                {
                    res = false;
                }
            }
            
            return res;
        }
    }
}