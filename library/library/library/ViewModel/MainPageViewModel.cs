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

            // Actions init
            BooksLbTapped = new Command(TappedExecute);
            BtnUserView = new Command(UserViewImageTappedExecute);
            ShowBooksByCategory = new Command(ShowBooksByCategoryExecute);
            ShowBook = new Command(ShowBookExecute);
            AddBook = new Command(AddBookExecute);

        }
        
        private void ShowBookExecute(object obj)
        {
            App.Navigation.PushAsync(new BookPage());
        }
        
        private void ShowBooksByCategoryExecute(object obj)
        {
            App.Navigation.PushAsync(new BooksByCategoryPage());
        }

        private void UserViewImageTappedExecute(object obj)
        {
            App.Navigation.PushAsync(new UserPage());
        }

        private void TappedExecute(object arg)
        {
            App.Navigation.PushAsync(new BookPage());
        }
        
        private bool LoginBtnCanExecute(object arg)
        {
            return true;
        }

        private void AddBookExecute(object arg)
        {
            App.Navigation.PushAsync(new AddBookDataPage());
        }
    }
}