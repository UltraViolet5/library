using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using library.Model;

namespace library.Infrastructure.Daos.Implementations
{
    class BorrowingDaoMemory : IBorrowingDao
    {
        private List<Borrowing> _data = new List<Borrowing>();

        private static BorrowingDaoMemory _s;

        public static BorrowingDaoMemory S
        {
            get
            {
                if (_s == null)
                    _s = new BorrowingDaoMemory();

                return _s;
            }

            private set { _s = value; }
        }

        private BorrowingDaoMemory()
        {
        }

        public void Add(Borrowing item)
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

        public Borrowing Get(int id)
        {
            var result = _data.First(x => x.Id == id);
            if (result == null)
                throw new ArgumentException($"No found Category with id {id}");

            return result;
        }

        public IEnumerable<Borrowing> GetAll()
        {
            return _data;
        }
    }
}
