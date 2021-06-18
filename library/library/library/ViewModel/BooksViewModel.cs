using library.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace library.ViewModel
{
    public class BooksViewModel : BaseViewModel
    {

        public ICommand AddBookCommand { get; private set; }


        public BooksViewModel()
        {
            AddBookCommand = new Command(AddBookExecute);
        }

        private void AddBookExecute()
        {
            App.Navigation.PushAsync(new AddBookPage());
        }
    }
}
