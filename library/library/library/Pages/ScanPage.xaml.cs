
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
    public partial class ScanPage : ContentPage
    {
        public ScanPage()
        {
           

            InitializeComponent();
            

        }

        private void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                scanResultText.Text = result.Text + "(type: " + result.BarcodeFormat.ToString() + ")";
            });
                
        }



       
    }
}