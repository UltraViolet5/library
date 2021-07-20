using library.Model;
using library.Pages;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace library.ViewModel
{
    public class MyRentalsViewModel : BaseViewModel
    {
        public IEnumerable<Borrowing> Borrowings { get; set; }

        public ICommand ShowBorrowingCommand { get; private set; }

        public MyRentalsViewModel()
        {
            Borrowings = App.DbService.GetBorrowings();

            ShowBorrowingCommand = new Command(BorrowingExecute);
        }

        private void BorrowingExecute(object obj)
        {
            App.Navigation.PushAsync(new BorrowingPage((Borrowing) obj));
        }
    }
}