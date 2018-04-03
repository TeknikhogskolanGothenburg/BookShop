using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShop.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Data 
{
    public class BookAuthorRepository : GenericRepository<BookContext, BookAuthor>
    {
        public virtual void AddBookToAuthor(string authorLastName, string bookTitle)
        {
            var authorRepo = new AuthorsRepository();
            var author = authorRepo.FindBy(a => a.LastName.StartsWith(authorLastName)).FirstOrDefault();
            Console.WriteLine(author.FirstName);
            var bookRepo = new BooksRepository();
            var book = bookRepo.FindBy(b => b.Title.StartsWith(bookTitle)).FirstOrDefault();
            Console.WriteLine(book.Title);
            var context = new BookContext();
            context.Add(new BookAuthor { BookId = book.Id, AuthorId = author.Id });
            Console.WriteLine("Waiting for books and authors");
            context.SaveChanges();
        }
    }
}
