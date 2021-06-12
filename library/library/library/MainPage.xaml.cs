using Xamarin.Forms;
using library.Pages;
using library.ViewModel;
using library.FactoryMethod;
using Xamarin.Forms.Xaml;


namespace library
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
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
            DisplayCategories();
        }

        /// <summary>
        /// Add book categories to main page.
        /// </summary>
        private void DisplayCategories()
        {
            foreach (CategoryViewModel category in _mainViewModel.Categories)
            {
                var categoriesUI = _componentFactory.CreateCategoryBtn(category);
                var TappGest = new TapGestureRecognizer();
                TappGest.SetBinding(TapGestureRecognizer.CommandProperty, "ShowBooksByCategory");
                categoriesUI.GestureRecognizers.Add(TappGest);
                Categories.Children.Add(categoriesUI);

            }


        }

    


        /// <summary>
        /// Add last books to main page.
        /// </summary>
        private void DisplayBooks()
        {
            foreach (var book in _mainViewModel.Books)
            {
                var bookCard = _componentFactory.CreateBookCard(book);
                var TappGest = new TapGestureRecognizer();
                TappGest.SetBinding(TapGestureRecognizer.CommandProperty, "ShowBook");
                bookCard.GestureRecognizers.Add(TappGest);
                LastBooks.Children.Add(bookCard);
            }
        }
        


        
    }
}