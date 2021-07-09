using System;
using library.FactoryMethod;
using library.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using library.Pages;
using library.Services;

namespace library
{
    public partial class App : Application
    {
        
        public static INavigation Navigation => MainPageInstance.Navigation;
        public static DBService DbService { get; private set; }
        public static ComponentFactory ComponentFactory { get; private set; }
        public static PageFactory PageFactory { get; private set; }

        public static User CurrentUser { get; set; }

        /// <summary>
        /// Static reference to main page
        /// </summary>
        private static Page MainPageInstance { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LogoPage());
            MainPageInstance = MainPage;

            DbService = new DBService();
            ComponentFactory = new ComponentFactory();
            PageFactory = new PageFactory();
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