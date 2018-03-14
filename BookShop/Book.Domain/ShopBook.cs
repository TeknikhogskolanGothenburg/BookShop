using System;
using System.Collections.Generic;


namespace BookShop.Domain
{
    public class ShopBook
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
    }
}
