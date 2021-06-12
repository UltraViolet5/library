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
        // TODO FIXME Propertisy z dużej litery
        Books books { get; set; }
        Login login { get; set; }
        Mates mates { get; set; }
        MyRentals myRentals { get; set; }
        Registration register { get; set; }
        Rented rented { get; set; }
        Settings settings { get; set; }
        UserView userView { get; set; }


        private readonly MainViewModel _mainViewModel;
        private readonly ComponentFactoryBase _componentFactory;

        public MainPage()
        {
            InitializeComponent();
            books = new Books();
            login = new Login();
            mates = new Mates();
            myRentals = new MyRentals();
            register = new Registration();
            rented = new Rented();
            settings = new Settings();
            userView = new UserView();


            btnBooks.Clicked += (s, e) => Navigation.PushAsync(books);
            btnLoginPage.Clicked += (s, e) => Navigation.PushAsync(login);
            btnMates.Clicked += (s, e) => Navigation.PushAsync(mates);
            btnMyRentals.Clicked += (s, e) => Navigation.PushAsync(myRentals);
            btnRegister.Clicked += (s, e) => Navigation.PushAsync(register);
            btnRented.Clicked += (s, e) => Navigation.PushAsync(rented);
            btnSettings.Clicked += (s, e) => Navigation.PushAsync(settings);
            btnUserView.Clicked += (s, e) => Navigation.PushAsync(userView);

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
                LastBooks.Children.Add(bookCard);
            }
        }
    }
}