
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using library.ViewModel;

namespace library.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanPage : ContentPage
    {

        public static Button ScanButton { get; set; }
        public static ZXing.Net.Mobile.Forms.ZXingScannerView ScannerView { get; private set; }

        public event EventHandler IsScaned;
        


        public ScanPage()
        {
           

            InitializeComponent();
            ScanButton = scanbuttonXAML;
            ScannerView = scanerXAML;

        }


        /// <summary>
        /// if scan have result, display result in label.
        /// </summary>
        /// <param name="result"></param>
        private void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
           


            Device.BeginInvokeOnMainThread(() =>
            {
                scanResultText.Text = result.Text + "(type: " + result.BarcodeFormat.ToString() + ")";
                ///AddBookDataPage.BarLabel.Text = scanResultText.Text;

            });

            ///AddBookViewModel.Instance.BarcodeText = result.Text + "(type: " + result.BarcodeFormat.ToString() + ")";
            ///Navigation.NavigationStack[Navigation.NavigationStack.Count-2]
            ScannerView.IsScanning = false;
            ScanButton.Text = "Scan";
            App.Navigation.PopModalAsync();

            if(IsScaned != null)
            {
                IsScaned(result.Text, EventArgs.Empty);

            }
            

        }

   


        /// <summary>
        /// Turn on or off scaning.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scanbuttonXAML_Clicked(object sender, EventArgs e)
        {
            if (ScannerView.IsScanning)
            {
                ScannerView.IsScanning = false;
                ScanButton.Text = "Scan";
            }
            else
            {
                ScannerView.IsScanning = true;
                ScanButton.Text = "Off Scan";
                scanResultText.Text += "Skanowanie";
            }
        }
    }
}