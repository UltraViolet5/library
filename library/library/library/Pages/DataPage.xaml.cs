using library.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace library.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataPage : ContentPage
    {
        public static Label Label;

        public DataPage()
        {
            InitializeComponent();
            Task.Run(AnimateBG);
            Label = BarcodeLabel;


        }



        private async void AnimateBG()
        {
            Action<double> forward = input => slGradient.AnchorY = input;
            Action<double> backward = input => slGradient.AnchorY = input;

            while (true)
            {
                slGradient.Animate("forward", callback: forward, start: 0, end: 1, length: 10000, easing: Easing.SinInOut);
                await Task.Delay(10000);
                slGradient.Animate("backward", callback: backward, start: 1, end: 0, length: 10000, easing: Easing.SinInOut);
                await Task.Delay(10000);
            }
        }

        private void AddAutor_Clicked(object sender, EventArgs e)
        {

        }

        private void AddCategory_Clicked(object sender, EventArgs e)
        {

        }
    }
}