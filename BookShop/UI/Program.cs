using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BookShop.Data;
using BookShop.Domain;
using System.Threading;
using static System.Threading.Thread;
using System.Threading.Tasks;

namespace UI
{
    public class Program
    {
        

        static void Main(string[] args)
        {


            //SingleObjectModification.GetAll(); 
            //Console.WriteLine("Got all books and authors");
            //GetBooksAndAuthors();
            //Console.WriteLine("Matched");
            //Console.ReadKey();

            //Här tilldelas trådarna varsin metod att exekvera och man kan på så sätt anropa båda metoderna samtidigt samt en tredje metod som körs i main tråden.
            //Thread thread1 = new Thread(SingleObjectModification.AddBook);
            //Thread thread2 = new Thread(SingleObjectModification.AddAuthor);
            //thread1.Start();
            //thread2.Start();
            //thread1.Join();
            //thread2.Join();
            //Console.WriteLine("All added");
            //Console.ReadKey();
            //var baRepo = new BookAuthorRepository();
            //baRepo.AddBookToAuthor("Berg", "Var dig");



            //SingleObjectModification.AddMany();
            //SingleObjectModification.GetAllBy();
            //SingleObjectModification.Update();
            //SingleObjectModification.UpdateDisconnected();
            //SingleObjectModification.DeleteOne();
            //SingleObjectModification.DeleteMany();
            //SingleObjectModification.DeleteManyDisconnected();
            //SingleObjectModification.SelectRawSql();
            //SingleObjectModification.SelectRawSqlWithOrderingAndFilter();
            //SingleObjectModification.SelectUsingStoredProcedure();
            //SingleObjectModification.AddRateWithCK();




            //AddBooksToShop();
            //DisplayBooksEagerLoad();
            //AddQuotesToBook();
            //AddRating();
            //ProjectionLoading();
            //ProjectionLoading2();
            //SelectBooksAndShops();
            //FindBookByAuthor();
            //AddBookToQuote();




        }

        public static void AddBookToQuote()
        {
            var bookRepo = new BooksRepository();
            var quoteRepo = new QuotesRepository();
            Book book = bookRepo.FindBy(b => b.Title.StartsWith("Haparanda")).FirstOrDefault();
            Quote quote = quoteRepo.FindBy(q => q.Id == 5).FirstOrDefault();
            quote.FromBook = book;
            Console.WriteLine(quote.Text + " " + quote.FromBook.Title);
            Console.ReadKey();
        }

        //Visar böcker efter författarens efternamn
        public static void FindBookByAuthor()
        {
            var context = new BookContext();
            var authorLastName = "Do";
            var books = context.Books.FromSql("SELECT Books.Id, Title, ReleaseDate FROM Books JOIN BookAuthors ON " +
                "BookAuthors.BookId = Books.Id JOIN Authors ON BookAuthors.AuthorId = Authors.Id " +
                "WHERE Authors.LastName LIKE '" + @authorLastName + "%'").ToList();
            foreach (var book in books)
            {
                Console.WriteLine(book.Title);
            }
        }



        //Visar alla böcker som finns att köpa
        public static void SelectBooksAndShops()
        {
            var bookRepo = new BooksRepository();
            var books = bookRepo.BookInShop();

            foreach (var book in books)
            {
                if( 0 < book.Shops.Count)
                {

                Console.WriteLine(book.Title + " " + book.ReleaseDate);
                foreach ( var author in book.Authors)
                {
                    Console.WriteLine(author.Author.FirstName + " " + author.Author.LastName);

                }
                foreach (var shop in book.Shops)
                {
                    Console.WriteLine(shop.Shop.Name);
                }

                }

            }
 
        }


        public static void ProjectionLoading2()
        {
            var context = new BookContext();
            var projectedAuthor = context.Authors.Select(a =>
                new { a.FirstName, a.LastName })
                .ToList();

            projectedAuthor.ForEach(pa => Console.WriteLine(pa.LastName + " " + pa.FirstName));
        }



        public static void ProjectionLoading()
        {
            var context = new BookContext();
            var projectedBook = context.Books.Select(a =>
                new { a.Title, QuoteCount = a.Quotes.Count })
                .Where(a => a.QuoteCount > 0)
                .ToList();

            projectedBook.ForEach(pb => Console.WriteLine(pb.Title + " has " + pb.QuoteCount + " quotes"));
        }

        public static void AddRating()
        {
            var context = new BookContext();
            var book = context.Books.Find(6);
            var rate = new Rating { BookId = book.Id, Magazine = "New Yorker", RatingDate = DateTime.Now, Points = 5 };
            context.Add(rate);
            context.SaveChanges();
        }

        public static void AddQuotesToBook()
        {
            var context = new BookContext();
            var book = context.Books.FirstOrDefault(a => a.Title.StartsWith("Brott"));
            if (null != book)
            {
                book.Quotes.Add(new Quote { Text = "What else can I do to get some money?" });
                book.Quotes.Add(new Quote { Text = "Where can I hide the axe?" });
                context.SaveChanges();
            }
        }
        
        public static void GetBooksAndAuthors()
        {
            var bookRepo = new BooksRepository();
            var books = bookRepo.GetBooksAuthorsRatings();
            foreach (var book in books)
            {
                if (book.Authors.Count > 0)
                {
                    Console.WriteLine(book.Title + " " + book.ReleaseDate);

                    foreach (var author in book.Authors)
                    {
                        Console.WriteLine("\t" + author.Author.LastName + ", " + author.Author.FirstName);
                    }                   
                    if (book.Ratings.Count > 0)
                    {
                        foreach (var rating in book.Ratings)
                        {
                            Console.WriteLine("\tRated by: " + rating.Magazine + ", points: " + rating.Points + ", " + rating.RatingDate);
                        }
                    }
                    else
                    {
                            Console.WriteLine("\tBook not rated yet");
                    }
                   
                }
                              
            }

        }


        public static void DisplayBooksEagerLoad()
            {
            var bookRepo = new BooksRepository();
            var books = bookRepo.GetBooksAuthorsAndQuotes();

            Console.WriteLine("\n\n\n====================\n");
            foreach (var book in books)
            {
                Console.Write(book.Title);
                if  (book.Ratings.Count > 0)
                {
                    foreach (var rating in book.Ratings)
                    {
                        Console.WriteLine(" => Magazine: " + rating.Magazine + " Points: " + rating.Points);
                    }
               
                }
                else
                {
                    Console.WriteLine(" => Not Rated!");
                }
                foreach (var author in book.Authors)
                {
                    Console.WriteLine("\t" + author.Author.FirstName + " " + author.Author.LastName);
                    foreach (var quote in book.Quotes)
                    {
                        Console.WriteLine("\t\t" + quote.Text);
                    }
                }
            }
        }

        public static void AddBooksToShop()
        {   
            var shopRepo = new ShopsRepository();
            var shop = shopRepo.FindBy(s => s.Address.StartsWith("Fantasigatan")).FirstOrDefault();
            var bookRepo = new BooksRepository();
            var books = bookRepo.GetAll();
            var sbRepo = new ShopBookRepository();
            foreach(var book in books)
            {
                sbRepo.Add(new ShopBook { BookId = book.Id, ShopId = shop.Id });
            }
            sbRepo.Save();
        }

    }
}
