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

        // Pages
        private RegistrationPage RegistrationPage { get; set; }

        // Commands
        public ICommand RegisterCommand { get; private set; }

        public LoginViewModel()
        {
            RegistrationPage = new RegistrationPage();

            RegisterCommand = new Command(RegisterExecute);
        }

        private void RegisterExecute(object obj)
        {
            App.FirstPage.Navigation.PushAsync(RegistrationPage);
        }
    }
}
