using System;
using System.Collections.Generic;
using Xamarin.Forms;
using library.Model;


namespace library.ViewModel
{
    public class AddBookViewModel : BaseViewModel
    {
        private bool _saveBtnValue;

        public bool SaveBtnValue
        {
            get { return _saveBtnValue; }
            set
            {
                _saveBtnValue = value;
                RaisePropertyChanged(nameof(SaveBtnValue));
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

        public List<string> CategoriesList => new List<string>(Enum.GetNames(typeof(Category)));


        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _authors;

        public string Authors
        {
            get { return _authors; }
            set { _authors = value; }
        }


        public DateTime PublishingYear { get; set; }
        public string CategoryString { get; set; }
        public bool Read { get; set; }
        public bool Available { get; set; }

        public Command SaveButton { get; set; }


        public AddBookViewModel()
        {
            SaveButton = new Command(SaveButtonExecute, canExecute: SaveButtonCanExecute);
        }


        private bool SaveButtonCanExecute(object arg)
        {
            if (!string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Authors))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Category GetCategoryByString(string categoryName)
        {
            Category categoryEnum = (Category) Enum.Parse(typeof(Category), categoryName);

            return categoryEnum;
        }


        private void SaveButtonExecute(object obj)
        {
            Book NewBook = new Book();

            NewBook.Title = this.Title;
            NewBook.Authors = this.Authors;
            NewBook.BarcodeNumber = this._barcodeText;
            NewBook.Owner = App.CurrentUser;
            NewBook.Category = GetCategoryByString(CategoryString);
            NewBook.PublishingYear = this.PublishingYear;
            NewBook.Available = this.Available;
            NewBook.Read = this.Read;

            App.DbService.AddBook(NewBook);


            App.Navigation.PopAsync();
        }
    }
}