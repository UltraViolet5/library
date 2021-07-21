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
    public partial class AddMatePage : ContentPage
    {
        public AddMatePage()
        {
            InitializeComponent();

            BindingContext = new AddMateViewModel();
        }
    }
}