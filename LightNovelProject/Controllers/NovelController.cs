using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LightNovelProject.Models;
using System.IO;

namespace LightNovelProject.Controllers
{
    public class NovelController : Controller
    {
        private LightNovelEntities2 db = new LightNovelEntities2();
        // GET: Novel
        public ActionResult Manage()
        {
            var novels = db.Novels.Include(n => n.Category1);
            return View(novels.ToList());
        }

        // GET: Novel/Details/5
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

        // GET: Novel/Create
        public ActionResult Create()
        {
            ViewBag.Category = new SelectList(db.Categories, "IDCate", "NameCate");
            return View();
        }

        // POST: Novel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Novel,Name_NoveL,Avatar,Category,Author,Desc,DateUp,Status,Rate,Rate_Count,View_Count,BookMarked_Count,Qty_Chapters")] Novel novel,
            HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    //Lấy tên file của hình được up lên
                    var fileName = Path.GetFileName(Image.FileName);
                    //Tạo đường dẫn tới file
                    var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    //Lưu tên
                    novel.Avatar = "~/Images/" + fileName;
                    //Save vào Images Folder
                    Image.SaveAs(path);
                }
                novel.Rate_Count = novel.Qty_Chapters 
                    = novel.View_Count = novel.BookMarked_Count = 0;
                novel.DateMod = novel.DatePub = novel.DateUp = DateTime.Now;
                novel.Rate =  0;
                novel.Status = false;

                db.Novels.Add(novel);
                db.SaveChanges();
                return RedirectToAction("Manage");
            }
            ViewBag.Category = new SelectList(db.Categories, "IDCate", "NameCate",novel.Category);
            return View(novel);
        }

        // GET: Novel/Edit/5
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

        // POST: Novel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Novel,Name_NoveL,Avatar,Category,Author,Desc,DateUp,Status,Rate,Rate_Count,View_Count,BookMarked_Count,Qty_Chapters")] 
        Novel novel, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                var novelDB = db.Novels.FirstOrDefault(p => p.ID_Novel == novel.ID_Novel);
                if(novelDB != null)
                {
                    novelDB.Name_NoveL = novel.Name_NoveL;
                    novelDB.Qty_Chapters = novel.Qty_Chapters;
                    novelDB.Rate = novel.Rate;
                    novelDB.Rate_Count = novel.Rate_Count;
                    novelDB.View_Count = novel.View_Count;
                    novelDB.Status = novel.Status;
                    novelDB.Author = novel.Author;
                    novelDB.BookMarked_Count = novel.BookMarked_Count;
                    novelDB.DateMod = DateTime.Now;
                    novelDB.DateUp = novel.DateUp;
                    novelDB.DatePub = novel.DatePub;
                    novelDB.Desc = novel.Desc;
                    if (Image != null)
                    {
                        //Lấy tên file của hình được up lên
                        var fileName = Path.GetFileName(Image.FileName);
                        //Tạo đường dẫn tới file
                        var path = Path.Combine(Server.MapPath("~/Images/"),
                       fileName);
                        //Lưu tên
                        novelDB.Avatar = fileName;
                        //Save vào Images Folder
                        Image.SaveAs(path);
                    }
                    novelDB.Category = novel.Category;
                }
                db.SaveChanges();
                return RedirectToAction("Manage");
            }
            ViewBag.Category = new SelectList(db.Categories, "IDCate", "NameCate", novel.Category);
            return View(novel);
        }

        // GET: Novel/Delete/5
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

        // POST: Novel/Delete/5
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
