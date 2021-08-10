using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using library.Model;
using library.Pages;
using Xamarin.Forms;

namespace library.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        public ImageSource PhotoSource
        {
            get
            {
                var photoSource = Utils.BytesToImageSource(App.CurrentUser.Photo);

                if (photoSource == null)
                {
                    // If user don't have photo load photo from global resources
                    var assembly = this.GetType().GetTypeInfo().Assembly;
                    var data = Utils.ImageDataFromResource("library.Resources.user.png", assembly);
                    return Utils.BytesToImageSource(data);
                }

                return photoSource;
            }
        }

        public UserPage _userPage { get; private set; }
        public IEnumerable<BookViewModel> Books { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public IEnumerable<UserViewModel> Mates { get; set; }
        public IEnumerable<BorrowingViewModel> Borrowings { get; set; }

        public ICommand UserCommand { get; private set; }
        public ICommand MatesCommand { get; private set; }
        public ICommand RentalsCommand { get; private set; }
        public ICommand ShowBorrowingCommand { get; private set; }
        public ICommand SettingsCommand { get; private set; }
        public ICommand BooksCommand { get; private set; }
        public ICommand AddBookCommand { get; private set; }
        public ICommand ShowBooksByCategory { get; private set; }
        public ICommand ShowBookCommand { get; private set; }
        public ICommand AddMateCommand { get; set; }


        public MainPageViewModel()
        {
            // Actions init
            BooksCommand = new Command(BooksExecute);
            UserCommand = new Command(UserViewImageTappedExecute);
            ShowBookCommand = new Command(ShowBookExecute);
            AddBookCommand = new Command(AddBookExecute);
            SettingsCommand = new Command(SettingsExecute);
            MatesCommand = new Command(MatesExecute);
            RentalsCommand = new Command(RentalsExecute);
            ShowBorrowingCommand = new Command(ShowBorrowingExecute);
            AddMateCommand = new Command(AddMateExecute);

            _userPage = new UserPage();
            _userPage.UserViewModel.IsPhotoUpdated += HandlePhotoUpdated;
        }
        
        private void AddMateExecute()
        {
            App.Navigation.PushAsync(new AddMatePage());
        }

        private void ShowBorrowingExecute(object obj)
        {
            App.Navigation.PushAsync(new BorrowingPage((Borrowing)obj));
        }

        private void HandlePhotoUpdated(object sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(PhotoSource));
        }

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
            App.Navigation.PushAsync(_userPage);
        }

        private void BooksExecute(object arg)
        {
            App.Navigation.PushAsync(new BooksPage(App.CurrentUser, true));
        }

        private void AddBookExecute(object arg)
        {
            App.Navigation.PushAsync(new AddBookDataPage());
        }
    }
}