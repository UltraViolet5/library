using System;
using System.Collections.Generic;
using System.Text;
using library.Model;

namespace library.ViewModel
{
    public class UserViewModel : BaseViewModel
    {
        private readonly User _user;

        public UserViewModel(User user)
        {
            _user = user;
        }

        public string Email
        {
            get => _user.Email;
            set
            {
                if (value == _user.Email) return;

                _user.Email = value;
                RaisePropertyChanged(nameof(Email));
            }
        }
    }
}