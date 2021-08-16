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
        public UserViewModel UserViewModel { get; private set; }
        public UserPage()
        {
            InitializeComponent();

            InitData();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void InitData()
        {
            UserViewModel = new UserViewModel(App.CurrentUser);
            BindingContext = UserViewModel;

            Content = App.PageFactory.GetUserPage();
        }
    }
}