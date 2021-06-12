using System.Collections.Generic;
using library.Model;

namespace library.Infrastructure.Daos
{
    public interface IBookDao : IDao<Book>
    {
        IEnumerable<Book> GetBy(User owner);
        IEnumerable<Book> GetBy(User owner, Category category);
    }
}
