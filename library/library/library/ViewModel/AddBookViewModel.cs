using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;
using library.Model;


namespace library.ViewModel
{
    public class AddBookViewModel : BaseViewModel
    {
        public Book NewBook { get; set; } = new Book();

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

        public bool AddPhotoIsEnabled
        {
            get => _addPhotoIsEnabled;
            set
            {
                _addPhotoIsEnabled = value;
                RaisePropertyChanged(nameof(AddPhotoIsEnabled));
            }
        }

        private byte[] _photo;

        public byte[] Photo
        {
            get => _photo;
            set
            {
                _photo = value;
                RaisePropertyChanged(nameof(PhotoSource));
            }
        }

        /// <summary>
        /// Photo source represent Source in component Image
        /// </summary>
        public ImageSource PhotoSource
        {
            get
            {
                var photoSource = Utils.BytesToImageSource(Photo);
                if (photoSource == null)
                {
                    // If book don't have photo load photo from global resources
                    var assembly = this.GetType().GetTypeInfo().Assembly;
                    var data = Utils.ImageDataFromResource("library.Resources.picture.png", assembly);
                    return Utils.BytesToImageSource(data);
                }
                return photoSource;
            }
          
        }

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
        private bool _addPhotoIsEnabled = true;
        private ImageSource _photoSource;
        private DateTime _publishingYear;
        private string _categoryString = "Other";
        private bool _read;
        private bool _available;
        private Command _saveButton;
        private ICommand _addPhotoCommand;

        public string Authors
        {
            get { return _authors; }
            set { _authors = value; }
        }


        public DateTime PublishingYear
        {
            get => _publishingYear;
            set => _publishingYear = value;
        }

        public string CategoryString
        {
            get => _categoryString;
            set => _categoryString = value;
        }

        public bool Read
        {
            get => _read;
            set => _read = value;
        }

        public bool Available
        {
            get => _available;
            set => _available = value;
        }

        public Command SaveButton
        {
            get => _saveButton;
            set => _saveButton = value;
        }

        public ICommand AddPhotoCommand
        {
            get => _addPhotoCommand;
            set => _addPhotoCommand = value;
        }

        public AddBookViewModel()
        {
            SaveButton = new Command(SaveButtonExecute, canExecute: SaveButtonCanExecute);
            AddPhotoCommand = new Command(AddPhotoExecute);
        }

        private async void AddPhotoExecute()
        {
            AddPhotoIsEnabled = false;

            Photo = await Utils.TakePhoto();

            AddPhotoIsEnabled = true;
        }


        private bool SaveButtonCanExecute(object arg)
        {
            if (!string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Authors))
            {
                return true;
            }

            return false;
        }

        private Category GetCategoryByString(string categoryName)
        {
            Category categoryEnum = (Category) Enum.Parse(typeof(Category), categoryName);

            return categoryEnum;
        }


        private void SaveButtonExecute(object obj)
        {
            NewBook.Title = this.Title;
            NewBook.Authors = this.Authors;
            NewBook.BarcodeNumber = this._barcodeText;
            NewBook.Owner = App.CurrentUser;
            NewBook.Category = GetCategoryByString(CategoryString);
            NewBook.PublishingYear = this.PublishingYear;
            NewBook.Available = this.Available;
            NewBook.Read = this.Read;
            NewBook.Photo = Photo;

            App.DbService.AddBook(NewBook);


            App.Navigation.PopAsync();
        }
    }
}