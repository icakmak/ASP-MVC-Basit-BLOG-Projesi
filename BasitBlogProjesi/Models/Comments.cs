using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasitBlogProjesi.Models
{
    public class Comments
    {
        public int Id { get; set; }
        public string AdSoyad { get; set; }
        public string Mail { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public string Mesaj { get; set; }

        public int BlogId { get; set; }
        public Blog Bloglar { get; set; }
    }
}