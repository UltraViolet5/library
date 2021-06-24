using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace library.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogoPage : ContentPage
    {
        public LogoPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            Device.StartTimer(TimeSpan.FromSeconds(3), () =>
            {
                if (Application.Current.Properties.ContainsKey("IsLoggedIn") &&
                    (bool) Application.Current.Properties["IsLoggedIn"])
                {
                    Navigation.PushAsync(new MainPage());
                }
                else
                    Navigation.PushAsync(new LoginPage());

                return false;
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Navigation.RemovePage(this);
        }
    }
}