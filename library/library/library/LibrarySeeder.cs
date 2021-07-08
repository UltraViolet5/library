using System;
using System.Collections.Generic;
using System.Text;
using library.Model;
using library.Services;

namespace library
{
    public class LibrarySeeder
    {
        private readonly DBService _dbService;
        private User _admin;

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
        }

        public void Seed()
        {
            _dbService.AddBooks(GetBooks());
            _dbService.AddBorrowings(GetBorrowindg());
            _dbService.AddUsers(GetUsers());
        }

        private IEnumerable<User> GetUsers()
        {
            return new List<User>()
            {
                _admin,
                new User(),
                new User(),
                new User()
            };
        }

        private IEnumerable<Borrowing> GetBorrowindg()
        {
            return new List<Borrowing>()
            {
                new Borrowing() {Book = new Book(){Title= "book1",Authors="Author1" } ,ReturnDate = new DateTime(2021, 9, 3),},
                new Borrowing() {Book = new Book(){Title= "book2",Authors="Author2" } ,ReturnDate = new DateTime(2021, 10, 21),},
                new Borrowing() {Book = new Book(){Title= "book3",Authors="Author3" } ,ReturnDate = new DateTime(2021, 8, 12),},
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
                PublishingYear = new DateTime(2020,1,1),
                Read = true,
                Available = false,
                Owner = _admin,
                Categories = Category.Crime
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
                Categories = Category.Novel
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
                Categories = Category.Documentary
            });

            return books;
        }
    }
}