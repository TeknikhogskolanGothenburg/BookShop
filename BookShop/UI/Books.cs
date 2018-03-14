using System;
using System.Collections.Generic;

namespace UI
{
    public partial class Books
    {
        public Books()
        {
            BookAuthor = new HashSet<BookAuthor>();
            Quotes = new HashSet<Quotes>();
            Ratings = new HashSet<Ratings>();
            ShopBook = new HashSet<ShopBook>();
        }

        public int Id { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Title { get; set; }

        public ICollection<BookAuthor> BookAuthor { get; set; }
        public ICollection<Quotes> Quotes { get; set; }
        public ICollection<Ratings> Ratings { get; set; }
        public ICollection<ShopBook> ShopBook { get; set; }
    }
}
