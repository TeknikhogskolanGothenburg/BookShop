using System.Linq;
using Microsoft.EntityFrameworkCore;
using BookShop.Data;
using BookShop.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UI
{
    public class SingleObjectModification
    {
        
        private static BookContext _context = new BookContext();

        public static void AddRateWithCK()
        {
            var rateRepo = new RatingsRepository();
            try
            {
                rateRepo.Add(new Rating { Magazine = "Samuel", Points = 5, RatingDate = DateTime.Now, BookId = 17 });
                rateRepo.Save();
                Console.WriteLine("Added new rating successfully");
            }
            catch
            {
                Console.WriteLine("Can't add a rating with less than 1 or higher than 5 points...");
            }
        }

        public static void SelectUsingStoredProcedure()
        {
            string searchString = "Om";
            var books = _context.Books.FromSql("EXEC FilterBooksByTitlePart {0}", searchString).ToList();
            foreach (var book in books)
            {
                Console.WriteLine(book.Title);
            }
        }

        public static void SelectRawSqlWithOrderingAndFilter()
        {
            var books = _context.Books.FromSql("SELECT * FROM Books")
                .OrderByDescending(m => m.ReleaseDate)
                .Where(m => m.Title.StartsWith("Brott"))
                .ToList();
            foreach (var book in books)
            {
                Console.WriteLine(book.Title);
            }
        }

        public static void SelectRawSql()
        {
            string sql = "SELECT * FROM Books";
            var books = _context.Books.FromSql(sql).ToList();
            foreach (var book in books)
            {
                Console.WriteLine(book.Title);
            }
        }

        public static void DeleteManyDisconnected()
        {
            string titleStart = "Fint";
            var books = _context.Books.Where(m => m.Title.StartsWith(titleStart)).ToList();

            //Här tänker vi att vi inte längre har kvar orginal contexten
            var newContext = new BookContext();
            newContext.Books.RemoveRange(books);
            newContext.SaveChanges();
        }

        public static void DeleteMany()
        {
            var bookRepo = new BooksRepository();
            var books = bookRepo.GetAll()
                .Where(b => b.Title.StartsWith("Om")).ToList();
            bookRepo.DeleteRange(books);
            bookRepo.Save();
        }

        public static void DeleteOne()
        {
            var bookRepo = new BooksRepository();
            var book = bookRepo.FindBy(b => b.Id == 11).FirstOrDefault();
            bookRepo.Delete(book);
            bookRepo.Save();
        }

        public static void UpdateDisconnected()
        {
            var book = _context.Books.Find(3);
            book.ReleaseDate = new DateTime(1992, 10, 14);

            //Här tänker vi att vi inte längre har kvar orginal contexten

            var newContext = new BookContext();
            newContext.Books.Update(book);
            newContext.SaveChanges();

        }

        //Gör så att FindBy metoden anropas och ugå ifrån den (multitrådning)
        public static void Update()
        {
            var quoteRepo = new QuotesRepository();
            var quote = quoteRepo.FindBy(q => q.Text.StartsWith("What")).FirstOrDefault();
            quote.Text = "Oh my darling";
            quoteRepo.Update(quote);
                quoteRepo.Save();
                
        }

        public async static void GetAll()
        {
            var bookRepo = new BooksRepository();
            var authorRepo = new AuthorsRepository();
           
            Task<ICollection<Book>> books = bookRepo.GetAllAsync();
            foreach (var book in books.Result)
            {
                Console.WriteLine(book.Title);
            }

            Task<ICollection<Author>> authors = authorRepo.GetAllAsync();
            foreach (var author in authors.Result)
            {
                Console.WriteLine(author.FirstName);
            }

            await Task.WhenAll(books, authors);
            Console.WriteLine("Both tasks done");
            Console.ReadKey();           
        }

        public static void GetAllBy()
        {
           
            var bookRepo = new BooksRepository();
            var books = bookRepo.FindBy(b => b.Title.StartsWith("Hap"));
            foreach (var book in books)
            {
                Console.WriteLine(book.Title);
            }

            var book2 = bookRepo.FindBy(b => b.Id == 1).FirstOrDefault();
            Console.WriteLine(book2.Title);
        }

        public static void AddMany()
        {
            var shopRepo = new ShopsRepository();
            shopRepo.AddRange(new List<Shop> { new Shop { Name = "Yellow Submarine", Address = "Anektodgatan 2, Stockholm" },
                new Shop { Name = "Läsa", Address = "Fantasigatan 30, Malmö" } });
            shopRepo.Save();
        }

       public static void AddAuthor()
        {
            var authorRepo = new AuthorsRepository();
            authorRepo.Add(new Author { FirstName = "Rasmus", LastName = "Berg", BirthDay = new DateTime(1984, 12, 25) });
            authorRepo.Save();
            Console.WriteLine("Added new author");
        }

       public static void AddBook()
        {
            var bookRepo = new BooksRepository();
            bookRepo.Add(new Book { Title = "Var dig själv", ReleaseDate = new DateTime(2005, 6, 17) });
            bookRepo.Save();
            Console.WriteLine("Added new book");

        }

       

       
    }
}
