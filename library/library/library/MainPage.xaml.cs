using System;
using Xamarin.Forms;
using library.ViewModel;
using Xamarin.Forms.Xaml;
using library.Model;
using System.Collections.Generic;
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
            MainPageViewModel = new MainPageViewModel();
            try
            {
                DisplayComponents();

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
                var mateUI = App.ComponentFactory.GetMateIcon(mate, "MatesCommand");

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
                var bookCard = App.ComponentFactory.GetBookCard(book);
                Console.WriteLine($"PhotoSource");
                LastBooks.Children.Add(bookCard);
                index++;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LastBooks.Children.Clear();
            DisplayBooks();
        }

        private async void DisplayComponents()
        {
            var books = await App.ApiService.GetBooks();
            MainPageViewModel.Books = books
                .Where(b => b.Owner.Email == (string)App.Current.Properties["UserEmail"])
                .Take(2)
                .Select(b => new BookViewModel(b));
            DisplayBooks();
            MainPageViewModel.Categories = Enum.GetNames(typeof(Category));
            DisplayCategories();
            MainPageViewModel.Mates = App.CurrentUser.Friends
                .Select(m => new UserViewModel(m));
            DisplayMates();
            MainPageViewModel.Borrowings = App.DbService.GetBorrowings()
                .Select(b => new BorrowingViewModel(b))
                .Take(2);
            DisplayBorrowings();
        }
    }

}