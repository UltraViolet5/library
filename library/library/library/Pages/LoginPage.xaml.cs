using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using library.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace library.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly LoginViewModel _loginViewModel;

        public LoginPage()
        {
            InitializeComponent();

            _loginViewModel = new LoginViewModel();

            BindingContext = _loginViewModel;
        }

        protected override bool OnBackButtonPressed()
        {
            App.FirstPage.Navigation.RemovePage(this);
            return base.OnBackButtonPressed();
        }
    }
}