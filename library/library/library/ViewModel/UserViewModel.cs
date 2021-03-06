using System;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using library.Model;
using Xamarin.Forms;

namespace library.ViewModel
{
    public class UserViewModel : BaseViewModel
    {
        public event EventHandler IsPhotoUpdated;

        public ICommand SaveChangesCommand
        {
            get => _saveChangesCommand;
            set => _saveChangesCommand = value;
        }

        public ICommand AddPhotoCommand
        {
            get => _addPhotoCommand;
            set => _addPhotoCommand = value;
        }

        #region Validations

        public bool UserNameValidation_ShowMsg
        {
            get => _userNameValidationShowMsg;
            set
            {
                _userNameValidationShowMsg = value;
                RaisePropertyChanged(nameof(UserNameValidation_ShowMsg));
            }
        }

        public bool EmailValidation_ShowMsg
        {
            get => _emailValidationShowMsg;
            set
            {
                _emailValidationShowMsg = value;
                RaisePropertyChanged(nameof(EmailValidation_ShowMsg));
            }
        }

        public bool PasswordValidation_ShowMsg
        {
            get => _passwordValidationShowMsg;
            set
            {
                _passwordValidationShowMsg = value;
                RaisePropertyChanged(nameof(PasswordValidation_ShowMsg));
            }
        }

        public bool NewPasswordValidation_ShowMsg
        {
            get => _newPasswordValidationShowMsg;
            set
            {
                _newPasswordValidationShowMsg = value;
                RaisePropertyChanged(nameof(NewPasswordValidation_ShowMsg));
                RaisePropertyChanged(nameof(PasswordConfirmValidation_ShowMsg));
            }
        }

        public bool PasswordConfirmValidation_ShowMsg
        {
            get => _passwordConfirmationValidationShowMsg;
            set
            {
                _passwordConfirmationValidationShowMsg = value;
                RaisePropertyChanged(nameof(PasswordConfirmValidation_ShowMsg));
            }
        }

        public bool LocalizationValidation_ShowMsg
        {
            get => _localizationValidationShowMsg;
            set
            {
                _localizationValidationShowMsg = value;
                RaisePropertyChanged(nameof(LocalizationValidation_ShowMsg));
            }
        }

        public bool DataUpdated_ShowMsg
        {
            get => _dataUpdatedShowMsg;
            set
            {
                _dataUpdatedShowMsg = value;
                RaisePropertyChanged(nameof(DataUpdated_ShowMsg));
            }
        }

        #endregion

        #region Private fields

        private readonly User _user;
        private readonly ContentPage _parentReference;
        private string _password;
        private string _newPassword;
        private string _confirmPassword;
        private bool _userNameValidationShowMsg;
        private bool _emailValidationShowMsg;
        private bool _passwordValidationShowMsg;
        private bool _newPasswordValidationShowMsg;
        private bool _passwordConfirmationValidationShowMsg;
        private bool _localizationValidationShowMsg;
        private bool _dataUpdatedShowMsg;
        private ICommand _saveChangesCommand;
        private ICommand _addPhotoCommand;
        private object _photo;
        private bool _addPhotoIsEnabled = true;
        private ImageSource _photoSource;

        #endregion

        public UserViewModel(User user)
        {
            _user = user;

            SaveChangesCommand = new Command(SaveChangesExecute, SaveChangesCanExecute);
            AddPhotoCommand = new Command(AddPhotoExecute);
        }

        public string UserName
        {
            get => _user.UserName;
            set
            {
                _user.UserName = value;
                UserNameValidation();
                RaisePropertyChanged(nameof(UserName));
            }
        }

        public string Email
        {
            get => _user.Email;
            set
            {
                if (value == _user.Email) return;

                _user.Email = value;
                EmailValidation();
                RaisePropertyChanged(nameof(Email));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                PasswordValidation();
                RaisePropertyChanged(nameof(Password));
            }
        }

        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                NewPasswordValidation();
                ConfirmPasswordValidation();
                PasswordValidation();
                RaisePropertyChanged(nameof(NewPassword));
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                NewPasswordValidation();
                ConfirmPasswordValidation();
                PasswordValidation();
                RaisePropertyChanged(nameof(ConfirmPassword));
            }
        }

        public DateTime BirthDate
        {
            get => _user.BirthDate;
            set
            {
                _user.BirthDate = value;
                BirthDateValidation();
                RaisePropertyChanged(nameof(BirthDate));
            }
        }

        public string Localization
        {
            get => _user.Localization;
            set
            {
                _user.Localization = value;
                LocalizationValidation();
                RaisePropertyChanged(nameof(Localization));
            }
        }

        public int BooksCount => App.DbService.GetBooks()
            .Count(b => b.Owner.Email == _user.Email);

        public string Id => _user.Id;

        public byte[] Photo
        {
            get => _user.Photo;
            set
            {
                _user.Photo = value;
                RaisePropertyChanged(nameof(PhotoSource));
                if (IsPhotoUpdated != null)
                {
                    IsPhotoUpdated(this, EventArgs.Empty);
                }
            }
        }

        public ImageSource PhotoSource
        {
            get
            {
                var photoSource = Utils.BytesToImageSource(Photo);
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

        public bool AddPhotoIsEnabled
        {
            get => _addPhotoIsEnabled;
            set
            {
                _addPhotoIsEnabled = value;
                RaisePropertyChanged(nameof(AddPhotoIsEnabled));
            }
        }

        private async void AddPhotoExecute(object obj)
        {
            AddPhotoIsEnabled = false;

            Photo = await Utils.TakePhoto();

            AddPhotoIsEnabled = true;
        }

        private bool SaveChangesCanExecute()
        {
            if (EmailValidation() &
                PasswordValidation() &
                NewPasswordValidation() &
                ConfirmPasswordValidation() &
                UserNameValidation() &
                BirthDateValidation() &
                LocalizationValidation())
            {
                return true;
            }

            return false;
        }

        private void SaveChangesExecute()
        {
            if (SaveChangesCanExecute())
            {
                Console.WriteLine("Save Changes Execute!");
                App.CurrentUser.BirthDate = BirthDate;
                App.CurrentUser.UserName = UserName;
                App.CurrentUser.Email = Email;
                App.CurrentUser.Localization = Localization;

                if (!String.IsNullOrWhiteSpace(NewPassword) &&
                    BCrypt.Net.BCrypt.Verify(Password, App.CurrentUser.PasswordHash))
                {
                    App.CurrentUser.BirthDate = BirthDate;
                    App.CurrentUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(NewPassword);
                }

                App.DbService.UpdateUser(App.CurrentUser);

                DataUpdated_ShowMsg = true;
                Device.StartTimer(TimeSpan.FromSeconds(3), () =>
                {
                    DataUpdated_ShowMsg = false;

                    return false;
                });
            }
        }

        private bool UserNameValidation()
        {
            bool result = !String.IsNullOrWhiteSpace(UserName);

            UserNameValidation_ShowMsg = !result;

            return result;
        }

        private bool EmailValidation()
        {
            bool result = !String.IsNullOrWhiteSpace(Email);
            EmailValidation_ShowMsg = !result;

            return result;
        }

        private bool PasswordValidation()
        {
            if (String.IsNullOrWhiteSpace(NewPassword))
                return true;

            bool result = !String.IsNullOrWhiteSpace(Password);

            PasswordValidation_ShowMsg = !result;

            return result;
        }

        private bool NewPasswordValidation()
        {
            if (String.IsNullOrWhiteSpace(NewPassword))
                return true;

            bool result = !String.IsNullOrWhiteSpace(NewPassword);

            NewPasswordValidation_ShowMsg = !result;

            return result;
        }

        private bool ConfirmPasswordValidation()
        {
            if (String.IsNullOrWhiteSpace(NewPassword))
                return true;

            bool result = NewPassword == ConfirmPassword;

            PasswordConfirmValidation_ShowMsg = !result;

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