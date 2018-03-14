using System;
using System.Collections.Generic;

namespace UI
{
    public partial class Shops
    {
        public Shops()
        {
            ShopBook = new HashSet<ShopBook>();
        }

        public int Id { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }

        public ICollection<ShopBook> ShopBook { get; set; }
    }
}
