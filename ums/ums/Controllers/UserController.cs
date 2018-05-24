using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace ums.Controllers
{
    public class UserController : Controller
    {
        // POST: LOGIN
        [HttpPost]
        public ActionResult Login()
        {
            if (Request.Form["btnLogin"] != null)
            {
                String login = Request["login"];
                String passwd = Request["password"];
                userfunctions obj = new userfunctions();
                user usr = obj.validateUser(login, passwd);
                if (usr != null)
                {
                    Session["uid"] = usr.userid;
                    Session["uname"] = usr.login;
                    Session["image"] = usr.imageName;
                    String url = "~/home/user";
                    return Redirect(url);
                }
                TempData["Login"] = login;
                TempData["Message"] = "Invalid username or password";
                return Redirect("~/mainscreen/existinguser");
            }
            else
            {
                String email = Request["email"];
                userfunctions obj = new userfunctions();
                bool isValid = obj.validateEmail(email);
                if (isValid)
                {
                    try
                    {

                        MailMessage mail = new MailMessage();

                        MailAddress to = new MailAddress(email);
                        mail.To.Add(to);

                        MailAddress from = new MailAddress("ead.csf15@gmail.com", "Shaheryar");
                        mail.From = from;

                        mail.Subject = "Reset Code";
                        string resetCode = Guid.NewGuid().ToString();
                        Session["ResetCode"] = resetCode;
                        Session["Email"] = email;
                        mail.Body = "Reset Code: " + resetCode;

                        var sc = new SmtpClient("smtp.gmail.com", 587)
                        {
                            Credentials = new System.Net.NetworkCredential("ead.csf15", "EAD_csf15m"),
                            EnableSsl = true
                        };

                        sc.Send(mail);
                    }
                    catch (Exception ex)
                    {
                    }
                    return Redirect("~/mainscreen/resetpasswd");
                }
                else
                {
                    TempData["Message"] = "Email not found!";
                    return Redirect("~/mainscreen/existinguser");
                }
            }
        }
        // POST: ADD NEW USER
        [HttpPost]
        public ActionResult AddUser(user usr)
        {
            Validator v1 = new Validator();
            bool isValid = v1.validateData(usr);
            if(!isValid)
            {
                ViewBag.Message = "All fields required in correct format!";
                return Redirect("~/mainscreen/newuser");
            }
            var uniqueName = "";
            if (Request.Files["image"] != null)
            { 
                var file = Request.Files["image"]; 
                if (file.FileName != "") 
                { 
                    var ext = System.IO.Path.GetExtension(file.FileName); 
                    uniqueName = Guid.NewGuid().ToString() + ext;
                    var rootPath = Server.MapPath("~/uploadedImages"); 
                    var fileSavePath = System.IO.Path.Combine(rootPath, uniqueName); 
                    file.SaveAs(fileSavePath);
                    usr.imageName = uniqueName;
                } 
            }
            String gen = Request["gender"];
            if (gen == "female")
                usr.gender = 'F';
            else if (gen == "male")
                usr.gender = 'M';
            else if (gen == "other")
                usr.gender = 'O';
            if (Request["cricket"] != null)
                usr.cricket = 1;
            if (Request["hockey"] != null)
                usr.hockey = 1;
            if (Request["chess"] != null)
                usr.chess = 1;
            if(Session["admin"] == null && Session["uname"] == null)
                usr.createdon = DateTime.Now;
            userfunctions obj = new userfunctions();
            if (usr.userid > 0) // Update Case
            {
                bool flag = obj.updateUser(usr);
                if (Session["admin"] != null)
                {
                    String uname = "admin";
                    return RedirectToAction("adminhome", "home", uname);
                }
                else
                {
                    String uname = "user";
                    return RedirectToAction("user", "home", uname);
                }
            }
            // Validate Data
            if(obj.validateUserName(usr.login))
            {
                ViewBag.Message = "Username already exist!";
            }
            else if (obj.validateEmail(usr.email))
            {
                ViewBag.Message = "Email already exist";
            }
            else
            {
                //Inserting data
                int result = obj.addUser(usr);
                if (result == 1)
                {
                    Session["uid"] = usr.userid;
                    Session["uname"] = usr.login;
                    Session["image"] = usr.imageName;
                    String url = "~/home/user";
                    return Redirect(url);
                }
            }
            return Redirect("~/mainscreen/newuser");
        }

    }


}
