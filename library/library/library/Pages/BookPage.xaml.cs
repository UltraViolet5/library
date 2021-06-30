using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using library.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace library.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookPage : ContentPage
    {
        public BookViewModel BookViewModel { get; set; }

        public BookPage(int id)
        {
            InitializeComponent();

            BookViewModel = new BookViewModel(App.DbService.GetBook(id));
            BindingContext = BookViewModel;
        }
    }
}