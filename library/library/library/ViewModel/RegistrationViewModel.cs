using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using library.Model;
using library.Pages;
using Xamarin.Forms;
using BCrypt;

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
            RegisterCommand = new Command(RegisterExecute, RegisterCanExecute);
            BackToLoginCommand = new Command(BackToLoginExecute);
        }

        private bool RegisterCanExecute()
        {
            if (EmailValidation() &&
                PasswordValidation() &&
                UserNameValidation() &&
                BirthDateValidation() &&
                LocalizationValidation() &&
                TermsAccepted)
            {
                Utils.WriteLine("Data is valid!", ConsoleColor.DarkGreen);
                return true;
            }

            Utils.WriteLine("Data is invalid!", ConsoleColor.DarkRed);
            return false;
        }
        
        private void BackToLoginExecute()
        {
            App.Navigation.PushAsync(new LoginPage());
        }

        private void RegisterExecute()
        {
            User user = new User()
            {
                Email = this.Email,
                UserName = this.UserName,
                Localization = this.Localization,
                BirthDate = this.BirthDate,
                PasswordHash = global::BCrypt.Net.BCrypt.HashPassword(this.Password),

            };
            App.DbService.AddUser(user);
            Console.WriteLine(user.PasswordHash);
            App.Navigation.PushAsync(new LoginPage());
        }

        private bool UserNameValidation()
        {
            return !String.IsNullOrWhiteSpace(UserName);
        }

        private bool PasswordValidation()
        {
            return
                !String.IsNullOrWhiteSpace(Password) &&
                Password == ConfirmPassword;
        }

        private bool EmailValidation()
        {
            return !String.IsNullOrWhiteSpace(Email);
        }

        private bool BirthDateValidation()
        {
            return true;
        }

        private bool LocalizationValidation()
        {
            return !String.IsNullOrWhiteSpace(Localization);
        }
    }
}