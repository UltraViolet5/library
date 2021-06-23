using System;
using System.Collections.Generic;
using System.Text;

namespace library.Model
{
    public class Category
    {
        public int Id { get; private set; }
        public string Name { get; set; }

        public Category()
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
