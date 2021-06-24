﻿using System;
using System.Collections.Generic;
using System.Text;
using library.Model;
using library.Services;

namespace library
{
    public class LibrarySeeder
    {
        private readonly DBService _dbService;

        public LibrarySeeder(DBService dbService)
        {
            _dbService = dbService;
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
                new User()
                {
                    Email = "admin",
                    // Password = admin
                    PasswordHash = "$2a$11$3C0BAwSbIIGYT8wiYt2xauRoyhCJMPC9SknVBW5JcAjOTN47oDfgS"
                },
                new User(),
                new User(),
                new User()
            };
        }

        private IEnumerable<Borrowing> GetBorrowindg()
        {
            return new List<Borrowing>()
            {
                new Borrowing() {ReturnDate = new DateTime(2021, 9, 3),},
                new Borrowing() {ReturnDate = new DateTime(2021, 10, 21),},
                new Borrowing() {ReturnDate = new DateTime(2021, 8, 12),},
            };
        }
        
        private IEnumerable<Book> GetBooks()
        {
            var author = new Author()
            {
                FirstName = "Jarek",
                LastName = "Kaa"
            };
            var author2 = new Author()
            {
                FirstName = "Krzysiek",
                LastName = "Fi"
            };

            List<Book> books = new List<Book>();

            books.Add(new Book()
            {
                Title = "First book Title",
                Authors = "Jarek, Krzysiek",
                PublishingYear = new DateTime(2020,1,1),
                Read = true,
                Available = false
            });
            books.Add(new Book()
            {
                Title = "Second super book",
                Authors = "Jarek",
                PublishingYear = new DateTime(2020, 1, 1),
                Available = true,
                Read = true
            });
            books.Add(new Book()
            {
                Title = "Third book",
                Authors = "Krzysiek",
                PublishingYear = new DateTime(2020, 1, 1),
                Available = true,
                Read = false
            });

            return books;
        }
    }
}