using System;
using System.Collections.Generic;
using System.Text;

namespace library.Model
{
    public class Author
    {
        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


        public Author()
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
