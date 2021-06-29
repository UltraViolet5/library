using System;
using System.Collections.Generic;
using System.Linq;
using library.Infrastructure.Daos;
using library.Infrastructure.Daos.Implementations;
using library.Model;
using Xamarin.Forms.Internals;

namespace library.Services
{
    public class DBService
    {
        private readonly IBookDao _bookDao;
        private readonly IBorrowingDao _borrowingDao;
        private readonly IUserDao _userDao;

        public DBService()
        {
            _bookDao = BookDaoMemory.S;
            _borrowingDao = BorrowingDaoMemory.S;
            _userDao = UserDaoMemory.S;

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

        public void AddBook(Book book)
        {
            _bookDao.Add(book);
        }
        public void AddBorrowings(IEnumerable<Borrowing> borrowings)
        {
            foreach (Borrowing borrowing in borrowings)
            {
                _borrowingDao.Add(borrowing);
            }
        }

        public void AddUsers(IEnumerable<User> users)
        {
            foreach (User user in users)
            {
                _userDao.Add(user);
            }
        }

        public IEnumerable<User> GetMates()
        {
            return _userDao.GetAll();
        }

        public IEnumerable<Borrowing> GetBorrowings()
        {
            return _borrowingDao.GetAll();
        }

        public void AddUser(User user)
        {
            _userDao.Add(user);
        }

        public User GetUser(string email)
        {
            return _userDao.GetAll().FirstOrDefault(x => x.Email == email);
        }

        public void UpdateUser(User newUser)
        {
            Utils.SaveUserInSession(newUser);
            if (!String.IsNullOrWhiteSpace(newUser.PasswordHash))
            {
                App.CurrentUser.PasswordHash = newUser.PasswordHash;
            }
        }
    }
}
