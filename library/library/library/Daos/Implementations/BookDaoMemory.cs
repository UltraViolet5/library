using System;
using System.Collections.Generic;
using System.Text;
using library.Model;

namespace library.Daos.Implementations
{
    class BookDaoMemory : IBookDao
    {
        private List<Book> _data = new List<Book>();

        private static BookDaoMemory _s;

        public static BookDaoMemory S
        {
            get
            {
                if (_s == null)
                    _s = new BookDaoMemory();

                return _s;
            }

            private set { _s = value; }
        }

        private BookDaoMemory()
        {
        }

        public void Add(Book item)
        {
            _data.Add(item);
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Book Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetAll()
        {
            return _data;
        }

        public IEnumerable<Book> GetBy(User owner)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetBy(User owner, Category category)
        {
            throw new NotImplementedException();
        }
    }
}
