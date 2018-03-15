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
            AddBooks();
        }

        private static void AddBooks()
        {
            Book newBook1 = new Book { Title = "Hej hej Karlsson", ReleaseDate = DateTime.Now };
            Book newBook2 = new Book { Title = "Fint Folk i Finkan 3", ReleaseDate = DateTime.Now };
            List<Book> newBooks = new List<Book> { newBook1, newBook2 };
            _context.Books.AddRange(newBooks);
            Console.WriteLine(newBooks);
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
