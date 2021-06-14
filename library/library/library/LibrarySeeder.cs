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

        public LibrarySeeder(DBService dbService)
        {
            _dbService = dbService;
        }

        public void Seed()
        {
            _dbService.AddBooks(GetBooks());
            _dbService.AddCategories(GetCategories());
        }

        private IEnumerable<Category> GetCategories()
        {
            return new List<Category>()
            {
                new Category() {Name = "fantasy"},
                new Category() {Name = "action"},
                new Category() {Name = "it"},
                new Category() {Name = "crime"},
                new Category() {Name = "documentary"}
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
                Authors = new List<Author>() {author, author2},
                PublishingYear = new DateTime(2020,1,1),
                Read = true,
                Available = false
            });
            books.Add(new Book()
            {
                Title = "Second super book",
                Authors = new List<Author>() {author, author2},
                PublishingYear = new DateTime(2020, 1, 1),
                Available = true,
                Read = true
            });
            books.Add(new Book()
            {
                Title = "Third book",
                Authors = new List<Author>() {author, author2},
                PublishingYear = new DateTime(2020, 1, 1),
                Available = true,
                Read = false
            });

            return books;
        }
    }
}