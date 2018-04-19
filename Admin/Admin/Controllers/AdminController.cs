using System;
using Admin;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;
using Admin.Models.db;
using Admin.Data;
using Admin.Models.ViewModels;

namespace Admin.Controllers
{
    public class AdminController : BaseController
    {
        private DbOperations dbo = new DbOperations();
        private tusjoseEntities db = new tusjoseEntities();

        // GET: Admin
        public ActionResult ListUsers()
        {
            //if (Convert.ToInt32(Session["Access_Id"]) == 2 || Session["Access_Id"] == null)
            //{
            //   Danger("Fel 403: Åtkomst nekad/förbjuden.", true);
            //    return RedirectToAction("Index", "Home");
            //}
            var model = dbo.ListUsers();

            return View(model);
        }
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            //if (Convert.ToInt32(Session["Access_Id"]) == 2 || Session["Access_Id"] == null)
            //{
            //    Danger("Fel 403: Åtkomst nekad/förbjuden.", true);
            //    return RedirectToAction("Index", "Home");
            //}

            User user = db.User.Find(id);
            if (user != null)
            {
                dbo.DeleteUser(id);

                Success(string.Format("<b>{0} {1}</b> har tagits bort.", user.Firstname, user.Lastname), true);
            }

            return RedirectToAction("ListUsers");
        }

        public ActionResult DeleteUser(int? id)
        {
            //if (Convert.ToInt32(Session["Access_Id"]) == 2 || Session["Access_Id"] == null)
            //{
            //    Danger("Fel 403: Åtkomst nekad/förbjuden.", true);
            //    return RedirectToAction("ListUsers", "Admin");
            //}

            if (id != null)
            {
                return PartialView(dbo.GetUser(id));
            }
            Danger("Något gick fel.", true);
            return RedirectToAction("ListUsers");
        }

        public ActionResult EditUser(int? id)
        {
            //if (Convert.ToInt32(Session["Access_Id"]) == 2 || Session["Access_Id"] == null)
            //{
            //    Danger("Fel 403: Åtkomst nekad/förbjuden.", true);
            //    return RedirectToAction("Index", "Home");
            //}

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.AccessId = new SelectList(db.Access, "Id", "Name", id);
            return View(dbo.GetUser(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(UserVM user)
        {
            //if (Convert.ToInt32(Session["Access_Id"]) == 2 || Session["Access_Id"] == null)
            //{
            //    Danger("Fel 403: Åtkomst nekad/förbjuden.", true);
            //    return RedirectToAction("Index", "Home");
            //}

           User u = new User
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                Access_Id = user.Access_Id,
                Address_Id = dbo.SetAddress(user)
            };

            if (ModelState.IsValid)
            {
                db.Entry(u).State = EntityState.Modified;
                db.SaveChanges();
                Success(string.Format("<b>{0} {1}</b> har uppdaterats.", user.Firstname, user.Lastname), true);
                return RedirectToAction("ListUsers");
            }
            ViewBag.AccessId = new SelectList(db.Access, "Id", "Name", u.Access_Id);
            return View(user);
        }
    }
}