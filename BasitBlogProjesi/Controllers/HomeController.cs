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
                .Where(i => i.Onay == true && i.CategoryId == Id)
                .OrderByDescending(i => i.Id)
                .ToList();
                return View(bloglar);

            }
            return RedirectToAction("Index");

        }
        [ChildActionOnly]
        public PartialViewResult kategorilers()
        {
            ViewBag.Kategori = db.Kategoriler
                .Select(i =>
                new CategoryModel()
                {
                    Id = i.Id,
                    KategoriAdi = i.KategoriAdi,
                    BlogSayisi = i.Bloglar.Count
                })
                .ToList();

            return PartialView("KategoriMenu", ViewBag.Kategori);
        }
        public ActionResult BlogDetail(int? id,int? msj)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
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

            ViewBag.Msj = msj;
            ViewBag.Comments = db.Yorumlar.Where(i => i.BlogId == id).ToList();

            return View(blog);
        }


        public ActionResult BlogSearch(string search)
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
            .Where(i => i.Onay == true && i.Baslik.Contains(search))
            .OrderByDescending(i => i.Id)
            .ToList();
            return View(bloglar);
        }
        [HttpPost]
        public ActionResult Comments(string AdSoyad,string Mail,string Mesaj, int BlogId)
        {
            int msj = 0;
            if (Mesaj.Length > 0)
            {
                Comments com = new Comments();
                com.Mesaj = Mesaj;
                com.BlogId = BlogId;
                com.Mail = Mail;
                com.EklenmeTarihi = DateTime.Now;
                com.AdSoyad = AdSoyad;
                var cm = db.Yorumlar.Add(com);
                db.SaveChanges();
                if (cm != null)
                {
                    msj = 1;
                    return RedirectToAction("BlogDetail", new { id = BlogId, msj = msj });
                }
            }
            
            msj = 2;
            return RedirectToAction("BlogDetail", new { id = BlogId, msj = msj });

        }
    }
}