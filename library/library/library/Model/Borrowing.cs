using System;
using System.Collections.Generic;
using System.Text;

namespace library.Model
{
    public class Borrowing
    {
        public int Id { get; set; }
        public User Borrowed { get; set; }
        public Book Book { get; set; }
        public DateTime Date { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
