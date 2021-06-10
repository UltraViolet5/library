using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using library.Pages;
using library.ViewModel;
using library.FactoryMethod;


namespace library
{
    public partial class MainPage : ContentPage
    {

        /*Books Books { get; set; }
        Login Login { get; set; }
        Mates Mates { get; set; }
        MyRentals MyRentals { get; set; }
        Registration Register { get; set; }
        Rented Rented { get; set; }
        Settings Settings { get; set; }
        UserView UserView { get; set; }*/



        private readonly MainViewModel _mainViewModel;
        private readonly ComponentFactoryBase _componentFactory;

        public MainPage()
        {
            InitializeComponent();
        /*    Books = new Books();
            Login = new Login();
            Mates = new Mates();
            MyRentals = new MyRentals();
            Register = new Registration();
            Rented = new Rented();
            Settings = new Settings();
            UserView = new UserView();*/




            /*AddTappedLbEvent(Books, BooksLabel);*/
            /*btnBooks.Clicked += (s, e) => Navigation.PushAsync(Books);*/
            /* btnLoginPage.Clicked += (s, e) => Navigation.PushAsync(Login);*/
            /*  btnMates.Clicked += (s, e) => Navigation.PushAsync(Mates);
              btnMyRentals.Clicked += (s, e) => Navigation.PushAsync(MyRentals);
              btnRegister.Clicked += (s, e) => Navigation.PushAsync(Register);
              btnRented.Clicked += (s, e) => Navigation.PushAsync(Rented);
              btnSettings.Clicked += (s, e) => Navigation.PushAsync(Settings);
              btnUserView.Clicked += (s, e) => Navigation.PushAsync(UserView);*/

            _mainViewModel = new MainViewModel();
            _componentFactory = new ComponentFactory();

            BindingContext = _mainViewModel;
            DisplayBooks();

            
        }

        /// <summary>
        /// Add last books to main page.
        /// </summary>
        private void DisplayBooks()
        {
            foreach (var book in _mainViewModel.Books)
            {
                var bookCard = _componentFactory.CreateBookCard(book);
                LastBooks.Children.Add(bookCard);
            }
        }
    /*    /// <summary>
        /// Add Taped event to the label
        /// </summary>
        /// <param name="page"></param>
        /// <param name="label"></param>
        public void AddTappedLbEvent(Page page,Label label)
        {
            TapGestureRecognizer TappedEvt = new TapGestureRecognizer();
            TappedEvt.Tapped += (s, e) =>
            {
                Navigation.PushAsync(page);
            };

            label.GestureRecognizers.Add(TappedEvt);
        }*/

        
    }
}