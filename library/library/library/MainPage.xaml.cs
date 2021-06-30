using Xamarin.Forms;
using library.ViewModel;
using Xamarin.Forms.Xaml;


namespace library
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
    
        public readonly MainPageViewModel MainPageViewModel;

        public MainPage()
        {
            
            InitializeComponent();
  
            MainPageViewModel = new MainPageViewModel();

            BindingContext = MainPageViewModel;

            NavigationPage.SetHasNavigationBar(this, false);

            DisplayBooks();
            DisplayCategories();
            DisplayMates();
            DisplayBorrowings();
        }


        /// <summary>
        /// Display list of lasts borrowings
        /// </summary>
        private void DisplayBorrowings()
        {
            foreach (BorrowingViewModel borrowing in MainPageViewModel.Borrowings)
            {
                var borrowingUi = App.ComponentFactory.CreateRentalBtn(borrowing);
                
                Rentals.Children.Add(borrowingUi);
            }
        }

        /// <summary>
        /// Add mates icons to main page
        /// </summary>
        private void DisplayMates()
        {
            foreach (UserViewModel mate in MainPageViewModel.Mates)
            {
                var mateUI = App.ComponentFactory.CreateMateIcon(mate);

                Mates.Children.Add(mateUI);
            }
        }

        /// <summary>
        /// Add book categories to main page.
        /// </summary>
        private void DisplayCategories()
        {
            foreach (string category in MainPageViewModel.Categories)
            {
                var categoriesUI = App.ComponentFactory.CreateCategoryBtn(category);
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
            foreach (var book in MainPageViewModel.Books)
            {
                var bookCard = App.ComponentFactory.CreateBookCard(book);
                LastBooks.Children.Add(bookCard);
            }
        }
    }

}