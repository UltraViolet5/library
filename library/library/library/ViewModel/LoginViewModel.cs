using System;
using System.Linq;
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

        private bool _loginValidationShowMsg = false;

        public bool LoginValidation_ShowMsg
        {
            get { return _loginValidationShowMsg; }
            set
            {
                _loginValidationShowMsg = value;
                RaisePropertyChanged(nameof(LoginValidation_ShowMsg));
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
            bool canExecute = !string.IsNullOrWhiteSpace(Email)
                              && !string.IsNullOrEmpty(Password);

            if (!canExecute)
                LoginValidation_ShowMsg = true;

            return canExecute;
            
        }

        private async void LoginExecute(object obj)
        {
            var user = await App.ApiService.GetUserByEmail(Email);
            if (user == null)
            {
                LoginValidation_ShowMsg = true;
                return;
            }

            bool verification = global::BCrypt.Net.BCrypt.Verify(Password, user.PasswordHash);
            if (verification)
            {
                Utils.SaveUserInSession(user);
                await App.Navigation.PushAsync(new MainPage());
            }
            else
            {
                LoginValidation_ShowMsg = true;
            }
        }

        private void ToRegistrationExecute(object obj)
        {
            App.Navigation.PushAsync(new RegistrationPage());
        }
    }
}