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
            //SingleObjectModification.Add();
            //SingleObjectModification.AddMany();
            //SingleObjectModification.GetAllBy();
            //SingleObjectModification.GetAll();
            //SingleObjectModification.Update();
            //SingleObjectModification.UpdateDisconnected();
            //SingleObjectModification.DeleteOne();
            //SingleObjectModification.DeleteMany();
            //SingleObjectModification.DeleteManyDisconnected();
            //SingleObjectModification.SelectRawSql();
            //SingleObjectModification.SelectRawSqlWithOrderingAndFilter();
            //SingleObjectModification.SelectUsingStoredProcedure();


            //AddAuthorsToBook("Le Pain", "Svensson");
            //AddBooksToShop();
            //DisplayBooksEagerLoad();
            //AddManyToManyObject();
            //AddQuotesToBook();
            //AddRating();
            //ProjectionLoading();
            //ProjectionLoading2();
            //SelectBooksAndShops();
            //FindBookByAuthor();
            //GetBooksAndAuthors();


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

        public static void AddManyToManyObject()
        {
            var context = new BookContext();
            var author = new Author { FirstName = "Paolo", LastName = "Roberto", BirthDay = new DateTime(1966, 5, 2)};
            var book = context.Books.Find(6);
            context.Add(author);
            context.Add(new BookAuthor { Book = book, Author = author });
            context.SaveChanges();
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
            var context = new BookContext();
            var shop = context.Shops.First();
            var books = context.Books.ToList();
            foreach(var book in books)
            {
                context.ShopBooks.Add(new ShopBook { BookId = book.Id, ShopId = shop.Id });
            }
            context.SaveChanges();
        }


        public static void AddAuthorsToBook(string bookTitle, string authorLastName)
        {
            var bookRepo = new BooksRepository();
            var book = bookRepo.FindBy(b => b.Title.StartsWith(bookTitle)).FirstOrDefault();
            var authorRepo = new AuthorsRepository();
            var author = authorRepo.FindBy(a => a.LastName.StartsWith(authorLastName)).FirstOrDefault();
            var baRepo = new BookAuthorRepository();
            baRepo.Add(new BookAuthor { AuthorId = author.Id, BookId = book.Id });
            
            baRepo.Save();
            

        }
    }
}
