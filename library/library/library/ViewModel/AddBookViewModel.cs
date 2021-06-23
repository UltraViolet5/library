using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using library.Pages;
using library.Services;
using Xamarin.Forms;
using library.Model;
using library.FactoryMethod;


namespace library.ViewModel
{
    public class AddBookViewModel : BaseViewModel
    {

        //public static AddBookViewModel Instance
        //{
        //    get
          //  {
          //      if (Instance == null)
           //     {
                   // return Instance = new AddBookViewModel();
             //   }
              //  return Instance;
            //}
            //private set { }
       // }

        /* public ZXing.Result Result
         {
             get { return Result; }
             set
             {
                 if (Result != value)
                 {
                     Result = value;
                     // Can pass the property name as a string,
                     // -or- let the compiler do it because of the
                     // CallerMemberNameAttribute on the RaisePropertyChanged method.
                     RaisePropertyChanged("Result");
                 }
             }
         }*/

        private string _barcodeText = "Scan your barcode!";

        public string BarcodeText
        {
            get { return _barcodeText; }
            set
            {
              
                    _barcodeText = value;
                    RaisePropertyChanged(nameof(BarcodeText));
              
            }
        }
       
       /*     get
            {
                if (Result == null)
                {
                    return "Scan your Barcode";
                }
                else
                {
                    return Result.Text + "(type: " + Result.BarcodeFormat.ToString() + ")";
                }
            }
            set 
            {
                BarcodeText = value;
                RaisePropertyChanged("BarcodeText");
            } */
        

        public ICommand SaveButton { get; set; }
        public ICommand AddAutor { get; set; }

        public string Title { get; set; }
        public List<Author> Autors;
        public DatePicker PublishingYear { get; set; }
        public List<Category> Categories;


        public AddBookViewModel()
        {
            

            SaveButton = new Command(SaveButtonExecute);
            AddAutor = new Command(AddAutorExecute);
        
        }

        

        private void AddAutorExecute(object obj)
        {
        }


        private void SaveButtonExecute(object obj)
        {
            var NewBook = new Book();
        }
    }
}