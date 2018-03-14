using System;
using System.Collections.Generic;

namespace UI
{
    public partial class Quotes
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Text { get; set; }

        public Books Book { get; set; }
    }
}
