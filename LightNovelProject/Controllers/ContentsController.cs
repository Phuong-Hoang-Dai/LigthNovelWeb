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
    public class ContentsController : Controller
    {
        private LightNovelEntities2 db = new LightNovelEntities2();

        // GET: Contents
        public ActionResult Index()
        {
            var contents = db.Contents.Include(c => c.Novel);
            return View(contents.ToList());
        }

        // GET: Contents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = db.Contents.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);
        }

        // GET: Contents/Create
        public ActionResult Create()
        {
            ViewBag.ID_Novel = new SelectList(db.Novels, "ID_Novel", "Name_NoveL");
            return View();
        }

        // POST: Contents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Chapter,ID_Novel,Chapter,Name_Chapter,Content1")] Content content)
        {
            if (ModelState.IsValid)
            {
                db.Contents.Add(content);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Novel = new SelectList(db.Novels, "ID_Novel", "Name_NoveL", content.ID_Novel);
            return View(content);
        }

        // GET: Contents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = db.Contents.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Novel = new SelectList(db.Novels, "ID_Novel", "Name_NoveL", content.ID_Novel);
            return View(content);
        }

        // POST: Contents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Chapter,ID_Novel,Chapter,Name_Chapter,Content1")] Content content)
        {
            if (ModelState.IsValid)
            {
                db.Entry(content).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Novel = new SelectList(db.Novels, "ID_Novel", "Name_NoveL", content.ID_Novel);
            return View(content);
        }

        // GET: Contents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = db.Contents.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);
        }

        // POST: Contents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Content content = db.Contents.Find(id);
            db.Contents.Remove(content);
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
