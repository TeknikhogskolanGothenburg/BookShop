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
            //Update();
            //UpdateDisconnected();
            //DeleteOne();
            //DeleteMany();
            //DeleteManyDisconnected();
            //SelectRawSql();
            //AddAuthors();
            //SelectRawSqlWithOrderingAndFilter();
            //SelectUsingStoredProcedure();

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

        public static void AddAuthors()
        {
            Author author1 = new Author();
            author1.FirstName = "Fjodor";
            author1.LastName = "Dostojevskij";
            author1.BirthDay = new DateTime(1821, 11, 11);
           
            Author author2 = new Author();
            author2.FirstName = "Kalle";
            author2.LastName = "Svensson";
            author2.BirthDay = new DateTime(1983, 4, 19);

            _context.Authors.AddRange(author1, author2);
            _context.SaveChanges();
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
            string titleStart = "A";
            var books = _context.Books.Where(m => m.Title.StartsWith(titleStart)).ToList();
            _context.Books.RemoveRange(books);
            _context.SaveChanges();
        }

        public static void DeleteOne()
        {
            var book = _context.Books.Find(2);
            // var movie2 = new Movie { Id = 99, Title = "kjshdf", ReleaseDate = DateTime.Now};
            _context.Books.Remove(book);
            _context.SaveChanges();
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
            string titleStart = "Hej";
            var book = _context.Books.Where(m => m.Title.StartsWith(titleStart)).ToList();
            book.ForEach(b => b.Title = "Brott och starff");
           // _context.Books.Add(new Book { ReleaseDate = DateTime.Now });
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
