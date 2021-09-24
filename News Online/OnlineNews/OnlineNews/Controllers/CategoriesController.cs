using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieShop;

namespace OnlineNews.Controllers.backend
{
    public class CategoriesController : Controller
    {
        private OnlineNewsEntities db = new OnlineNewsEntities();

        // GET: Categories
        public ActionResult Index()
        {
            TempData["mainHead"] = "Zones";
            TempData["cardTitle"] = "Manage Zones";
            TempData["breadcrumb"] = "Manage-Zones";
            var ms_Zones = db.categories.Include(m => m.flag);
            return View(ms_Zones.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category  ms_categories = db.categories.Find(id);
            if (ms_categories == null)
            {
                return HttpNotFound();
            }
            return View(ms_categories);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            TempData["mainHead"] = "Zones";
            TempData["cardTitle"] = "Add Zones";
            TempData["breadcrumb"] = "Add-Zones";
            ViewBag.flag_id = new SelectList(db.flags.Where(f => f.flagtype == "Category"), "id", "flag");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,cname,ctype,flag_id,createdby,modiffiedby,createdDate,modifiedDate,meta_keys,meta_description")] category ms_categories)
        {
            if (ModelState.IsValid)
            {
                db.categories.Add(ms_categories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.flag_id = new SelectList(db.flags.Where(f => f.flagtype == "Category"), "id", "flag");
            return View(ms_categories);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category ms_categories = db.categories.Find(id);
            if (ms_categories == null)
            {
                return HttpNotFound();
            }
            TempData["mainHead"] = "Zones";
            TempData["cardTitle"] = "Edit Zones";
            TempData["breadcrumb"] = "Edit-Zones";
            ViewBag.flag_id = new SelectList(db.flags.Where(f => f.flagtype == "Category"), "id", "flag");
            return View(ms_categories);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,cname,ctype,createdby,modiffiedby,createdDate,modifiedDate")] category ms_categories)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(ms_categories).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.flag_id = new SelectList(db.flags.Where(f => f.flagtype == "Category"), "id", "flag");
                return View(ms_categories);
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
               //throw;
            }


            ViewBag.flag_id = new SelectList(db.flags.Where(f => f.flagtype == "Category"), "id", "flag");
            return View(ms_categories);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category ms_categories = db.categories.Find(id);
            if (ms_categories == null)
            {
                return HttpNotFound();
            }
            db.categories.Remove(ms_categories);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Categories/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    ms_categories ms_categories = db.ms_categories.Find(id);
        //    db.ms_categories.Remove(ms_categories);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
