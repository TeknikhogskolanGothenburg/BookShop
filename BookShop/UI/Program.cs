using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BookShop.Data;
using BookShop.Domain;

namespace UI
{
    class Program
    {

        static void Main(string[] args)
        {
            //AddBook();
            //AddBooks();
            //GetAllBooks();
            //GetFirst();
            //Find();
            //Update();
            //UpdateDisconnected();
            //DeleteOne();
            //DeleteMany();
            //DeleteManyDisconnected();
            //SelectRawSql();
            //AddAuthors();
            //SelectRawSqlWithOrderingAndFilter();
            //SelectUsingStoredProcedure();

            //AddAuthorsToBook();
            //DisplayBooksEagerLoad();
            //AddManyToManyObject();
            //AddQuotesToBook();
            //AddRating();
            //ProjectionLoading();
            //ProjectionLoading2();


        }

        private static void ProjectionLoading2()
        {
            var context = new BookContext();
            var projectedAuthor = context.Authors.Select(a =>
                new { a.FirstName, a.LastName })
                .ToList();

            projectedAuthor.ForEach(pa => Console.WriteLine(pa.LastName + " " + pa.FirstName));
        }

        private static void ProjectionLoading()
        {
            var context = new BookContext();
            var projectedBook = context.Books.Select(a =>
                new { a.Title, QuoteCount = a.Quotes.Count })
                .Where(a => a.QuoteCount > 0)
                .ToList();

            projectedBook.ForEach(pb => Console.WriteLine(pb.Title + " has " + pb.QuoteCount + " quotes"));
        }

        private static void AddRating()
        {
            var context = new BookContext();
            var book = context.Books.Find(6);
            var rate = new Rating { BookId = book.Id, Magazine = "New Yorker", RatingDate = DateTime.Now, Points = 5 };
            context.Add(rate);
            context.SaveChanges();
        }

        private static void AddQuotesToBook()
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

        private static void AddManyToManyObject()
        {
            var context = new BookContext();
            var author = new Author { FirstName = "Paolo", LastName = "Roberto", BirthDay = new DateTime(1966, 5, 2)};
            var book = context.Books.Find(6);
            context.Add(author);
            context.Add(new BookAuthor { Book = book, Author = author });
            context.SaveChanges();
        }

        private static void DisplayBooksEagerLoad()
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

        private static void AddAuthorsToBook()
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
