using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShop.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Data
{
    public class BooksRepository : GenericRepository<BookContext, Book>
    {
        public virtual ICollection<Book> BookInShop()
        {
            var context = new BookContext();
                return context.Books
                .Include(b => b.Authors)
                    .ThenInclude(ba => ba.Author)
                .Include(b => b.Shops)
                    .ThenInclude(sb => sb.Shop)
                .ToList();
        }
       

        //returerar böcker och dess författare samt betyg
        public virtual ICollection<Book> GetBooksAuthorsRatings()
        {
            var context = new BookContext();
            return context.Books
            .Include(b => b.Authors)
                .ThenInclude(ba => ba.Author)
            .Include(b => b.Ratings)
            .ToList();

        }

        //Returnerar böcker med författare, ratings och quotes
        public virtual ICollection<Book> GetBooksAuthorsAndQuotes()
        {
            var context = new BookContext();
            return context.Books
                .Include(b => b.Authors)
                    .ThenInclude(ba => ba.Author)
                .Include(b => b.Ratings)
                .Include(b => b.Quotes)
                .Include(b => b.Shops)
                    .ThenInclude(sb => sb.Shop)
                .ToList();
        }

        public virtual ICollection<Book> GetAllWithAuthors()
        {
            var context = new BookContext();
            return context.Books
                 .Include(m => m.Authors)
                 .ThenInclude(ma => ma.Author)
                 .ToList();
        }
    }
}
