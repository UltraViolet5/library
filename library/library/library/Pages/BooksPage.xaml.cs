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
        public static BooksViewModel BooksViewModelInst { get; set; }
        public static AddBookDataPage AddBookDataPage;
        

        public BooksPage()
        {
            InitializeComponent();
            AddBookDataPage = new AddBookDataPage();
            BooksViewModelInst = new BooksViewModel();

        }

        private void btnAddBook_Clicked(object sender, EventArgs e)
        {
             App.Navigation.PushAsync(AddBookDataPage);
        }
    }
}