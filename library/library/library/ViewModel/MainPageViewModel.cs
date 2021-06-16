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
    public class MainPageViewModel : BaseViewModel
    {
        private BooksPage BooksPage { get; set; }
        private AddBookPage AddBookPage { get; set; }
        private LoginPage LoginPage { get; set; }
        private MatesPage MatesPage { get; set; }
        private MyRentalsPage MyRentalsPage { get; set; }
        private RegistrationPage RegistrationPage { get; set; }
        private RentedPage RentedPage { get; set; }
        private SettingsPage SettingsPage { get; set; }
        private UserPage UserPage { get; set; }
        private BooksByCategoryPage BooksByCategoryPage { get; set; }
        private BookPage BookPage { get; set; }

        public IEnumerable<BookViewModel> Books { get; private set; }
        public IEnumerable<CategoryViewModel> Categories { get; private set; }
        public IEnumerable<UserViewModel> Mates { get; private set; }
        public IEnumerable<BorrowingViewModel> Borrowings { get; private set; }


        private DBService _dbService;

        public ICommand BtnLogin { get; private set; }
        public ICommand RegisterCommand { get; private set; }
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

        public MainPageViewModel()
        {
            // Data init
            _dbService = new DBService();
            
            Books = _dbService.GetBooks().Select(b => new BookViewModel(b));
            Categories = _dbService.GetCategories().Select(c => new CategoryViewModel(c));
            Mates = _dbService.GetMates().Select(m => new UserViewModel(m));
            Borrowings = _dbService.GetBorrowings().Select(b => new BorrowingViewModel(b));

            // Page init
            BooksPage = new BooksPage();
            LoginPage = new LoginPage();
            MatesPage = new MatesPage();
            MyRentalsPage = new MyRentalsPage();
            RegistrationPage = new RegistrationPage();
            RentedPage = new RentedPage();
            SettingsPage = new SettingsPage();
            UserPage = new UserPage();
            BooksByCategoryPage = new BooksByCategoryPage();
            BookPage = new BookPage();
            AddBookPage = new AddBookPage();

            // Actions init
            BtnLogin = new Command(LoginBtnExecute);
            BooksLbTapped = new Command(TappedExecute);
            BtnUserView = new Command(UserViewImageTappedExecute);
            ShowBooksByCategory = new Command(ShowBooksByCategoryExecute);
            ShowBook = new Command(ShowBookExecute);
            AddBook = new Command(AddBookExecute);
            RegisterCommand = new Command(RegisterExecute);

        }

        private bool ShowBookCanExecute(object arg)
        {
            return true;
        }

        private void ShowBookExecute(object obj)
        {
            App.FirstPage.Navigation.PushAsync(BookPage);
        }

        private bool ShowBooksByCategoryCanExecute(object arg)
        {
            return true;
        }

        private void ShowBooksByCategoryExecute(object obj)
        {
            App.FirstPage.Navigation.PushAsync(BooksByCategoryPage);
        }

        private bool UserViewImageCanExecute(object arg)
        {
            return true;
        }

        private void UserViewImageTappedExecute(object obj)
        {
            App.FirstPage.Navigation.PushAsync(UserPage);
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

        private void AddBookExecute(object arg)
        {
            App.FirstPage.Navigation.PushAsync(AddBookPage);
        }

        private void RegisterExecute()
        {
            App.FirstPage.Navigation.PushAsync(RegistrationPage);
        }
    }
}