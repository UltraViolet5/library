using library.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private readonly User _booksOwner;
        private readonly PageFactory _pageFactory;
        private readonly ScrollView _pageContent;
        private StackLayout _booksStack;
        
        public BooksPage(User booksOwner, bool addBookButton = false)
        {
            InitializeComponent();
            
            var books = App.DbService.GetBooks()
                .Where(b => b.Owner.Email == booksOwner.Email)
                .Select(b => new BookViewModel(b));
            BooksViewModel = new BooksViewModel(books);
            BindingContext = BooksViewModel;

            _pageFactory = new PageFactory();
            _booksOwner = booksOwner;
            _pageContent = _pageFactory.GetBooksPage(booksOwner, BooksViewModel, addBookButton);
            _booksStack = (StackLayout)_pageContent.Content;


            Content = _pageContent;
            // Refreshing books
            _pageFactory.ListBookCards(ref _booksStack, BooksViewModel.Books);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ClearBooks();
            // Refreshing books
            _pageFactory.ListBookCards(ref _booksStack, BooksViewModel.Books);
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
    }
}