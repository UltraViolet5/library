
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


        /// <summary>
        /// if scan have result, display Result in Label.
        /// </summary>
        /// <param name="result"></param>
        private void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                scanResultText.Text = result.Text + "(type: " + result.BarcodeFormat.ToString() + ")";
            });
                
        }


        /// <summary>
        /// Turn on or off scaning.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scanbutton_Clicked(object sender, EventArgs e)
        {
            if (scaner.IsScanning)
            {
                scaner.IsScanning = false;
                scanbutton.Text = "Scan";
            }
            else
            {
                scaner.IsScanning = true;
                scanbutton.Text = "Off Scan";
            }
        }
    }
}