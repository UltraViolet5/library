using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using library.Model;
using library.ViewModel;

namespace library.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyRentalsPage : ContentPage
    {
        MyRentalsViewModel MyRentalsViewModel { get; set; }

        public MyRentalsPage()
        {
            InitializeComponent();

            MyRentalsViewModel = new MyRentalsViewModel();

            BindingContext = MyRentalsViewModel;
        }
       
    }
}