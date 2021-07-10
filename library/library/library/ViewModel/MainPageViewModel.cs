using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using library.Model;
using library.Pages;
using library.Services;
using Xamarin.Forms;

namespace library.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        public IEnumerable<BookViewModel> Books { get; private set; }
        public IEnumerable<string> Categories { get; private set; }
        public IEnumerable<UserViewModel> Mates { get; private set; }
        public IEnumerable<BorrowingViewModel> Borrowings { get; private set; }

        public ICommand UserCommand { get; private set; }
        public ICommand MatesCommand { get; private set; }
        public ICommand RentalsCommand { get; private set; }
        public ICommand RentedCommand { get; private set; }
        public ICommand SettingsCommand { get; private set; }
        public ICommand BooksCommand { get; private set; }
        public ICommand AddBookCommand { get; private set; }
        public ICommand ShowBooksByCategory { get; private set; }
        public ICommand ShowBookCommand { get; private set; }
        

        public MainPageViewModel()
        {
            // Data init
            Books = App.DbService.GetBooks()
                .Where(b => b.Owner.Email == (string) App.Current.Properties["UserEmail"])
                .Take(2)
                .Select(b => new BookViewModel(b));
            Categories = Enum.GetNames(typeof(Category));
            Mates = App.CurrentUser.Friends
                .Select(m => new UserViewModel(m));
            Borrowings = App.DbService.GetBorrowings()
                .Select(b => new BorrowingViewModel(b));

            // Actions init
            BooksCommand = new Command(BooksExecute);
            UserCommand = new Command(UserViewImageTappedExecute);
            ShowBookCommand = new Command(ShowBookExecute);
            AddBookCommand = new Command(AddBookExecute);
            SettingsCommand = new Command(SettingsExecute);
            MatesCommand = new Command(MatesExecute);
            RentalsCommand = new Command(RentalsExecute);
            
            
        }

     


        //wszystko co asynch powinno byc wywołane z awaitem
        private void RentalsExecute()
        {
            App.Navigation.PushAsync(new MyRentalsPage());
        }

        private void MatesExecute()
        {
            App.Navigation.PushAsync(new MatesPage());
        }
        

        private void SettingsExecute()
        {
            App.Navigation.PushAsync(new SettingsPage());
        }

        private void ShowBookExecute(object obj)
        {
            App.Navigation.PushAsync(new BookPage((int) obj));
        }
        
        private void UserViewImageTappedExecute(object obj)
        {
            App.Navigation.PushAsync(new UserPage());
        }

        private void BooksExecute(object arg)
        {
            var booksOwner = App.CurrentUser;
            App.Navigation.PushAsync(new BooksPage(booksOwner));
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