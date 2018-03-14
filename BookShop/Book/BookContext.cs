using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using BookShop.Domain;

namespace BookShop.Data
{
    public class BookContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Shop> Shops { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>().HasKey(m => new { m.AuthorId, m.BookId }); //Säger till databasaen att kombinationen av ActorId och MovieId i MovieActor tabellen är unik.
            modelBuilder.Entity<ShopBook>().HasKey(b => new { b.BookId, b.ShopId });
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = BookShopDb; Trusted_Connection = True;");
        }

    }
}
