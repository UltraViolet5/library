using System.Collections.Generic;

namespace library.Infrastructure.Daos
{
    public interface IDao<T>
    {
        void Add(T item);
        void Remove(int id);

        T Get(int id);
        IEnumerable<T> GetAll();
    }
}
