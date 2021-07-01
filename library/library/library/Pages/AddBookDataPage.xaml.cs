using library.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using library.FactoryMethod;


namespace library.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBookDataPage : ContentPage
    {
        public static Label BarLabel;
        private readonly ComponentFactoryBase _componentFactory;
        public readonly AddBookViewModel AddBookViewModel;
        public ScanPage ScanPage;
        public static CheckBox CheckBox;

        

        public AddBookDataPage()
        {
            InitializeComponent();
            Task.Run(AnimateBG);
            BarLabel = BarcodeLabel;
            
            _componentFactory = new ComponentFactory();
            AddBookViewModel = new AddBookViewModel();
            BindingContext = AddBookViewModel;
            ScanPage = new ScanPage();
            ScanPage.IsScaned += HandleScanSucced;
            



            
        }



        private void HandleScanSucced(object sender, EventArgs e)
        {
            string Scantext = (string)sender;
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

    

   

        private void SaveBtn_Clicked(object sender, EventArgs e)
        {
           
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