using System;
using System.Collections.Generic;

namespace UI
{
    public partial class BookAuthor
    {
        public int AuthorId { get; set; }
        public int BookId { get; set; }

        public Authors Author { get; set; }
        public Books Book { get; set; }
    }
}
