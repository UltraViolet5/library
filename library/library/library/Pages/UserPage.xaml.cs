using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using library.FactoryMethod;
using library.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace library.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentPage
    {
        private readonly UserViewModel _userViewModel;

        public UserPage()
        {
            InitializeComponent();

            var currentUser = App.DbService.GetUser((string)Application.Current.Properties["UserEmail"]);
            _userViewModel = new UserViewModel(currentUser);
            BindingContext = _userViewModel;

            Content = App.PageFactory.GetUserPage();

            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}