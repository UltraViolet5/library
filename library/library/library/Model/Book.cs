using System;
using System.Collections.Generic;
using System.Text;

namespace library.Model
{
    public class Book
    {
        public int Id { get; private set; }
        public string Title { get; set; }
        public List<Author> Authors { get; set; }

        public DateTime PublishingYear { get; set; }
        // TODO implement pictures
        public string Picture { get; set; }
        public User Owner { get; set; }
        public bool Read { get; set; }
        public List<Category> Categories { get; set; }
        public int Votes { get; set; }
        public Bookcase Bookcase { get; set; }
        public bool Available { get; set; }

        public Book()
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
