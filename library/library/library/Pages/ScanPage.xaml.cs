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
            //ScanButton = scanbuttonXAML;
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
                ScannerView.IsScanning = false;
                if (IsScaned != null)
                {
                    IsScaned(result.Text, EventArgs.Empty);
                }
            });

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ScannerView.IsScanning = true;
        }
    }
}