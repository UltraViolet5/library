using System;
using System.Collections.Generic;
using System.Text;

namespace library.Daos
{
    public interface IDao<T>
    {
        void Add(T item);
        void Remove(int id);

        T Get(int id);
        IEnumerable<T> GetAll();
    }
}
