using library.FactoryMethod;
using library.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace library.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MatesPage : ContentPage
    {
        public MatesViewModel MatesViewModel { get; set; }

        public MatesPage()
        {
            InitializeComponent();
            MatesViewModel = new MatesViewModel();
            BindingContext = MatesViewModel;

            Content = new PageFactory().GetMatesPage(MatesViewModel);

            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}