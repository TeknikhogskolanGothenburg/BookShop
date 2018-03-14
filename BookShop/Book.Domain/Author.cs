using System;
using System.Collections.Generic;


namespace BookShop.Domain
{
    public class Author
    {
        public Author()
        {
            Books = new List<BookAuthor>();
        }
       
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public List<BookAuthor> Books { get; set; }
    }
}
