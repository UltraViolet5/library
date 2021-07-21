using System;
using Xamarin.Forms;
using library.ViewModel;
using Xamarin.Forms.Xaml;
using library.Model;
using System.Collections.Generic;



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

            var Borrowings = App.DbService.GetBorrowings();

            var FutureReturnsList = new List<Borrowing>();

            var NextMonth = DateTime.UtcNow.AddMonths(5);


            foreach (var borrowing in Borrowings)
            {
                if (borrowing.ReturnDate< NextMonth)
                {
                    FutureReturnsList.Add(borrowing);
                }
            }

            foreach (var borrowing in FutureReturnsList)
            {
                var book = App.DbService.GetBook(borrowing.BookId);

                var borrowingUi = App.ComponentFactory.GetRentalBtn(new BorrowingViewModel(borrowing));
                Rentals.Children.Add(borrowingUi);
            }

            //foreach (BorrowingViewModel borrowing in MainPageViewModel.Borrowings)
            //{
            //    var borrowingUi = App.ComponentFactory.GetRentalBtn(borrowing);

            //    Rentals.Children.Add(borrowingUi);
            //}
        }

        /// <summary>
        /// Add mates icons to main page
        /// </summary>
        private void DisplayMates()
        {
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
    }

}