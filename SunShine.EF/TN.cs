namespace SunShine.EF {
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TN : DbContext {
        public TN()
            : base("name=TN") {
        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ManageUser> ManageUsers { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<SiteCategory> SiteCategories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<Article>()
                .Property(e => e.idarticle)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.img)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.idcategory)
                .IsUnicode(false);

            modelBuilder.Entity<ManageUser>()
                .Property(e => e.iduser)
                .IsUnicode(false);

            modelBuilder.Entity<ManageUser>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<ManageUser>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<ManageUser>()
                .Property(e => e.md5salt)
                .IsUnicode(false);

            modelBuilder.Entity<ManageUser>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<ManageUser>()
                .Property(e => e.notes)
                .IsUnicode(false);

            modelBuilder.Entity<Picture>()
                .Property(e => e.idimage)
                .IsUnicode(false);

            modelBuilder.Entity<Picture>()
                .Property(e => e.idmodule)
                .IsUnicode(false);

            modelBuilder.Entity<Picture>()
                .Property(e => e.path)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.idproduct)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.basicinfo)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.img)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.idcategory)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.idcategory)
                .IsUnicode(false);

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.categorycode)
                .IsUnicode(false);

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.categoryname)
                .IsUnicode(false);

            modelBuilder.Entity<SiteCategory>()
                .Property(e => e.idcategory)
                .IsUnicode(false);

            modelBuilder.Entity<SiteCategory>()
                .Property(e => e.categorycode)
                .IsUnicode(false);

            modelBuilder.Entity<SiteCategory>()
                .Property(e => e.categoryname)
                .IsUnicode(false);

            modelBuilder.Entity<SiteCategory>()
                .Property(e => e.englishname)
                .IsUnicode(false);

            modelBuilder.Entity<SiteCategory>()
                .Property(e => e.parentid)
                .IsUnicode(false);
        }
    }
}
