using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using library.Pages;

namespace library
{
    public partial class App : Application
    {
        LogoPage LogoPage { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new library.MainPage());
            LogoPage = new LogoPage();
        }

        protected override void OnStart()
        {
            MainPage.Appearing += MainPage_Appearing;
        }

        private void MainPage_Appearing(object sender, EventArgs e)
        {
            MainPage.Navigation.PushModalAsync(LogoPage);
            System.Threading.Thread.Sleep(2000);
            MainPage.Navigation.PopModalAsync();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}