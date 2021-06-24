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
        public ICommand AddAuthor { get; set; }

        public string Title { get; set; }
        public string  Authors;
        public DateTime PublishingYear { get; set; }
        public Category Category { get; set; }



        public AddBookViewModel()
        {
            SaveButton = new Command(SaveButtonExecute);
            AddAuthor = new Command(AddAutorExecute);
        }

        

        private void AddAutorExecute(object obj)
        {
        }


        private void SaveButtonExecute(object obj)
        {
            Book NewBook = new Book();

            NewBook.Title = this.Title;
            NewBook.Authors = this.Authors;
            NewBook.BarcodeNumber = this._barcodeText;
            NewBook.Categories = Category.SiFi;
            NewBook.PublishingYear = this.PublishingYear;
            
            App.Navigation.PopAsync();
            
        }
    }
}