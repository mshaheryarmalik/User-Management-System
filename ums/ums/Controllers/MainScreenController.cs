using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ums.Controllers
{
    public class MainScreenController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExistingUser()
        {
            ViewBag.Message = "Existing User";
            return View();
        }

        public ActionResult Admin()
        {
            ViewBag.Message = "Admin Login";
            return View();
        }
        public ActionResult NewUser(user usr)
        { 
            return View(usr);
        }
        
        public ActionResult ResetPasswd(String email)
        {
            return View();
        }

        [HttpPost]
        public ActionResult ValidateCode()
        {
            String sentCode = Session["ResetCode"].ToString();
            String userCode = Request["code"];
            if(sentCode == userCode)
            {
                return View();
            }
            TempData["Message"] = "Invalid Code";
            return RedirectToAction("resetpasswd");
        }
        [HttpPost]
        public ActionResult NewPasswd()
        {
            String passwd = Request["passwd"];
            String email = Session["Email"].ToString();
            userfunctions obj = new userfunctions();
            if (obj.updateUserPasswordByEmail(passwd,email))
                return RedirectToAction("existinguser");
            else
                return RedirectToAction("index");
        }
    }
}
