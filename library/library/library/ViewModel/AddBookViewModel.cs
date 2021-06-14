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
        public static ZXing.Result Result = null;

        public AddBookViewModel()
        {
            
            RaisePropertyChanged("Result");
        }

        

        /// <summary>
        /// Turn on or off scaning.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       
    }
}
