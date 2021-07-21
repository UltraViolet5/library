using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace library.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanPage : ContentPage
    {
        public event EventHandler OnScanResultReturned;

        public static ZXing.Net.Mobile.Forms.ZXingScannerView ScannerView { get; private set; }
        
        public ScanPage()
        {
            InitializeComponent();
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

                OnScanResultReturned?.Invoke(result.Text, EventArgs.Empty);

                App.Navigation.PopModalAsync();
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ScannerView.IsScanning = true;
        }
    }
}