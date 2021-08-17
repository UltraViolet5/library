using System;
using System.Collections.Generic;
using System.Text;

namespace library.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public string BarcodeNumber { get; set; }
        public DateTime PublishingYear { get; set; }
        public DateTime AddingDate { get; set; }
        public string OwnerId { get; set; }
        public User Owner { get; set; }
        public bool Read { get; set; }
        public Category Category { get; set; }
        public int Votes { get; set; }
        public Bookcase Bookcase { get; set; }
        public bool Available { get; set; }
        public byte[] Photo { get; set; }
    }
}
