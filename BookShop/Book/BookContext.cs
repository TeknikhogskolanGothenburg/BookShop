using Microsoft.EntityFrameworkCore;
using BookShop.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace BookShop.Data
{
    public class BookContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<ShopBook> ShopBooks { get; set; }

        public static readonly LoggerFactory BookLoggerFactory
        = new LoggerFactory(new[] {
            new ConsoleLoggerProvider((category, level)
                => category == DbLoggerCategory.Database.Command.Name
                && level == LogLevel.Information, true)
      });

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>().HasKey(m => new { m.AuthorId, m.BookId }); //Säger till databasaen att kombinationen av ActorId och MovieId i MovieActor tabellen är unik.
            modelBuilder.Entity<ShopBook>().HasKey(b => new { b.BookId, b.ShopId });
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                 .EnableSensitiveDataLogging()
                 .UseLoggerFactory(BookLoggerFactory)
                 .UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = BookShopDb; Trusted_Connection = True;");
        }

    }
}
