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
    public partial class Books : ContentPage
    {
        public static BooksViewModel BooksViewModelInst { get; set; }
        public static AddBook AddBookPage;
        

        public Books()
        {
            InitializeComponent();
            AddBookPage = new AddBook();
            BooksViewModelInst = new BooksViewModel();

        }

        private void btnAddBook_Clicked(object sender, EventArgs e)
        {
             App.FirstPage.Navigation.PushAsync(AddBookPage);
        }
    }
}