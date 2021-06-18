using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using library.Pages;
using Xamarin.Forms;

namespace library.ViewModel
{
    class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }


        // Commands
        public ICommand RegisterCommand { get; private set; }
        public ICommand ToMainPageCommand { get; private set; }

        public LoginViewModel()
        {
            RegisterCommand = new Command(RegisterExecute);
            ToMainPageCommand = new Command(ToMainPageExecute);
        }

        private void ToMainPageExecute(object obj)
        {
            App.Navigation.PushAsync(new MainPage());
        }

        private void RegisterExecute(object obj)
        {
            App.Navigation.PushAsync(new RegistrationPage());
        }
    }
}
