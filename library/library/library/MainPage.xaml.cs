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
    
        public readonly MainViewModel _mainViewModel;
        private readonly ComponentFactoryBase _componentFactory;

        public MainPage()
        {
            
            InitializeComponent();
  
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
                var tapGest = new TapGestureRecognizer();
                tapGest.SetBinding(TapGestureRecognizer.CommandProperty, "ShowBooksByCategory");
               
                categoriesUI.GestureRecognizers.Add(tapGest);
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