using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using library.Daos.Implementations;
using library.Infrastructure;
using library.Pages;
using library.Services;
using Xamarin.Forms;

namespace library.ViewModel
{
    class MainViewModel : BaseViewModel
    {

        Books BooksPage { get; set; }
        Login LoginPage { get; set; }
     /* Mates Mates { get; set; }
        MyRentals MyRentals { get; set; }
        Registration Register { get; set; }
        Rented Rented { get; set; }
        Settings Settings { get; set; }
        UserView UserView { get; set; }*/

        public IEnumerable<BookViewModel> Books { get; private set; }
        public IEnumerable<CategoryViewModel> Categories { get; private set; }

        private DBService _dbService;

        Login Login = new Login();

        public ICommand BtnLogin { get; private set; }
        public ICommand BtnRegister { get; private set; }
        public ICommand BtnUserView { get; private set; }
        public ICommand BtnBooks { get; private set; }
        public ICommand BtnMates { get; private set; }
        public ICommand BtnMyRentals { get; private set; }
        public ICommand BtnRented { get; private set; }
        public ICommand BtnSettings { get; private set; }
        public ICommand BooksLbTapped { get; private set; }

        public MainViewModel()
        {
            _dbService = new DBService();

            var seeder = new LibrarySeeder(_dbService);
            seeder.Seed();

            Books = _dbService.GetBooks().Select(b => new BookViewModel(b));
            Categories = _dbService.GetCategories().Select(c => new CategoryViewModel(c));

            BooksPage = new Books();
            LoginPage = new Login();
            /*Mates = new Mates();
            MyRentals = new MyRentals();
            Register = new Registration();
            Rented = new Rented();
            Settings = new Settings();
            UserView = new UserView();*/

            BtnLogin = new Command(LoginBtnExecute, LoginBtnCanExecute);
            BooksLbTapped = new Command(BooksLbTappedExecute, BooksLbTappedCanExecute);
        }



        private void BooksLbTappedExecute(object obj)
        {
            App.FirstPage.Navigation.PushAsync(BooksPage);
        }

        private bool BooksLbTappedCanExecute(object arg)
        {
            return true;
        }

        private void LoginBtnExecute(object obj)
        {
            App.FirstPage.Navigation.PushAsync(LoginPage);
        }

        private bool LoginBtnCanExecute(object arg)
        {
            return true;
        }
    }
}