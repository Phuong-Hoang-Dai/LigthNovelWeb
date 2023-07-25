using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LightNovelProject.Models;

namespace LightNovelProject.Controllers
{
    public class SiteController : Controller
    {
        private LightNovelEntities2 db = new LightNovelEntities2();

        public ActionResult Home()
        {
            var novels = db.Novels.Include(n => n.Category1);
            return View(novels.ToList());
        }
        

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
