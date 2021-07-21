using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using library.Model;
using library.Services;

namespace library
{
    public class LibrarySeeder
    {
        private readonly DBService _dbService;
        private User _admin;
        private IEnumerable<User> _usersList { get; set; }

        public LibrarySeeder(DBService dbService)
        {
            _dbService = dbService;
            _admin = new User()
            {
                Email = "admin",
                // Password = admin
                PasswordHash = "$2a$11$3C0BAwSbIIGYT8wiYt2xauRoyhCJMPC9SknVBW5JcAjOTN47oDfgS",
                UserName = "Admin Admin",
                BirthDate = new DateTime(2001, 2, 14),
                Localization = "Cracow"
            };
            _usersList = GetUsers();
        }

        public void Seed()
        {
            _dbService.AddBooks(GetBooks());
            _dbService.AddBorrowings(GetBorrowings());


            // Make relationship. All users are friends of admin
            _admin.Friends.Add(_usersList.ToList()[1]);
            _admin.Friends.Add(_usersList.ToList()[2]);
            _admin.Friends.Add(_usersList.ToList()[3]);


            _dbService.AddUsers(_usersList);
        }

        private IEnumerable<User> GetUsers()
        {
            return new List<User>()
            {
                _admin,
                new User()
                {
                    UserName = "Janek",
                    Email = "Email",
                    Localization = "Cracow"
                },
                new User()
                {
                    UserName = "Brian",
                    Email = "Email2",
                    Localization = "Warsaw"
                },
                new User()
                {
                    UserName = "Pep",
                    Email = "Email3",
                    Localization = "Cracow"
                }
            };
        }

        private IEnumerable<Borrowing> GetBorrowings()
        {
            return new List<Borrowing>()
            {
                new Borrowing()
                {
                    BookId = 5,
                    Borower = _admin,
                    Client = _usersList.ToList()[0],
                    Date = new DateTime(2021, 5, 4),
                    ReturnDate = new DateTime(2021, 9, 3),
                },
                new Borrowing()
                {
                    BookId = 4,
                    Borower = _admin,
                    Client = _usersList.ToList()[1],
                    Date = new DateTime(2021, 5, 2),
                    ReturnDate = new DateTime(2021, 10, 21),
                },
                new Borrowing()
                {
                    BookId = 3,
                    Borower = _admin,
                    Client = _usersList.ToList()[2],
                    Date = new DateTime(2021, 5, 1),
                    ReturnDate = new DateTime(2021, 8, 12),
                }
            };
        }

        private IEnumerable<Book> GetBooks()
        {
            List<Book> books = new List<Book>();

            books.Add(new Book()
            {
                // Id = 0,
                Title = "First book Title",
                Authors = "Jarek, Krzysiek",
                PublishingYear = new DateTime(2020, 1, 1),
                Read = true,
                Available = false,
                Owner = _admin,
                Category = Category.Crime
            });
            books.Add(new Book()
            {
                // Id = 1,
                Title = "Second super book",
                Authors = "Jarek",
                PublishingYear = new DateTime(2020, 1, 1),
                Available = true,
                Read = true,
                Owner = _admin,
                Category = Category.Novel
            });
            books.Add(new Book()
            {
                // Id = 2,
                Title = "Third book",
                Authors = "Krzysiek",
                PublishingYear = new DateTime(2020, 1, 1),
                Available = true,
                Read = false,
                Owner = _admin,
                Category = Category.Documentary
            });
            books.Add(new Book()
            {
                // Id = 3,
                Title = "Book1",
                Authors = "Jarek, Krzysiek",
                PublishingYear = new DateTime(2020, 1, 1),
                Read = true,
                Available = false,
                Owner = _admin,
                Category = Category.Crime
            });
            books.Add(new Book()
            {
                // Id = 4,
                Title = "Book2",
                Authors = "Jarek",
                PublishingYear = new DateTime(2020, 1, 1),
                Available = true,
                Read = true,
                Owner = _admin,
                Category = Category.Novel
            });
            books.Add(new Book()
            {
                // Id = 5,
                Title = "Book3",
                Authors = "Krzysiek",
                PublishingYear = new DateTime(2020, 1, 1),
                Available = true,
                Read = false,
                Owner = _admin,
                Category = Category.Documentary
            });


            return books;
        }
    }
}