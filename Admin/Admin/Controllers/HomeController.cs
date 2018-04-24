using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Admin.Data;
using Admin.Models;
using Admin.Models.db;
using Admin.Models.ViewModels;


namespace Admin.Controllers
{
    public class HomeController : Controller
    {
        DbOperations dbOps = new DbOperations();

       

        //Metod för att logga in
    
        public ActionResult Index()
        {
            ViewBag.Message = "Newp";

            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginVM login)
        {
            if (ModelState.IsValid)
            {
                if (dbOps.UserCheck(login.Username, login.Password))
                {
                    var user = dbOps.GetUsernamePass(login.Username, login.Password);
                    login.Access = dbOps.GetAccess(user);

                    FormsAuthentication.SetAuthCookie(user.Username, false);

                    if (user != null)
                    {
                        SetCookie(login);
                        return RedirectToAction("ListAuthorities", "Admin");
                    }

                }
                else
                    ModelState.AddModelError("", "Var vänlig att kontrollera dina användaruppgifter");
            }
            return View();
        }
    

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
        public void SetCookie(LoginVM lvm)
        {
            User p = dbOps.GetUsernamePass(lvm.Username, lvm.Password);

            HttpCookie myCookie = new HttpCookie("UserInfo");
            myCookie["Id"] = p.Id.ToString();
            myCookie.Expires = DateTime.Now.AddDays(1d);
            Response.Cookies.Add(myCookie);
        }

      

     
    }
}