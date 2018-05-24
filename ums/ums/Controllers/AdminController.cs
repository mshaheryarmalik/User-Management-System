using DAL;
using DAL;
using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ums.Controllers
{
    public class AdminController : Controller
    {
        
        [HttpPost]
        public ActionResult Login()
        {
            String login = Request["login"];
            String pass = Request["password"];
            adminFunctions obj = new adminFunctions();
            Admin rev = obj.validateAdmin(login, pass);
            if (rev != null)
            {
                Session["admin"] = login;
                String url = "~/home/adminhome";
                return Redirect(url + "?uname=" + HttpUtility.UrlEncode(login));
            }
            TempData["login"] = login;
            TempData["Message"] = "Invalid username or password";
            return Redirect("~/mainscreen/admin");
        }

    }
}
