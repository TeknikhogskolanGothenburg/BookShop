using System;
using System.Collections.Generic;

namespace UI
{
    public partial class Authors
    {
        public Authors()
        {
            BookAuthor = new HashSet<BookAuthor>();
        }

        public int Id { get; set; }
        public DateTime BirthDay { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<BookAuthor> BookAuthor { get; set; }
    }
}
