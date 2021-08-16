using library.ViewModel;
using System;
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
        public BooksViewModel BooksViewModel { get; set; }
        public bool PageLoaded { get; private set; }

        private PageFactory _pageFactory;
        private ScrollView _pageContent;
        private StackLayout _booksStack;

        public BooksPage(User booksOwner, bool addBookButton = false)
        {
            InitializeComponent();

            InitPage(booksOwner, addBookButton);
        }

        private async void InitPage(User booksOwner, bool addBookButton = false)
        {
            var books = await App.ApiService.GetBooks(booksOwner.Id);
            var bookViewModels = books
                .Select(b => new BookViewModel(b));
            BooksViewModel = new BooksViewModel(bookViewModels, this);
            foreach (BookViewModel model in BooksViewModel.Books)
            {
                model.OnBookRemoved += HandleBookRemoved;
            }
            BooksViewModel.OnSortingMethodChange += HandleOnSortingMethodChange;
            BindingContext = BooksViewModel;

            _pageFactory = new PageFactory();
            _pageContent = _pageFactory.GetBooksPage(booksOwner, BooksViewModel, addBookButton);
            _booksStack = (StackLayout)_pageContent.Content;

            Content = _pageContent;
            RefreshBooks();
            PageLoaded = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (!PageLoaded) return;

            ClearBooks();
            RefreshBooks();
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

        public void RefreshBooks()
        {
            ClearBooks();
            _pageFactory.ListBookCards(ref _booksStack, BooksViewModel.Books);
        }

        private void HandleOnSortingMethodChange(object sender, EventArgs e)
        {
            RefreshBooks();
        }
        private void HandleBookRemoved(object sender, EventArgs e)
        {
            var toRemoveId = 0;
            foreach (BookViewModel model in BooksViewModel.Books)
            {
                if (model.Id == (int) sender)
                {
                    break;
                }
                toRemoveId++;
            }
            BooksViewModel.Books.RemoveAt(toRemoveId);
            RefreshBooks();
        }
    }
}