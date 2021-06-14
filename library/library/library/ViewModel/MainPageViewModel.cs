using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using library.Pages;
using library.Services;
using Xamarin.Forms;

namespace library.ViewModel
{
    class MainViewModel : BaseViewModel
    {

        Books BooksPage { get; set; }
        Login LoginPage { get; set; }
        Mates Mates { get; set; }
        MyRentals MyRentals { get; set; }
        Registration Register { get; set; }
        Rented Rented { get; set; }
        Settings Settings { get; set; }
        UserView UserView { get; set; }
        BooksByCategory BooksByCategory { get; set; }
        Book Book { get; set; }

        public IEnumerable<BookViewModel> Books { get; private set; }
        public IEnumerable<CategoryViewModel> Categories { get; private set; }

        private DBService _dbService;

        public ICommand BtnLogin { get; private set; }
        public ICommand BtnRegister { get; private set; }
        public ICommand BtnUserView { get; private set; }
        public ICommand BtnBooks { get; private set; }
        public ICommand BtnMates { get; private set; }
        public ICommand BtnMyRentals { get; private set; }
        public ICommand BtnRented { get; private set; }
        public ICommand BtnSettings { get; private set; }
        public ICommand BooksLbTapped { get; private set; }
        public ICommand AddBook { get; private set; }
        public ICommand ShowBooksByCategory { get; private set; }
        public ICommand ShowBook { get; private set; }

        public MainViewModel()
        {
            _dbService = new DBService();
            
            Books = _dbService.GetBooks().Select(b => new BookViewModel(b));
            Categories = _dbService.GetCategories().Select(c => new CategoryViewModel(c));

            BooksPage = new Books();
            LoginPage = new Login();
            Mates = new Mates();
            MyRentals = new MyRentals();
            Register = new Registration();
            Rented = new Rented();
            Settings = new Settings();
            UserView = new UserView();
            BooksByCategory = new BooksByCategory();
            Book = new Book();

            BtnLogin = new Command(LoginBtnExecute, LoginBtnCanExecute);
            BooksLbTapped = new Command(TappedExecute, BooksLbTappedCanExecute);
            BtnUserView = new Command(UserViewImageTappedExecute, UserViewImageCanExecute);
            ShowBooksByCategory = new Command(ShowBooksByCategoryExecute, ShowBooksByCategoryCanExecute);
            ShowBook = new Command(ShowBookExecute, ShowBookCanExecute);
            //BtnUserView = new Command(UserViewImageTappedExecute, UserViewImageCanExecute);
            //BtnUserView = new Command(UserViewImageTappedExecute, UserViewImageCanExecute);
            //BtnUserView = new Command(UserViewImageTappedExecute, UserViewImageCanExecute);
            //BtnUserView = new Command(UserViewImageTappedExecute, UserViewImageCanExecute);

        }

        private bool ShowBookCanExecute(object arg)
        {
            return true;
        }

        private void ShowBookExecute(object obj)
        {
            App.FirstPage.Navigation.PushAsync(Book);
        }

        private bool ShowBooksByCategoryCanExecute(object arg)
        {
            return true;
        }

        private void ShowBooksByCategoryExecute(object obj)
        {
            App.FirstPage.Navigation.PushAsync(BooksByCategory);
        }

        private bool UserViewImageCanExecute(object arg)
        {
            return true;
        }

        private void UserViewImageTappedExecute(object obj)
        {
            App.FirstPage.Navigation.PushAsync(UserView);
        }

        private void TappedExecute(object arg)
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