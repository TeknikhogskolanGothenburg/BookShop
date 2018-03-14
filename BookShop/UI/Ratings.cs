using System;
using System.Collections.Generic;

namespace UI
{
    public partial class Ratings
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Magazine { get; set; }
        public int Points { get; set; }
        public DateTime RatingDate { get; set; }

        public Books Book { get; set; }
    }
}
