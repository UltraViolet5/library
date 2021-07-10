using System;
using System.Collections.Generic;
using System.Linq;
using library.Model;

namespace library.Infrastructure.Daos.Implementations
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

            private set => _s = value;
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
            var toRemove = _data.First(x => x.Id == id);
            if (toRemove == null)
                throw new ArgumentException($"No found Category with id {id}");

            _data.Remove(toRemove);
        }

        public Book Get(int id)
        {
            var result = _data.First(x => x.Id == id);
            if (result == null)
                throw new ArgumentException($"No found Category with id {id}");

            return result;
        }

        public IEnumerable<Book> GetAll()
        {
            return _data;
        }

        public IEnumerable<Book> GetBy(User owner)
        {
            return _data.Where(x => x.Owner.Id == owner.Id);
        }

        public IEnumerable<Book> GetBy(User owner, Category category)
        {
            return _data.Where(x => x.Owner.Id == owner.Id && x.Category == category);
        }
    }
}