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
    public class TestController : Controller
    {
        private LightNovelEntities2 db = new LightNovelEntities2();

        // GET: Test
        public ActionResult Index()
        {
            var novels = db.Novels.Include(n => n.Category1);
            return View(novels.ToList());
        }

        // GET: Test/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Novel novel = db.Novels.Find(id);
            if (novel == null)
            {
                return HttpNotFound();
            }
            return View(novel);
        }

        // GET: Test/Create
        public ActionResult Create()
        {
            ViewBag.Category = new SelectList(db.Categories, "IDCate", "NameCate");
            return View();
        }

        // POST: Test/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Novel,Name_NoveL,Avatar,Category,Author,Desc,DateUp,Status,Rate,Rate_Count,View_Count,BookMarked_Count,Qty_Chapters")] Novel novel)
        {
            if (ModelState.IsValid)
            {
                db.Novels.Add(novel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category = new SelectList(db.Categories, "IDCate", "NameCate", novel.Category);
            return View(novel);
        }

        // GET: Test/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Novel novel = db.Novels.Find(id);
            if (novel == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category = new SelectList(db.Categories, "IDCate", "NameCate", novel.Category);
            return View(novel);
        }

        // POST: Test/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Novel,Name_NoveL,Avatar,Category,Author,Desc,DateUp,Status,Rate,Rate_Count,View_Count,BookMarked_Count,Qty_Chapters")] Novel novel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(novel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category = new SelectList(db.Categories, "IDCate", "NameCate", novel.Category);
            return View(novel);
        }

        // GET: Test/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Novel novel = db.Novels.Find(id);
            if (novel == null)
            {
                return HttpNotFound();
            }
            return View(novel);
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Novel novel = db.Novels.Find(id);
            db.Novels.Remove(novel);
            db.SaveChanges();
            return RedirectToAction("Index");
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
