using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using library.Pages;
using library.Services;
using Xamarin.Forms;


namespace library.ViewModel
{
    public class AddBookViewModel : BaseViewModel
    {
        public static ZXing.Result Result;

        public ICommand Scan { get; set; }

        public AddBookViewModel()
        {
            Scan = new Command(ScanExecute, ScanCanExecute);
        }

        private bool ScanCanExecute(object arg)
        {
            return true;
        }


        /// <summary>
        /// Turn on or off scaning.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScanExecute(object obj)
        {
            if (ScanPage.ScannerView.IsScanning)
            {
                ScanPage.ScannerView.IsScanning = false;
                ScanPage.ScanButton.Text = "Scan";
            }
            else
            {
                ScanPage.ScannerView.IsScanning = true;
                ScanPage.ScanButton.Text = "Off Scan";
            }
        }
    }
}
