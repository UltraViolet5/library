using System;
using System.Threading.Tasks;
using library.Model;
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

            NavigationPage.SetHasNavigationBar(this, false);

            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {
                if (Application.Current.Properties.ContainsKey("IsLoggedIn") &&
                    (bool) Application.Current.Properties["IsLoggedIn"])
                {
                   InitMainPageAsync();
                }
                else
                    Navigation.PushAsync(new LoginPage());

                return false;
            });
        }

        private async void InitMainPageAsync()
        {
            var userId = (string)App.Current.Properties["UserId"];
            var result = await App.ApiService.GetUser(userId);
            App.CurrentUser = result;
            App.CurrentUser.Friends = await App.ApiService.GetUserFriends(App.CurrentUser.Id);
            Navigation.PushAsync(new MainPage());
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