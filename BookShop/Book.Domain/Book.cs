using System;
using System.Collections.Generic;


namespace BookShop.Domain
{
    public class Book
    {
        public Book()
        {
            Authors = new List<BookAuthor>();
            Quotes = new List<Quote>();
            Ratings = new List<Rating>();
            Shops = new List<ShopBook>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<BookAuthor> Authors { get; set; }
        public List<Quote> Quotes { get; set; }
        public List<Rating> Ratings { get; set; }
        public List<ShopBook> Shops { get; set; }
    }
}
