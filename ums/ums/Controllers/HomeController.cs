using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ums.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AdminHome()
        {
            String uname = Request["uname"];
            userfunctions obj = new userfunctions();
            List<user> list = obj.GetAllUsers();
            if(uname != null)
                ViewData["uname"] = uname.ToUpper();
            return View(list);
        }
        [HttpGet]
        public ActionResult User()
        {
            return View();
        }

        public ActionResult Edit(int userid)
        {
            userfunctions obj = new userfunctions();
            user usr = obj.GetUserById(userid);
            return RedirectToAction("newuser", "mainscreen", usr);
        }
    }
}
