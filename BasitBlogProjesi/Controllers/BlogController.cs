using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BasitBlogProjesi.Models;

namespace BasitBlogProjesi.Controllers
{
    public class BlogController : Controller
    {
        private VTContext db = new VTContext();

        // GET: Blog
        public ActionResult Index()
        {
            var bloglar = db.Bloglar.Include(b => b.Kategoriler).OrderByDescending(i => i.Id);
            return View(bloglar.ToList());
        }

        // GET: Blog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //.Select(i => new CategoryModel()
            // {
            //     Id = i.Id,
            //     KategoriAdi = i.KategoriAdi,
            //     BlogSayisi = i.Bloglar.Count,
            // })

            Blog blog = db.Bloglar.Find(id);
            
            
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.Kategori = db.Kategoriler
                    .Select(i =>
                    new CategoryModel()
                    {
                        Id = i.Id,
                        KategoriAdi = i.KategoriAdi,
                        BlogSayisi = i.Bloglar.Count
                    })
                    .ToList();
            foreach (var item in ViewBag.Kategori)
            {
                if (item.Id == blog.CategoryId)
                {
                    ViewBag.Kat = item.KategoriAdi;
                }
            }
            
            return View(blog);
        }

        // GET: Blog/Create
        public ActionResult Create()
        {
            ViewBag.Category = db.Kategoriler.ToList();
            //ViewBag.CategoryId = new SelectList(db.Kategoriler, "Id", "KategoriAdi");
            return View();
        }

        // POST: Blog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Baslik,Aciklama,Icerik,Resim,EklenmeTarihi,Onay,Anasayfa,CategoryId")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                db.Bloglar.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Kategoriler, "Id", "KategoriAdi", blog.CategoryId);
            return View(blog);
        }

        // GET: Blog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Bloglar.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Kategoriler, "Id", "KategoriAdi", blog.CategoryId);
            return View(blog);
        }

        // POST: Blog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Baslik,Aciklama,Icerik,Resim,Onay,Anasayfa,CategoryId")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                var entity = db.Bloglar.Find(blog.Id);
                if (entity != null)
                {
                    entity.Baslik = blog.Baslik;
                    entity.Aciklama = blog.Aciklama;
                    entity.Icerik = blog.Icerik;
                    entity.Resim = blog.Resim;
                    entity.Onay = blog.Onay;
                    entity.Anasayfa = blog.Anasayfa;
                    entity.CategoryId = blog.CategoryId;
                    //db.Entry(blog).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }


            }
            ViewBag.CategoryId = new SelectList(db.Kategoriler, "Id", "KategoriAdi", blog.CategoryId);
            return View(blog);
        }

        // GET: Blog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Bloglar.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Bloglar.Find(id);
            db.Bloglar.Remove(blog);
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
