using System;
using System.Collections.Generic;


namespace BookShop.Domain
{
    public class Shop
    {
        public Shop()
        {
            Books = new List<ShopBook>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<ShopBook> Books { get; set; }
    }
}
