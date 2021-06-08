using System;
using System.Collections.Generic;
using System.Text;
using library.Model;
using library.Services;

namespace library
{
    public class LibrarySeeder
    {
        private readonly BookService _bookService;

        public LibrarySeeder(BookService bookService)
        {
            _bookService = bookService;
        }

        public void Seed()
        {
            _bookService.AddBooks(GetBooks());
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
                Authors = new List<Author>() { author, author2 }
            });
            books.Add(new Book()
            {
                Title = "Second super book",
                Authors = new List<Author>() { author, author2 }
            });
            books.Add(new Book()
            {
                Title = "Third book",
                Authors = new List<Author>() { author, author2 }
            });

            return books;
        }
    }
}
