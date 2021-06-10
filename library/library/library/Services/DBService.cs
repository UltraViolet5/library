using System.Collections.Generic;
using System.Linq;
using library.Daos;
using library.Daos.Implementations;
using library.Model;

namespace library.Services
{
    public class DBService
    {
        private readonly IBookDao _bookDao;
        private readonly ICategoryDao _categoryDao;

        public DBService()
        {
            _bookDao = BookDaoMemory.S;
            _categoryDao = CategoryDaoMemory.S;

            var seeder = new LibrarySeeder(this);
            seeder.Seed();
        }

        public IEnumerable<Book> GetBooks(int? limit = null)
        {
            if (limit == null)
            {
                return _bookDao.GetAll();
            }

            return _bookDao.GetAll().Take((int)limit);
        }

        public void AddBooks(IEnumerable<Book> books)
        {
            foreach (Book book in books)
            {
                _bookDao.Add(book);
            }
        }

        public IEnumerable<Category> GetCategories()
        {
            return _categoryDao.GetAll();
        }

        public void AddCategories(IEnumerable<Category> categories)
        {
            foreach (Category category in categories)
            {
                _categoryDao.Add(category);
            }
        }
    }
}
