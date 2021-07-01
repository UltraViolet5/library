using System;
using System.Collections.Generic;
using System.Text;

namespace library.Model
{
    public class Bookcase
    {
        public int Id { get; private set; }
        public string Name { get; set; } = "No bookcase name";

        public Bookcase()
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
