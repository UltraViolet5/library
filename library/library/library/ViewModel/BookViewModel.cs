using System;
using System.Collections.Generic;
using System.Text;
using library.Model;
using System.Linq;

namespace library.ViewModel
{
    public class BookViewModel : BaseViewModel
    {
        private readonly Book _book;

        public string Title
        {
            get => _book.Title;
            set
            {
                if (_book.Title != value)
                {
                    _book.Title = value;
                    RaisePropertyChanged();
                }
            }
        }


        public string Authors
        {
            get
            {
                return _book.Authors;
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
                    RaisePropertyChanged();
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
                    RaisePropertyChanged();
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
                    RaisePropertyChanged();
                }
            }
        }

        public Category Category
        {
            get => _book.Categories;
            set
            {
                _book.Categories = value;
                RaisePropertyChanged();
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
                    RaisePropertyChanged();
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

        public BookViewModel(Book book)
        {
            _book = book;
        }
    }
}
