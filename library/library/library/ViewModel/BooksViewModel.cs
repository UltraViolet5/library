using library.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace library.ViewModel
{
    public class BooksViewModel : BaseViewModel
    {

        public IEnumerable<BookViewModel> Books { get; private set; }

        public ICommand AddBookCommand { get; private set; }
        public ICommand ShowBookCommand { get; private set; }

        public BooksViewModel()
        {
            Books = App.DbService.GetBooks()
                .Where(b => b.Owner.Email == Utils.GetCurrentUserEmail())
                .Select(b => new BookViewModel(b));

            AddBookCommand = new Command(AddBookExecute);
            ShowBookCommand = new Command(ShowBookExecute);
        }

        private void AddBookExecute()
        {
            App.Navigation.PushAsync(new AddBookPage());
        }

        private void ShowBookExecute(object obj)
        {
            App.Navigation.PushAsync(new BookPage((int) obj));
        }
    }
}
