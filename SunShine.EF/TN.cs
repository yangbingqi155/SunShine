namespace SunShine.EF {
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TN : DbContext {
        public TN()
            : base("name=TN") {
        }

        public virtual DbSet<ManageUser> ManageUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
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
        }
    }
}
