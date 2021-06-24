using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using library.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace library.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        private readonly SettingsViewModel _settingsViewModel;
        public SettingsPage()
        {
            InitializeComponent();

            _settingsViewModel = new SettingsViewModel();
            BindingContext = _settingsViewModel;
        }
    }
}