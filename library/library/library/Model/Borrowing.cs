using System;
using System.Collections.Generic;
using System.Text;

namespace library.Model
{
    public class Borrowing
    {
        public int Id { get; private set; }
        public User Client { get; set; }
        //public Book Book { get; set; }
        public User Borower { get; set; }
        public int BookId { get; set; }
        public DateTime Date { get; set; }
        public DateTime ReturnDate { get; set; }

        public Borrowing()
        {
            Id = NextId;
        }

        private static int _nextId = 0;
        public static int NextId
        {
            get
            {
                int toReturn = _nextId;
                _nextId++;
                return toReturn;
            }
        }
    }
}
