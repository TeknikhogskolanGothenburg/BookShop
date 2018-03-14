using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UI
{
    public partial class BookShopDbContext : DbContext
    {
        public virtual DbSet<Authors> Authors { get; set; }
        public virtual DbSet<BookAuthor> BookAuthor { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<Quotes> Quotes { get; set; }
        public virtual DbSet<Ratings> Ratings { get; set; }
        public virtual DbSet<ShopBook> ShopBook { get; set; }
        public virtual DbSet<Shops> Shops { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server = (localdb)\mssqllocaldb; Database = BookShopDb; Trusted_Connection = True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>(entity =>
            {
                entity.HasKey(e => new { e.AuthorId, e.BookId });

                entity.HasIndex(e => e.BookId);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.BookAuthor)
                    .HasForeignKey(d => d.AuthorId);

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookAuthor)
                    .HasForeignKey(d => d.BookId);
            });

            modelBuilder.Entity<Quotes>(entity =>
            {
                entity.HasIndex(e => e.BookId);

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Quotes)
                    .HasForeignKey(d => d.BookId);
            });

            modelBuilder.Entity<Ratings>(entity =>
            {
                entity.HasIndex(e => e.BookId);

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.BookId);
            });

            modelBuilder.Entity<ShopBook>(entity =>
            {
                entity.HasKey(e => new { e.BookId, e.ShopId });

                entity.HasIndex(e => e.ShopId);

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.ShopBook)
                    .HasForeignKey(d => d.BookId);

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.ShopBook)
                    .HasForeignKey(d => d.ShopId);
            });
        }
    }
}
