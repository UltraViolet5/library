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
        public IEnumerable<CategoryViewModel> Categories { get; private set; }

        private DBService _dbService;

        public MainViewModel()
        {
            _dbService = new DBService();

            Books = _dbService.GetBooks().Select(b => new BookViewModel(b));
            Categories = _dbService.GetCategories().Select(c => new CategoryViewModel(c));
        }
    }
}