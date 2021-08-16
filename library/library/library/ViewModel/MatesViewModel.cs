using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using library.Model;
using library.Pages;
using Xamarin.Forms;

namespace library.ViewModel
{
    public class MatesViewModel : BaseViewModel
    {
        public IEnumerable<UserViewModel> Mates { get; set; }

        public ICommand ShowBooksCommand { get; private set; }

        public MatesViewModel()
        {
            IEnumerable<User> mates = App.CurrentUser.Friends;
            Mates = mates.Select(u => new UserViewModel(u));

            ShowBooksCommand = new Command(ShowBooksExecute);
        }

        private async void ShowBooksExecute(object booksOwnerId)
        {
            var user = await App.ApiService.GetUser((string)booksOwnerId);

            App.Navigation.PushAsync(new BooksPage(user));
        }
    }
}
