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
        public static StackLayout Autorlayout;
        private readonly ComponentFactoryBase _componentFactory;

        public AddBookDataPage()
        {
            InitializeComponent();
            Task.Run(AnimateBG);
            BarLabel = BarcodeLabel;
            Autorlayout = AutorFrameXAML;
            _componentFactory = new ComponentFactory();
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

        private void AddAutor_Clicked(object sender, EventArgs e)
        {
            Autorlayout.Children.Add(_componentFactory.CreateFrameWithEntry());
        }

        private void AddCategory_Clicked(object sender, EventArgs e)
        {
        }

        private void SaveBtn_Clicked(object sender, EventArgs e)
        {
        }

        private void CancelBtn_Clicked(object sender, EventArgs e)
        {
        }

        private void ScanBtn_Clicked(object sender, EventArgs e)
        {
            App.Navigation.PushAsync(new ScanPage());
        }
    }
}