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
        public ActionResult ListAuthorities()
        {
            //if (Convert.ToInt32(Session["Access_Id"]) == 2 || Session["Access_Id"] == null)
            //{
            //   Danger("Fel 403: Åtkomst nekad/förbjuden.", true);
            //    return RedirectToAction("Index", "Home");
            //}
            var model = dbo.ListAuthorities();

            return View(model);
        }

        public ActionResult Show(int id)
        {
            var imagedata = db.Authority.Where(x => x.Id == id).Single();

            if (imagedata.Logo != null)
            {
                return File(imagedata.Logo, "image/jpg");
            }

            return RedirectToAction("ListAuthorities");
        }

        [HttpPost, ActionName("DeleteAuthority")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            //if (Convert.ToInt32(Session["Access_Id"]) == 2 || Session["Access_Id"] == null)
            //{
            //    Danger("Fel 403: Åtkomst nekad/förbjuden.", true);
            //    return RedirectToAction("Index", "Home");
            //}

            Authority authority = db.Authority.Find(id);
            if (authority != null)
            {
                dbo.DeleteAuthority(id);

                Success(string.Format("<b>{0}</b> har tagits bort.", authority.Name), true);
            }

            return RedirectToAction("ListAuthorities");
        }

        public ActionResult DeleteAuthority(int? id)
        {
            //if (Convert.ToInt32(Session["Access_Id"]) == 2 || Session["Access_Id"] == null)
            //{
            //    Danger("Fel 403: Åtkomst nekad/förbjuden.", true);
            //    return RedirectToAction("ListUsers", "Admin");
            //}

            if (id != null)
            {
                return PartialView(dbo.GetAuthority(id));
            }
            Danger("Något gick fel.", true);
            return RedirectToAction("ListAuthorities");
        }

        public ActionResult EditAuthority(int? id)
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

            ViewBag.Category = new SelectList(db.Category, "Id", "Name", id);
            return View(dbo.GetAuthority(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAuthority(AuthorityVM authority, HttpPostedFileBase image1)
        {
            //if (Convert.ToInt32(Session["Access_Id"]) == 2 || Session["Access_Id"] == null)
            //{
            //    Danger("Fel 403: Åtkomst nekad/förbjuden.", true);
            //    return RedirectToAction("Index", "Home");
            //}

            if (ModelState.IsValid)
            {
                dbo.EditAuthority(authority, image1);

                Success(string.Format("<b>{0}</b> har uppdaterats.", authority.Name), true);
                return RedirectToAction("ListAuthorities");
            }

            ViewBag.Category = new SelectList(db.Category, "Id", "Name", authority.Category_Id);

            return View(authority);
        }
    }
}