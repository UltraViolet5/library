using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using library.Pages;

namespace library
{
    public partial class App : Application
    {
        /// <summary>
        /// Static reference to main page
        /// </summary>
        private static Page MainPageInstance { get; set; }

        public static INavigation Navigation => MainPageInstance.Navigation;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LogoPage());
            MainPageInstance = MainPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}