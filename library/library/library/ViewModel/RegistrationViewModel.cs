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

        public ICommand RegisterCommand { get; private set; }
        public ICommand BackToLoginCommand{ get; private set; }

        public RegistrationViewModel()
        {
            RegisterCommand = new Command(RegisterExecute);
            BackToLoginCommand = new Command(BackToLoginExecute);
        }

        private void BackToLoginExecute()
        {
            App.Navigation.PushAsync(new LoginPage());
        }

        private void RegisterExecute()
        {
            // TODO Register user
            App.Navigation.PushAsync(new LoginPage());
        }
    }
}