using GoogleVisionBarCodeScanner;
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
            GoogleVisionBarCodeScanner.Methods.SetSupportBarcodeFormat(BarcodeFormats.QRCode);
            InitializeComponent();
            

        }

        private async void CameraView_OnDetected(object sender, GoogleVisionBarCodeScanner.OnDetectedEventArg e)
        {
            List<GoogleVisionBarCodeScanner.BarcodeResult> obj = e.BarcodeResults;

            string result = string.Empty;
            for (int i = 0; i < obj.Count; i++)
            {
                result += $"{i + 1}. Type : {obj[i].BarcodeType}, Value : {obj[i].DisplayValue}{Environment.NewLine}";
            }
            Device.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Result", result, "OK");
                //If you want to stop scanning, you can close the scanning page
                await Navigation.PopModalAsync();
                //if you want to keep scanning the next barcode, do not close the scanning page and call below function
                //GoogleVisionBarCodeScanner.Methods.SetIsScanning(true);
            });

        }

    }
}