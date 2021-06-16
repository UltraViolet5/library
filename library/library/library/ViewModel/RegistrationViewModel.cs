using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using library.Pages;
using Xamarin.Forms;

namespace library.ViewModel
{
    class RegistrationViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Localization { get; set; }
        public bool TermsAccepted { get; set; }

        public LoginPage LoginPage { get; set; }

        public ICommand RegisterCommand { get; private set; }

        public RegistrationViewModel()
        {
            LoginPage = new LoginPage();

            RegisterCommand = new Command(RegisterExecute);
        }

        private void RegisterExecute()
        {
            App.FirstPage.Navigation.PushAsync(LoginPage);
        }
    }
}