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
    public partial class RegistrationPage : ContentPage
    {
        private readonly RegistrationViewModel _registrationViewModel;
        public RegistrationPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            _registrationViewModel = new RegistrationViewModel();

            BindingContext = _registrationViewModel;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (App.Navigation.NavigationStack.Count > 1)
            {
                Navigation.RemovePage(this);
            }
        }
    }
}