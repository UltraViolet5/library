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

        private readonly IPageFactory _pageFactory;

        public MyRentalsPage()
        {
            InitializeComponent();

            MyRentalsViewModel = new MyRentalsViewModel();

            BindingContext = MyRentalsViewModel;

            _pageFactory = new PageFactory();

            Content = _pageFactory.GetMyRentalsPage(MyRentalsViewModel.Borrowings);
        }

    
       
    }
}