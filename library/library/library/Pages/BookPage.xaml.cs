using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using library.FactoryMethod;
using library.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace library.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookPage : ContentPage
    {
        private readonly IPageFactory _pageFactory;
        public BookViewModel BookViewModel { get; set; }

        public BookPage(int id)
        {
            InitializeComponent();

            BookViewModel = new BookViewModel(App.DbService.GetBook(id));
            BindingContext = BookViewModel;

            _pageFactory = new PageFactory();
            Content = _pageFactory.GetBookPage();

            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}