using System;
using System.Collections.Generic;


namespace BookShop.Domain
{
    public class Quote
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Book FromBook { get; set; }
        public int BookId { get; set; }

    }
}
