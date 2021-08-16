using library.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace library.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookPage : ContentPage
    {
        public BookViewModel BookViewModel { get; set; }

        public BookPage(BookViewModel model)
        {
            InitializeComponent();

            InitPageAsync(model);

            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void InitPageAsync(BookViewModel model)
        {
            // var book = await App.ApiService.GetBook(id);
            BookViewModel = model;
            BindingContext = BookViewModel;

            Content = App.PageFactory.GetBookPage();
        }
    }
}