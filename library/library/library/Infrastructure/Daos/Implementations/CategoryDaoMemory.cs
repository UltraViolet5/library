using System;
using System.Collections.Generic;
using System.Linq;
using library.Model;

namespace library.Infrastructure.Daos.Implementations
{
    class CategoryDaoMemory : ICategoryDao
    {
        private List<Category> _data = new List<Category>();

        private static CategoryDaoMemory _S;

        public static CategoryDaoMemory S
        {
            get
            {
                if (_S == null)
                    _S = new CategoryDaoMemory();

                return _S;
            }
            set => _S = value;
        }

        public void Add(Category item)
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

        public Category Get(int id)
        {
            var result = _data.First(x => x.Id == id);
            if (result == null)
                throw new ArgumentException($"No found Category with id {id}");
            
            return result;
        }

        public IEnumerable<Category> GetAll()
        {
            return _data;
        }
    }
}