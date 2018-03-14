using System;
using System.Collections.Generic;

namespace UI
{
    public partial class ShopBook
    {
        public int BookId { get; set; }
        public int ShopId { get; set; }

        public Books Book { get; set; }
        public Shops Shop { get; set; }
    }
}
