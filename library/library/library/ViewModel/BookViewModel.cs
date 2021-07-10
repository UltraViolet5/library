using System;
using System.Collections.Generic;
using library.Model;
using System.Windows.Input;
using Xamarin.Forms;

namespace library.ViewModel
{
    public class BookViewModel : BaseViewModel
    {
        private readonly Book _book;
        private bool _titleValidationShowMsg;
        private bool _authorsValidationShowMsg;

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


        public string Picture
        {
            get => _book.Picture;
            set
            {
                if (_book.Picture != value)
                {
                    _book.Picture = value;
                    RaisePropertyChanged(nameof(Picture));
                }
            }
        }

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

        /// <summary>
        /// Dropdown categories source
        /// </summary>
        public List<string> Categories => new List<string>(Enum.GetNames(typeof(Category)));

        /// <summary>
        /// Dropdown selected category
        /// </summary>
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

        public ICommand SaveChangesCommand { get; set; }

        public BookViewModel(Book book)
        {
            _book = book;

            SaveChangesCommand = new Command(SaveChangesExecute);
        }

        public void RefreshView()
        {
            RaisePropertyChanged(nameof(Title));
            RaisePropertyChanged(nameof(Authors));
            RaisePropertyChanged(nameof(Read));
            RaisePropertyChanged(nameof(Available));
            RaisePropertyChanged(nameof(Category));
        }

        private void SaveChangesExecute()
        {
            var v = (int) Category;
            // TODO
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