using System;
using System.Collections.Generic;
using System.Text;
using library.Daos;
using library.Model;

namespace library.Services
{
    public class BookService
    {
        private readonly IBookDao _bookDao;

        public BookService(IBookDao bookDao)
        {
            _bookDao = bookDao;
        }

        public IEnumerable<Book> GetBooks(int? limit = null)
        {
            if (limit == null)
            {
                return _bookDao.GetAll();
            }

            return _bookDao.GetAll();
        }

        public void AddBooks(IEnumerable<Book> books)
        {
            foreach (Book book in books)
            {
                _bookDao.Add(book);
            }
        }
    }
}
