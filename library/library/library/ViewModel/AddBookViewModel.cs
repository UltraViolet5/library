using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using library.Pages;
using library.Services;
using Xamarin.Forms;
using library.Model;
using library.FactoryMethod;
using System.Linq;


namespace library.ViewModel
{
    public class AddBookViewModel : BaseViewModel
    {
        private bool _saveBtnValue;

        public bool SaveBtnValue { get { return  _saveBtnValue ; } set { _saveBtnValue = value;
                RaisePropertyChanged(nameof(SaveBtnValue));
            } }

        private bool _checkboxValue;
        public bool checkboxValue 
        { 
            get 
            {
                return _checkboxValue; 
            } 
            set 
            {   _checkboxValue = value;
                SaveBtnValue = value;
                RaisePropertyChanged(nameof(checkboxValue));
            } 
        }

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

        public List<string> CategoriesList
        {

            get { return new List<string>(Enum.GetNames(typeof(Category))); }
            private set { }
        }

        public ICommand SaveButton { get; set; }
        

        public string Title { get; set; }

        public string Authors { get; set; }

 
     
        public DateTime PublishingYear { get; set; }
        public string CategoryString { get; set; }



        public AddBookViewModel()
        {
            SaveButton = new Command(execute: SaveButtonExecute);

        }

        private Category GetCategoryByString(string categoryName)
        {
            Category categoryEnum = (Category)Enum.Parse(typeof(Category), categoryName);

            return categoryEnum;
        }


        private void SaveButtonExecute(object obj)
        {
            
            Book NewBook = new Book();

            NewBook.Title = this.Title;
            NewBook.Authors = this.Authors;
            NewBook.BarcodeNumber = this._barcodeText;
            NewBook.Categories = GetCategoryByString(CategoryString);
            NewBook.PublishingYear = this.PublishingYear;
             

            App.Navigation.PopAsync();
            
        }
    }
}