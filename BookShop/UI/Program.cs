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
        private static BookContext _context = new BookContext();

        static void Main(string[] args)
        {
            //AddBook();
            //AddBooks();
            //GetAllBooks();
            //GetFirst();
            //Find();
            Update();
        }

        public static void Update()
        {
            string titleStart = "Hej";
            var book = _context.Books.Where(m => m.Title.StartsWith(titleStart)).ToList();
            book.ForEach(b => b.Title = "Brott och starff");
           // _context.Books.Add(new Book { Title = "Brott och straff", ReleaseDate = DateTime.Now });
            _context.SaveChanges();
        }

        public static void Find()
        {
            var book1 = _context.Books.FirstOrDefault(b => b.Id == 2);
            var book2 = _context.Books.Find(2);
            Console.WriteLine(book1.Title);
            Console.WriteLine(book2.Title);
        }

        public static void GetFirst()
        {
            string titleStart = "A";
            //var movie = (from m in _context.Movies where m.Title.StartsWith(titleStart) select m).FirstOrDefault();
            var book = _context.Books.FirstOrDefault(m => m.Title.StartsWith(titleStart));
            Console.WriteLine(book.Title);
        }

        public static void GetAllBooks()
        {
            var books1 = _context.Books.ToList();
            //SELECT m.title FROM m movies
            var books2 = (from b in _context.Books select b.Title).ToList();
            var books3 = _context.Books.Where(m => m.Title.StartsWith("En het")).ToList();
            string startTitle = "A";
            var books4 = (from b in _context.Books where b.Title.StartsWith(startTitle) select b).ToList();
            // SELECT * FROM movies
            // foreach(var book in _context.Books)
            foreach (var book in books4)
            {
                Console.WriteLine(book.Title);
            }
        }

        private static void AddBooks()
        {
            Book newBook1 = new Book { Title = "Afrodite", ReleaseDate = DateTime.Now };
            Book newBook2 = new Book { Title = "Omringad av idioter", ReleaseDate = new DateTime (2017, 03, 14) };
            Book newBook3 = new Book { Title = "Are you ok?", ReleaseDate = new DateTime ( 1970, 05, 05 ) };
            List<Book> newBooks = new List<Book> { newBook1, newBook2, newBook3 };
            _context.Books.AddRange(newBooks);
            _context.SaveChanges();
        }

        private static void AddBook()
        {
            Book newBook = new Book();
            newBook.Title = "Svindlande höjder";
            newBook.ReleaseDate = System.DateTime.Now;

            _context.Books.Add(newBook);
            _context.SaveChanges();
        }
    }
}
