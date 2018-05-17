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
        private TSUEntities db = new TSUEntities();

        // GET: Admin
        public ActionResult ListAuthorities()
        {
            if (Convert.ToInt32(Session["Access_Id"]) == 2 || Session["Access_Id"] == null)
            {
                Danger("Fel 403: Åtkomst nekad/förbjuden.", true);
                return RedirectToAction("Index", "Home");
            }
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

        public ActionResult AddAuthority()
        {
            if (Convert.ToInt32(Session["Access_Id"]) == 2 || Session["Access_Id"] == null)
            {
                Danger("Fel 403: Åtkomst nekad/förbjuden.", true);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Category = new SelectList(db.Category, "Id", "Name");
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("SetAuthority")]
        public ActionResult AddAuthority([Bind(Exclude = "Id")] AuthorityVM model)
        {
            if (Convert.ToInt32(Session["Access_Id"]) == 2 || Session["Access_Id"] == null)
            {
                Danger("Fel 403: Åtkomst nekad/förbjuden.", true);
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {

                var id = dbo.SetAuthority(model);

                return RedirectToAction("AddElements/" + id);

            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);

            Danger("Alla fält måste fyllas i!");
            return RedirectToAction("AddAuthority");


        }

        public ActionResult AddElements(int? id)
        {
            if (Convert.ToInt32(Session["Access_Id"]) == 2 || Session["Access_Id"] == null)
            {
                Danger("Fel 403: Åtkomst nekad/förbjuden.", true);
                return RedirectToAction("Index", "Home");
            }

            var model = dbo.GetAuthority(id);

            return View(model);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("SetElements")]
        public ActionResult AddElements(AuthorityVM model, HttpPostedFileBase image1)
        {
            if (Convert.ToInt32(Session["Access_Id"]) == 2 || Session["Access_Id"] == null)
            {
                Danger("Fel 403: Åtkomst nekad/förbjuden.", true);
                return RedirectToAction("Index", "Home");
            }

            dbo.SetLogo(model, image1);

            return RedirectToAction("ListAuthorities");


        }

        #region Redigera/Ta bort nummer
        [HttpPost, ActionName("AddNumber")]
        [ValidateAntiForgeryToken]
        public ActionResult Add(NumberVM number)
        {
            if (Convert.ToInt32(Session["Access_Id"]) == 2 || Session["Access_Id"] == null)
            {
                Danger("Fel 403: Åtkomst nekad/förbjuden.", true);
                return RedirectToAction("Index", "Home");
            }

            if (number != null)
            {
                dbo.SetNumber(number);

            }

            return RedirectToAction("AddElements/" + number.Authority_Id);
        }

        public ActionResult AddNumber(int? id)
        {
            if (Convert.ToInt32(Session["Access_Id"]) == 2 || Session["Access_Id"] == null)
            {
                Danger("Fel 403: Åtkomst nekad/förbjuden.", true);
                return RedirectToAction("Index", "Home");
            }

            if (id != null)
            {
                NumberVM number = new NumberVM
                {
                    Authority_Id = id
                };

                return PartialView(number);
            }

            Danger("Något gick fel.", true);
            return RedirectToAction("AddElements/" + id);

        }

        [HttpPost, ActionName("EditNumber")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NumberVM number)
        {
            if (Convert.ToInt32(Session["Access_Id"]) == 2 || Session["Access_Id"] == null)
            {
                Danger("Fel 403: Åtkomst nekad/förbjuden.", true);
                return RedirectToAction("Index", "Home");
            }

            if (number != null)
            {
                dbo.EditNumber(number);

            }
            return RedirectToAction("EditAuthority/" + number.Authority_Id);
        }

        public ActionResult EditNumber(int? id, int? authId, int? check)
        {
            if (Convert.ToInt32(Session["Access_Id"]) == 2 || Session["Access_Id"] == null)
            {
                Danger("Fel 403: Åtkomst nekad/förbjuden.", true);
                return RedirectToAction("Index", "Home");
            }

            if (id != null)
            {

                var number = dbo.GetNumber(id);

                return PartialView(number);

            }

            Danger("Något gick fel.", true);
            return RedirectToAction("EditAuthority/" + id);

        }
        public ActionResult DeleteNumber(int id, int authId)
        {
            if (Convert.ToInt32(Session["Access_Id"]) == 2 || Session["Access_Id"] == null)
            {
                Danger("Fel 403: Åtkomst nekad/förbjuden.", true);
                return RedirectToAction("Index", "Home");
            }

            Number number = db.Number.Find(id);
            if (number != null)
            {
                dbo.DeleteNumber(id);

            }

            return RedirectToAction("EditAuthority/" + authId);
        }
        #endregion

        #region Lägg till/Redigera/Ta bort öppettider
        [HttpPost, ActionName("AddHour")]
        [ValidateAntiForgeryToken]
        public ActionResult Add(HourVM hour)
        {
            if (Convert.ToInt32(Session["Access_Id"]) == 2 || Session["Access_Id"] == null)
            {
                Danger("Fel 403: Åtkomst nekad/förbjuden.", true);
                return RedirectToAction("Index", "Home");
            }

            if (hour != null)
            {
                dbo.SetHour(hour);

            }

            return RedirectToAction("AddElements/" + hour.Authority_Id);
        }

        public ActionResult AddHour(int? id)
        {
            if (Convert.ToInt32(Session["Access_Id"]) == 2 || Session["Access_Id"] == null)
            {
                Danger("Fel 403: Åtkomst nekad/förbjuden.", true);
                return RedirectToAction("Index", "Home");
            }

            if (id != null)
            {
                HourVM hour = new HourVM
                {
                    Authority_Id = id
                };

                return PartialView(hour);
            }

            Danger("Något gick fel.", true);
            return RedirectToAction("AddElements/" + id);

        }
        [HttpPost, ActionName("EditHour")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Hour hour)
        {
            if (Convert.ToInt32(Session["Access_Id"]) == 2 || Session["Access_Id"] == null)
            {
                Danger("Fel 403: Åtkomst nekad/förbjuden.", true);
                return RedirectToAction("Index", "Home");
            }

            if (hour != null)
            {
                dbo.EditHour(hour);

            }

            return RedirectToAction("EditAuthority/" + hour.Authority_Id);
        }

        public ActionResult EditHour(int? id, int? authId)
        {
            if (Convert.ToInt32(Session["Access_Id"]) == 2 || Session["Access_Id"] == null)
            {
                Danger("Fel 403: Åtkomst nekad/förbjuden.", true);
                return RedirectToAction("Index", "Home");
            }

            if (id != null)
            {
                var hour = dbo.GetHour(id);

                return PartialView(hour);
            }

            Danger("Något gick fel.", true);
            return RedirectToAction("EditAuthority/" + authId);

        }
        public ActionResult DeleteHour(int id, int authId)
        {
            if (Convert.ToInt32(Session["Access_Id"]) == 2 || Session["Access_Id"] == null)
            {
                Danger("Fel 403: Åtkomst nekad/förbjuden.", true);
                return RedirectToAction("Index", "Home");
            }

            Hour hour = db.Hour.Find(id);
            if (hour != null)
            {
                dbo.DeleteHour(id);

            }

            return RedirectToAction("EditAuthority/" + authId);
        }

        #endregion

        [HttpPost, ActionName("DeleteAuthority")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (Convert.ToInt32(Session["Access_Id"]) == 2 || Session["Access_Id"] == null)
            {
                Danger("Fel 403: Åtkomst nekad/förbjuden.", true);
                return RedirectToAction("Index", "Home");
            }

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
            if (Convert.ToInt32(Session["Access_Id"]) == 2 || Session["Access_Id"] == null)
            {
                Danger("Fel 403: Åtkomst nekad/förbjuden.", true);
                return RedirectToAction("ListUsers", "Admin");
            }

            if (id != null)
            {
                return PartialView(dbo.GetAuthority(id));
            }
            Danger("Något gick fel.", true);
            return RedirectToAction("ListAuthorities");
        }

        public ActionResult EditAuthority(int? id)
        {
            if (Convert.ToInt32(Session["Access_Id"]) == 2 || Session["Access_Id"] == null)
            {
                Danger("Fel 403: Åtkomst nekad/förbjuden.", true);
                return RedirectToAction("Index", "Home");
            }

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
            if (Convert.ToInt32(Session["Access_Id"]) == 2 || Session["Access_Id"] == null)
            {
                Danger("Fel 403: Åtkomst nekad/förbjuden.", true);
                return RedirectToAction("Index", "Home");
            }


            if (ModelState.IsValid)
            {
                bool size = dbo.EditAuthority(authority, image1);
                if (size)
                {
                    Success(string.Format("<b>{0}</b> har uppdaterats.", authority.Name), true);
                    return RedirectToAction("ListAuthorities");
                }
                else {
                    Danger("Fel 404-13: Bilden får inte vara större än 500 KB", true);
                    return RedirectToAction("EditAuthority/"+authority.Id);
                }
            }

            ViewBag.Category = new SelectList(db.Category, "Id", "Name", authority.Category_Id);

            return View(authority);
        }
    }
}