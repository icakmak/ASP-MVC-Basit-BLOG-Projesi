using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BasitBlogProjesi.Models
{
    public class VTContext:DbContext
    {
        public VTContext():base("BlogDB")
        {
            Database.SetInitializer(new BlogInitializer());
        }
        public DbSet<Category> Kategoriler { get; set; }
        public DbSet<Blog> Bloglar { get; set; }
        public DbSet<Comments> Yorumlar { get; set; }
    }
}