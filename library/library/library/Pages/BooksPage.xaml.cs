using library.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace library.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BooksPage : ContentPage
    {
        public static BooksViewModel BooksViewModel { get; set; }
        
        public BooksPage()
        {
            InitializeComponent();

            BooksViewModel = new BooksViewModel();
            BindingContext = BooksViewModel;

            DisplayBooks();
        }

        private void DisplayBooks()
        {
            foreach (var book in BooksViewModel.Books)
            {
                var bookCard = App.ComponentFactory.GetBookCard(book);
                Books.Children.Add(bookCard);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Books.Children.Clear();
            DisplayBooks();
        }
    }
}