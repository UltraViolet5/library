using System;

namespace library.Model
{
    public class Borrowing
    {
        public int Id { get; private set; }
        public User Client { get; set; }
        public User Borrower { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public DateTime Date { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
