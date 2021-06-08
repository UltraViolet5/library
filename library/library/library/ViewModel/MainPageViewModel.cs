using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using library.Daos.Implementations;
using library.Infrastructure;
using library.Services;

namespace library.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        public IEnumerable<BookViewModel> Books { get; private set; }

        private BookService _bookService;

        public MainViewModel()
        {
            _bookService = new BookService(BookDaoMemory.S);

            var seeder = new LibrarySeeder(_bookService);
            seeder.Seed();

            Books = _bookService.GetBooks().Select(b => new BookViewModel(b));
        }
    }
}
