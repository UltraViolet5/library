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
            
            InitDataAsync();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void InitDataAsync()
        {
            MyRentalsViewModel = new MyRentalsViewModel();
            MyRentalsViewModel.Borrowings = await App.ApiService.GetBorrowings(App.CurrentUser.Id);
            BindingContext = MyRentalsViewModel;
            Content = App.PageFactory.GetMyRentalsPage(MyRentalsViewModel.Borrowings);
        }
    }
}