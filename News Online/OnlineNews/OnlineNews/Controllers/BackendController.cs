using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MovieShop.Controllers.backend
{
    public class BackendController : Controller
    {
        [Authorize(Roles = "superuser,admin")]
        //GET: backend
        public ActionResult Dashboard()
        {
            return View();
        }

    }
}