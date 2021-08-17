using System;
using Xamarin.Forms;
using library.ViewModel;
using Xamarin.Forms.Xaml;
using library.Model;
using System.Linq;


namespace library
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {

        public readonly MainPageViewModel MainPageViewModel;

        public MainPage()
        {
            InitializeComponent();
            MainPageViewModel = new MainPageViewModel(this);
            try
            {
                DisplayComponentsAsync();

                BindingContext = MainPageViewModel;

                NavigationPage.SetHasNavigationBar(this, false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        /// <summary>
        /// Display list of lasts borrowings
        /// </summary>
        private void DisplayBorrowings()
        {
            if (MainPageViewModel.Borrowings == null) return;

            foreach (BorrowingViewModel borrowing in MainPageViewModel.Borrowings)
            {
                var borrowingUi = App.ComponentFactory.GetRentalBtn(borrowing);

                Rentals.Children.Add(borrowingUi);
            }
        }

        /// <summary>
        /// Add mates icons to main page
        /// </summary>
        private void DisplayMates()
        {
            if (MainPageViewModel.Mates == null)
                return;

            foreach (UserViewModel mate in MainPageViewModel.Mates)
            {
                var mateUI = App.ComponentFactory.GetMateIcon(mate, boundCommand:"MatesCommand");

                Mates.Children.Add(mateUI);
            }
        }

        /// <summary>
        /// Add book categories to main page.
        /// </summary>
        private void DisplayCategories()
        {
            if (MainPageViewModel.Categories == null)
                return;
            
            foreach (string category in MainPageViewModel.Categories)
            {
                var categoriesUI = App.ComponentFactory.GetCategoryBtn(category);
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
            if (MainPageViewModel.Books == null)
                return;
            
            int index = 0;
            foreach (var book in MainPageViewModel.Books)
            {
                // book.OnBookRemoved += HandleBookRemoved;
                var bookCard = App.ComponentFactory.GetBookCard(book);
                Console.WriteLine($"PhotoSource");
                LastBooks.Children.Add(bookCard);
                index++;
            }
        }

        private void HandleBookRemoved(object sender, EventArgs e)
        {
            var toRemoveId = 0;
            foreach (BookViewModel model in MainPageViewModel.Books)
            {
                if (model.Id == (int)sender)
                {
                    break;
                }
                toRemoveId++;
            }
            MainPageViewModel.Books.RemoveAt(toRemoveId);
            RefreshBooks();
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshBooks();
        }

        private async void DisplayComponentsAsync()
        {
            var books = await App.ApiService.GetBooks(App.CurrentUser.Id, 2);
            MainPageViewModel.Books = books
                .Select(b => new BookViewModel(b)).ToList();
            DisplayBooks();
            MainPageViewModel.Categories = Enum.GetNames(typeof(Category));
            DisplayCategories();
            MainPageViewModel.Mates = App.CurrentUser.Friends
                .Select(m => new UserViewModel(m));
            DisplayMates();
            var borrowings = await App.ApiService.GetBorrowings(App.CurrentUser.Id, 2);
            MainPageViewModel.Borrowings = borrowings
                .Select(b => new BorrowingViewModel(b));
            DisplayBorrowings();
        }

        private async void RefreshBooks()
        {
            var books = await App.ApiService.GetBooks(App.CurrentUser.Id, 2);
            MainPageViewModel.Books = books
                .Select(b => new BookViewModel(b)).ToList();
            LastBooks.Children.Clear();
            DisplayBooks();
        }
    }

}