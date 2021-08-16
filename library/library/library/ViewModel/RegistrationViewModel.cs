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
    class RegistrationViewModel : BaseViewModel
    {
        // Model
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                EmailValidation();
                RaisePropertyChanged(nameof(EmailValidation_ShowMsg));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                PasswordValidation();
                RaisePropertyChanged(nameof(PasswordValidation_ShowMsg));
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                ConfirmPasswordValidation();
                RaisePropertyChanged(nameof(PasswordConfirmValidation_ShowMsg));
            }
        }

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                UserNameValidation();
                RaisePropertyChanged(nameof(UserNameValidation_ShowMsg));
            }
        }

        public DateTime BirthDate
        {
            get => _birthDate;
            set => _birthDate = value;
        }

        public string Localization
        {
            get => _localization;
            set
            {
                _localization = value;
                LocalizationValidation();
                RaisePropertyChanged(nameof(LocalizationValidation_ShowMsg));
            }
        }

        public bool TermsAccepted
        {
            get => _termsAccepted;
            set
            {
                _termsAccepted = value;
                TermsValidation();
                RaisePropertyChanged(nameof(TermsValidation_ShowMsg));
            }
        }


        #region Validations
        public bool EmailValidation_ShowMsg
        {
            get { return _emailValidation_ShowMsg; }
            set
            {
                _emailValidation_ShowMsg = value;
                RaisePropertyChanged(nameof(EmailValidation_ShowMsg));
            }
        }


        public bool PasswordValidation_ShowMsg
        {
            get { return _passwordValidation_ShowMsg; }
            set
            {
                _passwordValidation_ShowMsg = value;
                RaisePropertyChanged(nameof(PasswordValidation_ShowMsg));
            }
        }


        public bool PasswordConfirmValidation_ShowMsg
        {
            get { return _passwordConfirmValidation_ShowMsg; }
            set
            {
                _passwordConfirmValidation_ShowMsg = value;
                RaisePropertyChanged(nameof(ConfirmPasswordValidation));
            }
        }


        public bool UserNameValidation_ShowMsg
        {
            get { return _userNameValidation_ShowMsg; }
            set
            {
                _userNameValidation_ShowMsg = value;
                RaisePropertyChanged(nameof(UserNameValidation_ShowMsg));
            }
        }


        public bool LocalizationValidation_ShowMsg
        {
            get { return _localizationValidation_ShowMsg; }
            set
            {
                _localizationValidation_ShowMsg = value;
                RaisePropertyChanged(nameof(LocalizationValidation_ShowMsg));
            }
        }

        public bool TermsValidation_ShowMsg
        {
            get => _termsValidationShowMsg;
            set
            {
                _termsValidationShowMsg = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        // Commands
        public ICommand RegisterCommand => _registerCommand;

        public ICommand BackToLoginCommand => _backToLoginCommand;


        #region Private fields

        private bool _termsValidationShowMsg;
        private string _email;
        private string _password;
        private string _confirmPassword;
        private string _userName;
        private DateTime _birthDate;
        private string _localization;
        private bool _termsAccepted;
        private readonly ICommand _registerCommand;
        private readonly ICommand _backToLoginCommand;
        private bool _emailValidation_ShowMsg = false;
        private bool _passwordValidation_ShowMsg;
        private bool _localizationValidation_ShowMsg;
        private bool _userNameValidation_ShowMsg;
        private bool _passwordConfirmValidation_ShowMsg;

        #endregion


        public RegistrationViewModel()
        {
            _registerCommand = new Command(RegisterExecute, RegisterCanExecute);
            _backToLoginCommand = new Command(BackToLoginExecute);
        }

        private bool RegisterCanExecute()
        {
            if (EmailValidation() &
                PasswordValidation() &
                ConfirmPasswordValidation() &
                UserNameValidation() &
                BirthDateValidation() &
                LocalizationValidation() &
                TermsValidation())
            {
                return true;
            }

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
            App.ApiService.AddUser(user);
            Console.WriteLine(user.PasswordHash);
            App.Navigation.PushAsync(new LoginPage());
        }

        private bool TermsValidation()
        {
            TermsValidation_ShowMsg = !TermsAccepted;

            return TermsAccepted;
        }

        private bool UserNameValidation()
        {
            bool result = !String.IsNullOrWhiteSpace(UserName);

            UserNameValidation_ShowMsg = !result;

            return result;
        }

        private bool PasswordValidation()
        {
            bool result = !String.IsNullOrWhiteSpace(Password);

            PasswordValidation_ShowMsg = !result;

            return result;
        }

        private bool ConfirmPasswordValidation()
        {
            bool result = Password == ConfirmPassword;
            PasswordConfirmValidation_ShowMsg = !result;

            return result;
        }

        private bool EmailValidation()
        {
            bool result = !String.IsNullOrWhiteSpace(Email);
            EmailValidation_ShowMsg = !result;

            return result;
        }

        private bool BirthDateValidation()
        {
            return true;
        }

        private bool LocalizationValidation()
        {
            bool result = !String.IsNullOrWhiteSpace(Localization);
            LocalizationValidation_ShowMsg = !result;

            return result;
        }
    }
}