using System;
using library.ViewModel;
using System.Linq;
using library.FactoryMethod;
using library.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace library.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BooksPage : ContentPage
    {
        public static BooksViewModel BooksViewModel { get; set; }

        private StackLayout _booksStack;

        public BooksPage(bool addBookButton = false)
        {
            InitializeComponent();

            var books = App.DbService.GetBooks()
                .Where(b => b.Owner.Email == App.CurrentUser.Email)
                .Select(b => new BookViewModel(b));
            BooksViewModel = new BooksViewModel(books, this);
            BindingContext = BooksViewModel;

            Content = App.PageFactory
                .GetBooksPage(App.CurrentUser, BooksViewModel, addBookButton);
            _booksStack = ((StackLayout) ((ScrollView) Content).Content);

            RefreshBooksList();
            NavigationPage.SetHasNavigationBar(this, false);

            BooksViewModel.OnSortingMethodChange += HandleSortingMethodChange;
        }

        public void RefreshBooksList()
        {
            ClearBooks();
            App.PageFactory.ListBookCards(ref _booksStack, BooksViewModel.Books);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshBooksList();
        }
        
        /// <summary>
        /// Remove bookCards, and don't remove label and entries
        /// </summary>
        private void ClearBooks()
        {
            while (_booksStack.Children.Count > 3)
            {
                _booksStack.Children.RemoveAt(_booksStack.Children.Count - 1);
            }
        }
        private void HandleSortingMethodChange(object sender, EventArgs e)
        {
            RefreshBooksList();
        }
    }
}