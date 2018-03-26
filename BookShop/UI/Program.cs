using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BookShop.Data;
using BookShop.Domain;

namespace UI
{
    public class Program
    {
        

        static void Main(string[] args)
        {
            //SingleObjectModification.AddBook();
            //SingleObjectModification.AddBooks();
            //SingleObjectModification.GetAllBooksByTitle();
            //SingleObjectModification.GetAllBooks();
            //SingleObjectModification.GetFirstBook();
            //SingleObjectModification.FindBookById();
            //SingleObjectModification.FindBookByTitle();
            //SingleObjectModification.Update();
            //SingleObjectModification.UpdateDisconnected();
            //SingleObjectModification.DeleteOne();
            //SingleObjectModification.DeleteMany();
            //SingleObjectModification.DeleteManyDisconnected();
            //SingleObjectModification.SelectRawSql();
            //SingleObjectModification.AddAuthors();
            //SingleObjectModification.SelectRawSqlWithOrderingAndFilter();
            //SingleObjectModification.SelectUsingStoredProcedure();
            //SingleObjectModification.AddShops();

            //AddAuthorsToBook();
            //AddBookAuthorsId(2, 6);
            //AddBooksToShop();
            //DisplayBooksEagerLoad();
            //AddManyToManyObject();
            //AddQuotesToBook();
            //AddRating();
            //ProjectionLoading();
            //ProjectionLoading2();
            //SelectBooksAndShops();
            FindBookByAuthor();


        }

        public static void FindBookByAuthor()
        {
            var context = new BookContext();
            var books = context.Books.FromSql("SELECT Books.Id, Title, ReleaseDate FROM Books JOIN BookAuthors ON " +
                "BookAuthors.BookId = Books.Id JOIN Authors ON BookAuthors.AuthorId = Authors.Id " +
                "WHERE Authors.LastName LIKE 'Do%'").ToList();
            foreach (var book in books)
            {
                Console.WriteLine(book.Title);
            }
        }



        //Visar alla böcker som finns att köpa
        public static void SelectBooksAndShops()
        {
            var context = new BookContext();
            var books = context.Books
                .Include(b => b.Authors)
                    .ThenInclude(ba => ba.Author)
                .Include(b => b.Shops)
                    .ThenInclude(sb => sb.Shop)
                .ToList(); 

            
           
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

        public static void AddManyToManyObject()
        {
            var context = new BookContext();
            var author = new Author { FirstName = "Paolo", LastName = "Roberto", BirthDay = new DateTime(1966, 5, 2)};
            var book = context.Books.Find(6);
            context.Add(author);
            context.Add(new BookAuthor { Book = book, Author = author });
            context.SaveChanges();
        }

        //returerar böcker och dess författare samt betyg i webben
        public static List<Book> GetBooksAndAuthors()
        {
            var context = new BookContext();
            var books = context.Books
                .Include(b => b.Authors)
                .Include(b => b.Ratings)
                .ToList();
            return books;
        }

        public static void DisplayBooksEagerLoad()
        {
            var context = new BookContext();
            var books = context.Books
                .Include(b => b.Authors)
                    .ThenInclude(ba => ba.Author)                       
                .Include(b => b.Ratings)
                .Include(b => b.Quotes)
                .Include(b => b.Shops )
                    .ThenInclude(sb => sb.Shop)
                .ToList();

            Console.WriteLine("\n\n\n====================\n");
            foreach (var book in books)
            {
                Console.Write(book.Title);
                if (null != book.Ratings)
                {
                    foreach(var rating in book.Ratings)
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
            var context = new BookContext();
            var shop = context.Shops.First();
            var books = context.Books.ToList();
            foreach(var book in books)
            {
                context.ShopBooks.Add(new ShopBook { BookId = book.Id, ShopId = shop.Id });
            }
            context.SaveChanges();
        }

        public static void AddBookAuthorsId(int authorId, int bookId)
        {
            var context = new BookContext();
            context.BookAuthors.Add(new BookAuthor { AuthorId = authorId, BookId = bookId });
            context.SaveChanges();
        }

        public static void AddAuthorsToBook()
        {
            var context = new BookContext();
            var book = context.Books.First();
            var authors = context.Authors.ToList();
            foreach (var author in authors)
            {
                context.BookAuthors.Add(new BookAuthor { AuthorId = author.Id, BookId = book.Id });
            }
            context.SaveChanges();
            //authors.ForEach(a => context.BookAuthor.Add(new BookAuthor { AuthorId = a.Id, BookId = book.Id }));

        }
    }
}
