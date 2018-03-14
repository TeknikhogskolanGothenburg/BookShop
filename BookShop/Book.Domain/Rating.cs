using System;
using System.Collections.Generic;


namespace BookShop.Domain
{
    public class Rating
    {
        public int Id { get; set; }
        public string Magazine { get; set; }
        public DateTime RatingDate { get; set; }
        public int Points { get; set; } // Sätt constraint 1-5
        public Book TheBook { get; set; }
        public int BookId { get; set; }
    }
}
