using System.Linq;
using System.Windows.Input;
using library.Pages;
using Xamarin.Forms;

namespace library.ViewModel
{
    class SettingsViewModel
    {
        public ICommand LogoutCommand { get; set; }

        public SettingsViewModel()
        {
            LogoutCommand = new Command(LogoutExecute);
        }

        private void LogoutExecute()
        {
            Utils.RemoveUserFromSession();
            App.Navigation.InsertPageBefore(new LoginPage(), App.Navigation.NavigationStack.First());
            App.Navigation.PopToRootAsync();
        }
    }
}
