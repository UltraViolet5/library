using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using library.Pages;
using System.Timers;

namespace library
{
    public partial class App : Application
    {

        LogoPage logoPage { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new library.MainPage());
            logoPage = new LogoPage();
            
        }

        protected override void OnStart()
        {
            MainPage.Appearing += MainPage_Appearing;

           
           
        }

        private void MainPage_Appearing(object sender, EventArgs e)
        {
            
            MainPage.Navigation.PushModalAsync(logoPage);
            System.Threading.Thread.Sleep(2000);
            /*System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(10));*/
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
