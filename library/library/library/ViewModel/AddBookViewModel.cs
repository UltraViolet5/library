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
        public ICommand AddAutor { get; set; }

        public string Title { get; set; }
        public string  Autors;
        public DateTime PublishingYear { get; set; }
        public Category Category;



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
            Book NewBook = new Book();

            NewBook.Title = this.Title;
            NewBook.Authors = this.Autors;
            NewBook.BarcodeNumber = this._barcodeText;
            NewBook.Categories = Category.Si_Fi;
            NewBook.PublishingYear = this.PublishingYear;
            

            App.Navigation.PopAsync();
            
        }
    }
}