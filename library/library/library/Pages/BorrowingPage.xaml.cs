using library.Model;
using library.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace library.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BorrowingPage : ContentPage
    {

        public BorrowingViewModel BorrowingViewModel { get; set; }
        public BorrowingPage(Borrowing borrowing)
        {
            InitializeComponent();

            InitPageAsync(borrowing);
        }

        private async void InitPageAsync(Borrowing borrowing)
        {
            BorrowingViewModel = new BorrowingViewModel(borrowing);

            BindingContext = BorrowingViewModel;

            var book = await App.ApiService.GetBook(borrowing.BookId);

            Content = App.PageFactory.GetBorrowingPage(borrowing, book);
        }
    }
}