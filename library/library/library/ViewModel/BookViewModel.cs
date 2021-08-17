using System;
using System.Collections.Generic;
using System.Reflection;
using library.Model;
using System.Windows.Input;
using Xamarin.Forms;

namespace library.ViewModel
{
    public class BookViewModel : BaseViewModel
    {
        public event EventHandler OnBookRemoved;

        private readonly Book _book;
        private bool _titleValidationShowMsg;
        private bool _authorsValidationShowMsg;
        private bool _addPhotoIsEnabled = true;
        private ICommand _addPhotoCommand;
        private ICommand _saveChangesCommand;
        private ICommand _removeBookCommand;

        public int Id => _book.Id;

        public string Title
        {
            get => _book.Title;
            set
            {
                if (_book.Title != value)
                {
                    _book.Title = value;
                    TitleValidation();
                    RaisePropertyChanged(nameof(Title));
                }
            }
        }

        public string Authors
        {
            get { return _book.Authors; }
            set
            {
                _book.Authors = value;
                AuthorsValidation();
                RaisePropertyChanged(nameof(Authors));
            }
        }

        public string PublishingYear => _book.PublishingYear.Date.Year.ToString();

        public User Owner
        {
            get => _book.Owner;
            set
            {
                if (_book.Owner != value)
                {
                    _book.Owner = value;
                    RaisePropertyChanged(nameof(Owner));
                }
            }
        }

        public bool Read
        {
            get => _book.Read;
            set
            {
                if (_book.Read != value)
                {
                    _book.Read = value;
                    RaisePropertyChanged(nameof(Read));
                }
            }
        }

        public Category Category
        {
            get => _book.Category;
            set
            {
                _book.Category = value;
                RaisePropertyChanged(nameof(Category));
            }
        }

        public string SelectedCategory
        {
            get => Category.ToString();
            set
            {
                Category = GetCategoryByString(value);
                RaisePropertyChanged(nameof(SelectedCategory));
                RaisePropertyChanged(nameof(Category));
            }
        }

        /// <summary>
        /// Dropdown categories source
        /// </summary>
        public List<string> Categories => new List<string>(Enum.GetNames(typeof(Category)));

        public int Votes
        {
            get => _book.Votes;
            set
            {
                if (_book.Votes != value)
                {
                    _book.Votes = value;
                    RaisePropertyChanged(nameof(Votes));
                }
            }
        }

        public bool AddPhotoIsEnabled
        {
            get => _addPhotoIsEnabled;
            set
            {
                _addPhotoIsEnabled = value;
                RaisePropertyChanged(nameof(AddPhotoIsEnabled));
            }
        }

        public byte[] Photo
        {
            get => _book.Photo;
            set
            {
                _book.Photo = value;
                RaisePropertyChanged(nameof(PhotoSource));
            }
        }

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

        public Bookcase Bookcase
        {
            get => _book.Bookcase;
            set
            {
                if (_book.Bookcase != value)
                {
                    _book.Bookcase = value;
                    RaisePropertyChanged();
                }
            }
        }

        public bool Available
        {
            get => _book.Available;
            set
            {
                if (_book.Available != value)
                {
                    _book.Available = value;
                    RaisePropertyChanged();
                }
            }
        }
        
        public DateTime AddingDate => _book.AddingDate;

        /// <summary>
        /// Dropdown selected category
        /// </summary>

        #region Validation

        public bool TitleValidation_ShowMsg
        {
            get => _titleValidationShowMsg;
            set
            {
                _titleValidationShowMsg = value;
                RaisePropertyChanged(nameof(TitleValidation_ShowMsg));
            }
        }

        public bool AuthorsValidation_ShowMsg
        {
            get => _authorsValidationShowMsg;
            set
            {
                _authorsValidationShowMsg = value;
                RaisePropertyChanged(nameof(AuthorsValidation_ShowMsg));
            }
        }

        #endregion

        public ICommand RemoveBookCommand
        {
            get => _removeBookCommand;
            set => _removeBookCommand = value;
        }

        public ICommand AddPhotoCommand
        {
            get => _addPhotoCommand;
            set => _addPhotoCommand = value;
        }

        public ICommand SaveChangesCommand
        {
            get => _saveChangesCommand;
            set => _saveChangesCommand = value;
        }

        public BookViewModel(Book book)
        {
            _book = book;

            RemoveBookCommand = new Command(RemoveBookExecute);
            SaveChangesCommand = new Command(SaveChangesExecute);
            AddPhotoCommand = new Command(AddPhotoExecute);
        }

        private void RemoveBookExecute()
        {
            App.ApiService.RemoveBook(Id);
            OnBookRemoved?.Invoke(Id, EventArgs.Empty);
            App.Navigation.PopAsync();
        }

        private async void AddPhotoExecute(object obj)
        {
            AddPhotoIsEnabled = false;

            Photo = await Utils.TakePhoto();

            AddPhotoIsEnabled = true;
        }

        private void SaveChangesExecute()
        {
            App.ApiService.UpdateBook(_book);
        }

        private Category GetCategoryByString(string categoryName)
        {
            Category categoryEnum = (Category) Enum.Parse(typeof(Category), categoryName);

            return categoryEnum;
        }

        private bool TitleValidation()
        {
            bool result = !string.IsNullOrWhiteSpace(Title);
            TitleValidation_ShowMsg = !result;
            return result;
        }

        private bool AuthorsValidation()
        {
            bool result = !string.IsNullOrWhiteSpace(Authors);
            AuthorsValidation_ShowMsg = !result;
            return result;
        }
    }
}