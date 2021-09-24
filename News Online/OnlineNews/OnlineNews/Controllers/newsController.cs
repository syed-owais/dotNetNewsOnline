using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineNews;
using OnlineNews.Controllers.backend;

namespace OnlineNews.Controllers
{
    public class newsController : Controller
    {
        private OnlineNewsEntities db = new OnlineNewsEntities();
        TextEncryption enctxt = new TextEncryption();
        // GET: news
        public ActionResult Index()
        {
            return RedirectToAction("BackIndex");
        }
        public ActionResult BackIndex()
        {
            TempData["mainHead"] = "News";
            TempData["cardTitle"] = "Manage News";
            TempData["breadcrumb"] = "Manage-News";
            return View("~/Views/news/backend/Index.cshtml");
        }

        // GET: news/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            news news = db.news.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: news/Create
        public ActionResult Create()
        {
            TempData["mainHead"] = "News";
            TempData["cardTitle"] = "Manage News";
            TempData["breadcrumb"] = "Manage-News";
            ViewBag.category_id = new SelectList(db.categories, "id", "cname");
            return View("~/Views/news/backend/Create.cshtml");
        }

        // POST: news/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "id,newsTitle,newsDescription,category_id,news_img,active,createdby,modifiedby,createdDate,modifiedDate")] news news)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.news.Add(news);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(news);
        //}
        public ActionResult Create(HttpPostedFileBase items_img, HttpPostedFileBase trailerUrl, HttpPostedFileBase video)
        {
            string filex = null;
            string imgname = null, imgfilex = null, encryptedName = null, postername = null, posterfilex = null, encryptedposterName = null, videofilename = null, encryptedvideofile = null, videofilex = null;
            try
            {


                if (items_img != null)
                {
                    imgname = System.IO.Path.GetFileNameWithoutExtension(items_img.FileName);
                    filex = System.IO.Path.GetExtension(items_img.FileName);
                    encryptedName = enctxt.TextEncryp(imgname);
                    string imgpath = Server.MapPath("~/Uploads/" + encryptedName);
                    items_img.SaveAs(imgpath + filex);
                }

                if (trailerUrl != null)
                {
                    postername = System.IO.Path.GetFileNameWithoutExtension(trailerUrl.FileName);
                    posterfilex = System.IO.Path.GetExtension(trailerUrl.FileName);
                    encryptedposterName = enctxt.TextEncryp(postername);
                    string posterpath = Server.MapPath("~/Uploads/" + encryptedposterName);
                    trailerUrl.SaveAs(posterpath + posterfilex);
                }

                if (video != null)
                {
                    videofilename = System.IO.Path.GetFileNameWithoutExtension(video.FileName);
                    videofilex = System.IO.Path.GetExtension(video.FileName);
                    if (video.ContentLength < 104857600)
                    {
                        var uploadFilesDir = System.Web.HttpContext.Current.Server.MapPath("~/Uploads/Videos");
                        if (!Directory.Exists(uploadFilesDir))
                        {
                            Directory.CreateDirectory(uploadFilesDir);
                        }

                        encryptedvideofile = enctxt.TextEncryp(videofilename);
                        string vpath = Path.Combine(uploadFilesDir, encryptedvideofile + videofilex);
                        video.SaveAs(vpath);
                    }
                    else
                    {
                        Console.WriteLine("File is too large");
                    }

                }

                news i = new news();
                i.newsTitle = Request.Form["item_name"];
                i.newsDescription = Request.Form["items_description"];
                i.category_id = Convert.ToInt32(Request.Form["category_id"]);
                string username = Session["backUname"].ToString();
                i.createdby = (username != null) ? username : "Admin";
                i.modifiedby = (username != null) ? username : "Admin";
                i.createdDate = DateTime.Now;
                i.modifiedDate = DateTime.Now;
                i.news_img = encryptedName + filex;
                db.news.Add(i);
                db.SaveChanges();
                TempData["mainHead"] = "News";
                TempData["cardTitle"] = "Manage News";
                TempData["breadcrumb"] = "Manage-News";
                return RedirectToAction("BackIndex");
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
            TempData["mainHead"] = "News";
            TempData["cardTitle"] = "Manage News";
            TempData["breadcrumb"] = "Manage-News";
            return RedirectToAction("Create");

        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            news ms_items = db.news.Find(id);
            if (ms_items == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_id = new SelectList(db.categories, "id", "cname", ms_items.category_id);
            TempData["mainHead"] = "News";
            TempData["cardTitle"] = "Manage News";
            TempData["breadcrumb"] = "Manage-News";
            return View("~/Views/news/backend/Edit.cshtml", ms_items);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase items_img, HttpPostedFileBase trailerUrl, HttpPostedFileBase video)
        {
            //ms_items i = new ms_items();
            int id = Convert.ToInt32(Request.Form["id"]);
            var i = (from it in db.news
                     where it.id == id
                     select it).FirstOrDefault();

            string filepath = null;
            string videofilepath = null;
            string posterfilepath = null;
            string imgname = null, imgfilex = null, filex = null, encryptedName = null, postername = null, posterfilex = null, encryptedposterName = null, videofilename = null, encryptedvideofile = null, videofilex = null;

            if (items_img != null)
            {
                filepath = Server.MapPath("~/Uploads/" + items_img.FileName);
            }

            
            try
            {
                if (filepath != null)
                {
                    if (System.IO.File.Exists(filepath))
                    {
                        System.IO.File.Delete(filepath);
                    }
                }

                if (posterfilepath != null)
                {
                    if (System.IO.File.Exists(posterfilepath))
                    {
                        System.IO.File.Delete(posterfilepath);
                    }
                }

                if (videofilepath != null)
                {
                    if (System.IO.File.Exists(videofilepath))
                    {
                        System.IO.File.Delete(videofilepath);
                    }
                }

                if (ModelState.IsValid)
                {

                    if (items_img != null)
                    {
                        imgname = System.IO.Path.GetFileNameWithoutExtension(items_img.FileName);
                        filex = System.IO.Path.GetExtension(items_img.FileName);
                        encryptedName = enctxt.TextEncryp(imgname);
                        string imgpath = Server.MapPath("~/Uploads/" + encryptedName);
                        items_img.SaveAs(imgpath + filex);
                    }


                    i.newsTitle = Request.Form["item_name"];
                    i.newsDescription = Request.Form["items_description"];
                    i.category_id = Convert.ToInt32(Request.Form["category_id"]);
                    string username = Session["backUname"].ToString();
                    i.createdby = (username != null) ? username : "Admin";
                    i.modifiedby = (username != null) ? username : "Admin";
                    i.createdDate = DateTime.Now;
                    i.modifiedDate = DateTime.Now;
                    if (encryptedName != null)
                    {
                        i.news_img = encryptedName + filex;
                    }
                    db.news.AddOrUpdate(i);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                news ms_items = db.news.Find(id);
                TempData["mainHead"] = "News";
                TempData["cardTitle"] = "Manage News";
                TempData["breadcrumb"] = "Manage-News";
                ViewBag.category_id = new SelectList(db.categories, "id", "cname", ms_items.category_id);
                return View("~/Views/news/backend/Edit.cshtml",ms_items);

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

            news items = db.news.Find(id);
            TempData["mainHead"] = "News";
            TempData["cardTitle"] = "Manage News";
            TempData["breadcrumb"] = "Manage-News";
            ViewBag.category_id = new SelectList(db.categories, "id", "cname", items.category_id);
            return View("~/Views/news/backend/Edit.cshtml",items);
        }

        // POST: ms_items/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            news  ms_items = db.news.Find(id);
            string filepath = null;
            if (ms_items.news_img != null)
            {
                filepath = Server.MapPath("~/Uploads/" + ms_items.news_img);
            }
            if (filepath != null)
            {
                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }
            }
            db.news.Remove(ms_items);
            db.SaveChanges();
            return RedirectToAction("BackIndex");
        }
    }
}
