using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using library.Model;
using library.ViewModel;
using library.FactoryMethod;

namespace library.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyRentalsPage : ContentPage
    {
        MyRentalsViewModel MyRentalsViewModel { get; set; }

        public MyRentalsPage()
        {
            InitializeComponent();
            
            InitData();
        }

        private async void InitData()
        {
            MyRentalsViewModel = new MyRentalsViewModel();
            MyRentalsViewModel.Borrowings = await App.ApiService.GetBorrowings(App.CurrentUser.Email);
            BindingContext = MyRentalsViewModel;
            Content = App.PageFactory.GetMyRentalsPage(MyRentalsViewModel.Borrowings);
        }
    }
}