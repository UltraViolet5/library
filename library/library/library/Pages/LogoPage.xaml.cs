using System;
using library.Services;
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

            Utils.RemoveUserFromSession();

            NavigationPage.SetHasNavigationBar(this, false);

            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {
                if (Application.Current.Properties.ContainsKey("IsLoggedIn") &&
                    (bool) Application.Current.Properties["IsLoggedIn"])
                {
                    App.CurrentUser = Utils.GetCurrentUser();
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
            if (Navigation.NavigationStack.Count > 1)
            {
                Navigation.RemovePage(this);
            }
        }
    }
}