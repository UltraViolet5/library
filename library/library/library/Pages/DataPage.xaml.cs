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
        public DataPage()
        {
            InitializeComponent();
            Task.Run(AnimateBG);
        }



        private async void AnimateBG()
        {
            Action<double> forward = input => slGradient.AnchorY = input;
     
            slGradient.Animate("forward", callback: forward, start: 0, end: 1, length: 10000, easing: Easing.Linear);
            await Task.Delay(10000);

            AnimateBG();
        }
    }
}