using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineNews;
using System.Web.Security;

namespace OnlineNews.Controllers
{
    public class loginsController : Controller
    {
        private OnlineNewsEntities db = new OnlineNewsEntities();

        // GET: logins
        //public ActionResult Index()
        //{
        //    var logins = db.logins.Include(l => l.role).Include(l => l.register);
        //    return View(logins.ToList());
        //}

        //// GET: logins/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    login login = db.logins.Find(id);
        //    if (login == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(login);
        //}

        //// GET: logins/Create
        //public ActionResult Create()
        //{
        //    ViewBag.roles_Id = new SelectList(db.roles, "id", "role1");
        //    ViewBag.user_id = new SelectList(db.registers, "id", "fname");
        //    return View();
        //}

        //// POST: logins/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "id,username,pass,cpass,user_id,roles_Id")] login login)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.logins.Add(login);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.roles_Id = new SelectList(db.roles, "id", "role1", login.roles_Id);
        //    ViewBag.user_id = new SelectList(db.registers, "id", "fname", login.user_id);
        //    return View(login);
        //}

        //// GET: logins/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    login login = db.logins.Find(id);
        //    if (login == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.roles_Id = new SelectList(db.roles, "id", "role1", login.roles_Id);
        //    ViewBag.user_id = new SelectList(db.registers, "id", "fname", login.user_id);
        //    return View(login);
        //}

        //// POST: logins/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "id,username,pass,cpass,user_id,roles_Id")] login login)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(login).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.roles_Id = new SelectList(db.roles, "id", "role1", login.roles_Id);
        //    ViewBag.user_id = new SelectList(db.registers, "id", "fname", login.user_id);
        //    return View(login);
        //}

        //// GET: logins/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    login login = db.logins.Find(id);
        //    if (login == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(login);
        //}

        //// POST: logins/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    login login = db.logins.Find(id);
        //    db.logins.Remove(login);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Dashboard", "Backend");
            }
            else
            {
                return View("Index");
            }
        }
        [HttpPost]
        public ActionResult Login(login U)
        {
            OnlineNewsEntities db = new OnlineNewsEntities();
            var count = db.logins.Where(x => x.username == U.username && x.pass == U.pass).Count();
            if (count == 0)
            {
                ViewBag.Msg = "Invalid User";
                return View("Index");

            }
            else
            {
                var lg = db.logins.Where(x => x.username == U.username && x.pass == U.pass).FirstOrDefault();
                var data = (from u in db.registers
                            where u.id == lg.user_id
                            select u).FirstOrDefault();
                if (data.userTypeId == 1)
                {
                    FormsAuthentication.SetAuthCookie(U.username, false);
                    Session["backUname"] = U.username;
                    Session["buserTypeId"] = "1";
                    return RedirectToAction("Dashboard", "Backend");
                }
                return View("Index");
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            return RedirectToAction("Login");
        }

    }
}
