using BasitBlogProjesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasitBlogProjesi.Controllers
{
    public class HomeController : Controller
    {
        private VTContext db = new VTContext();
        // GET: Home
        public ActionResult Index()
        {
            var bloglar = db.Bloglar
                .Select(i => new BlogModel()
                {
                    Id = i.Id,
                    Baslik = i.Baslik,
                    KategoriAdi = i.Kategoriler.KategoriAdi,
                    Aciklama = i.Aciklama.Substring(0, 20),
                    Onay = i.Onay,
                    Anasayfa = i.Anasayfa,
                    Resim = i.Resim,
                })
                .Where(i => i.Onay == true && i.Anasayfa == true)
                .OrderByDescending(i => i.Id)
                .ToList();

            ViewBag.Kategori = db.Kategoriler
                .Select(i =>
                new CategoryModel()
                {
                    Id = i.Id,
                    KategoriAdi = i.KategoriAdi,
                    BlogSayisi = i.Bloglar.Count
                })
                .ToList();
            return View(bloglar);
        }

        public ActionResult KategoriBlog(int? Id)
        {
            if (Id != null)
            {
                var bloglar = db.Bloglar
                .Select(i => new BlogModel()
                {
                    Id = i.Id,
                    Baslik = i.Baslik,
                    KategoriAdi = i.Kategoriler.KategoriAdi,
                    Aciklama = i.Aciklama.Substring(0, 20),
                    Onay = i.Onay,
                    Anasayfa = i.Anasayfa,
                    Resim = i.Resim,
                    CategoryId = i.CategoryId
                })
                .Where(i => i.Onay == true && i.Anasayfa == true && i.CategoryId == Id)
                .OrderByDescending(i => i.Id)
                .ToList();

                ViewBag.Kategori = db.Kategoriler
                    .Select(i =>
                    new CategoryModel()
                    {
                        Id = i.Id,
                        KategoriAdi = i.KategoriAdi,
                        BlogSayisi = i.Bloglar.Count
                    })
                    .ToList();
                return View(bloglar);
            }
            return RedirectToAction("Index");
            
        }
    }
}