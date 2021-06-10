using System;
using System.Collections.Generic;
using System.Text;
using library.Model;

namespace library.Daos.Implementations
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
            throw new NotImplementedException();
        }

        public Category Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAll()
        {
            return _data;
        }
    }
}