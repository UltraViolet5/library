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
    public partial class RegistrationPage : ContentPage
    {
        // TODO using register view model break running application
        // private readonly RegistrationViewModel _registrationViewModel;
        public RegistrationPage()
        {
            InitializeComponent();

            // _registrationViewModel = new RegistrationViewModel();

            // BindingContext = _registrationViewModel;
        }
    }
}