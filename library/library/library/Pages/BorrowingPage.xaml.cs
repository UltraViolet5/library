using library.Model;
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
    public partial class BorrowingPage : ContentPage
    {

        public BorrowingViewModel BorrowingViewModel { get; set; }
        public BorrowingPage(Borrowing borrowing)
        {
            InitializeComponent();

            BorrowingViewModel = new BorrowingViewModel(borrowing);

            BindingContext = BorrowingViewModel;

            Content = App.ComponentFactory.GetBookCard(borrowing);
        }
    }
}