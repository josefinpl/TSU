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
    public class HomeController : BaseController
    {
        DbOperations dbOps = new DbOperations();
        tusjoseEntities db = new tusjoseEntities();

        public ActionResult Index()
        {
            bool auth = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (auth)
            {
                string username = System.Web.HttpContext.Current.User.Identity.Name.ToString();
                var user = db.User.Where(x => x.Username == username).Single();
                dbOps.LoginCheck(user);

                Session["Id"] = user.Id.ToString();
                Session["Username"] = user.Username.ToString();
                Session["Firstname"] = user.Firstname.ToString();
                Session["Lastname"] = user.Lastname.ToString();
                Session["Access_id"] = Convert.ToInt32(user.Access_Id);
                Session["Access"] = user.Access.Name.ToString();

               
                return RedirectToAction("ListAuthorities", "Admin");
            }
            else
                return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(User user)
        {
            var usr = dbOps.LoginCheck(user);

            if (usr != null)
            {
                //if (user.RememberMe == true)
                //{
                //    CreateLoginCookie(user);
                //}
                Session["Id"] = usr.Id.ToString();
                Session["Username"] = usr.Username.ToString();
                Session["Firstname"] = usr.Firstname.ToString();
                Session["Lastname"] = usr.Lastname.ToString();
                Session["Access_id"] = Convert.ToInt32(usr.Access_Id);
                Session["Access"] = usr.Access.Name.ToString();


                if (Session["Id"] != null)
                {
                    if (Convert.ToInt32(Session["Access_Id"]) == 1)
                    {
                        return RedirectToAction("ListAuthorities", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                Danger("Användarnamn och lösenord matchar inte!");
            }

            return View();
        }

        public ActionResult Logout()
        {
            string userId = Session["Id"].ToString();
            Session.Abandon();
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

      

     
    }
}