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
                .Where(b => b.Title.StartsWith("Var")).ToList();
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

        public static void Update()
        {
            var quoteRepo = new QuotesRepository();
            var quotes = quoteRepo.FindBy(q => q.Text.StartsWith("When you see you are blind")).ToList();
            foreach (var quote in quotes)
            {
                Console.WriteLine(quote.Text);
                quote.Text = "When you can't see you are blind";
                quoteRepo.Update(quote);
                quoteRepo.Save();
                Console.WriteLine("\tNew Quote: " + quote.Text);
            }

        }

        //Här använder jag asyncron metoden för att böcker och författare ska kunna hämtas oberoende av main tråden
        //samt att await pausar all vidare exekvering fram tills alla böcker och författare är framtagna.
        public async static void GetAll()
        {
            var bookRepo = new BooksRepository();
            var authorRepo = new AuthorsRepository();

            Console.WriteLine("Starting");


            Task<ICollection<Book>> books = bookRepo.GetAllAsync();
            foreach (var book in books.Result)
            {
                Console.WriteLine(book.Title);
                    
            }

            Console.WriteLine("In process");
           

            Task<ICollection<Author>> authors = authorRepo.GetAllAsync();
            foreach (var author in authors.Result)
            {
                Console.WriteLine(author.FirstName);
            }

            await Task.WhenAll(books, authors);

            Console.WriteLine("Both tasks done");
                      
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
            var quoteRepo = new QuotesRepository();
            quoteRepo.AddRange(new List<Quote> { new Quote { Text = "When you see you are blind", BookId = 18 },
                new Quote { Text = "When you see you are blind", BookId = 19 },
                new Quote {Text = "Äre nån i stuga?", BookId = 10 } });
            quoteRepo.Save();
        }

       public static void AddAuthor()
        {
            var authorRepo = new AuthorsRepository();
            authorRepo.Add(new Author { FirstName = "Anders", LastName = "Gren", BirthDay = new DateTime(1922, 1, 2) });
            authorRepo.Save();
            Console.WriteLine("Added new author");
        }

       public static void AddBook()
        {
            var bookRepo = new BooksRepository();
            bookRepo.Add(new Book { Title = "Som man bäddar får man ligga", ReleaseDate = new DateTime(2007, 10, 7) });
            bookRepo.Save();
            Console.WriteLine("Added new book");

        }

       

       

       
    }
}
