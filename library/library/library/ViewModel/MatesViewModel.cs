using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using library.Model;
using Xamarin.Forms;

namespace library.ViewModel
{
    public class MatesViewModel : BaseViewModel
    {
        public IEnumerable<UserViewModel> Mates { get; set; }

        public ICommand ShowMateCommand { get; private set; }

        public MatesViewModel()
        {
            IEnumerable<User> mates = App.CurrentUser.Friends;
            Mates = mates.Select(u => new UserViewModel(u));

            ShowMateCommand = new Command(ShowMateExecute);
        }

        private void ShowMateExecute(object property)
        {
            throw new NotImplementedException();
        }
    }
}
