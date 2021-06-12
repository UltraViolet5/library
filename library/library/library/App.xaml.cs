using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using library.Pages;

namespace library
{
    public partial class App : Application
    {
        public static LogoPage LogoPage { get; set; }
        public static Page FirstPage { get; set; }
       

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new library.MainPage());
            FirstPage = MainPage;
            LogoPage = new LogoPage();
            
           
        }

        /// <summary>
        /// Display LogoPage
        /// </summary>
        protected override void OnStart()
        {
            FirstPage.Navigation.PushModalAsync(LogoPage);
            System.Threading.Thread.Sleep(2000);
            FirstPage.Navigation.PopModalAsync();
        }

      

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}