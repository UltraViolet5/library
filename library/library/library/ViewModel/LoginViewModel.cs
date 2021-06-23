using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using library.Model;
using library.Pages;
using Xamarin.Forms;
using BCrypt = BCrypt.Net.BCrypt;

namespace library.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
        private string _email;

        public string Email
        {
            get => _email;
            set
            {
                if (_email == value) return;

                _email = value;
                RaisePropertyChanged(nameof(Email));
            }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                if (_password == value) return;

                _password = value;
                RaisePropertyChanged(nameof(Password));
            }
        }


        // Commands
        public ICommand RegisterCommand { get; private set; }
        public ICommand ToMainPageCommand { get; private set; }

        public LoginViewModel()
        {
            RegisterCommand = new Command(ToRegistrationExecute);
            ToMainPageCommand = new Command(LoginExecute, LoginCanExecute);
        }

        private bool LoginCanExecute(object arg)
        {
            return !string.IsNullOrWhiteSpace(Email)
                && !string.IsNullOrEmpty(Password);
        }

        private void LoginExecute(object obj)
        {
            User user = App.DbService.GetUser(Email);
            if (user == null)
                return;
            
            if (global::BCrypt.Net.BCrypt.Verify(Password, user.PasswordHash))
            {
                Console.WriteLine("Logged! Correct password");
                App.Navigation.PushAsync(new MainPage());
            }
        }

        private void ToRegistrationExecute(object obj)
        {
            App.Navigation.PushAsync(new RegistrationPage());
        }
    }
}