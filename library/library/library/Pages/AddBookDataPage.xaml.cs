using library.ViewModel;
using System;
using System.Threading.Tasks;
using library.FactoryMethod;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace library.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBookDataPage : ContentPage
    {
        public static Label BarLabel;
        public readonly AddBookViewModel AddBookViewModel;
        public ScanPage ScanPage;


        public AddBookDataPage()
        {
            InitializeComponent();
            Task.Run(AnimateBG);
            BarLabel = BarcodeLabel;


            AddBookViewModel = new AddBookViewModel();
            BindingContext = AddBookViewModel;
            ScanPage = new ScanPage();
            ScanPage.IsScaned += HandleScanSucceed;

            // Add Photo box to layout
            PhotoContainer.Children.Add(
                new ComponentFactory()
                    .GetPhotoBox("AddPhotoCommand", "Photo",
                        "AddPhotoIsEnabled", "PhotoSource"));
        }

        private void HandleScanSucceed(object sender, EventArgs e)
        {
            string Scantext = (string) sender;
            AddBookViewModel.BarcodeText = Scantext;
        }


        private async void AnimateBG()
        {
            Action<double> forward = input => slGradient.AnchorY = input;
            Action<double> backward = input => slGradient.AnchorY = input;

            while (true)
            {
                slGradient.Animate("forward", callback: forward, start: 0, end: 1, length: 10000,
                    easing: Easing.SinInOut);
                await Task.Delay(10000);
                slGradient.Animate("backward", callback: backward, start: 1, end: 0, length: 10000,
                    easing: Easing.SinInOut);
                await Task.Delay(10000);
            }
        }

        private void CancelBtn_Clicked(object sender, EventArgs e)
        {
            App.Navigation.PopAsync();
        }

        private void ScanBtn_Clicked(object sender, EventArgs e)
        {
            App.Navigation.PushModalAsync(ScanPage);
        }

        private void Title_TextChanged(object sender, TextChangedEventArgs e)
        {
            AddBookViewModel.SaveButton.ChangeCanExecute();
        }

        private void Author_TextChanged(object sender, TextChangedEventArgs e)
        {
            AddBookViewModel.SaveButton.ChangeCanExecute();
        }
    }
}