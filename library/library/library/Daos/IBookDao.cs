using System;
using System.Collections.Generic;
using System.Text;
using library.Model;

namespace library.Daos
{
    public interface IBookDao : IDao<Book>
    {
        IEnumerable<Book> GetBy(User owner);
        IEnumerable<Book> GetBy(User owner, Category category);
    }
}
