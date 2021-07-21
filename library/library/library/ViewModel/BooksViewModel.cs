using library.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using library.Model;
using Xamarin.Forms;

namespace library.ViewModel
{
    public class BooksViewModel : BaseViewModel
    {
        public event EventHandler OnSortingMethodChange;

        /// <summary>
        /// Parent Instance
        /// </summary>
        // private readonly BooksPage _page;
        public IEnumerable<BookViewModel> Books { get; private set; }
        public String Search { get; set; }

        private SortMethod _sortMethod = SortMethod.TitleAsc;
        public SortMethod SortMethod
        {
            get => _sortMethod;
            set
            {
                _sortMethod = value;
                Books = SortBooksBySortMethod();

                RaisePropertyChanged(nameof(SelectedSortMethod));

                OnSortingMethodChange?.Invoke(this, EventArgs.Empty);
            }

        }
        public List<string> SortMethods => new List<string>(Enum.GetNames(typeof(SortMethod)));
        public string SelectedSortMethod
        {
            get => SortMethod.ToString();
            set
            {
                SortMethod = SortMethodFromString(value);
                RaisePropertyChanged(nameof(SortMethod));
                RaisePropertyChanged(nameof(SelectedSortMethod));
            }
        }
        public ICommand AddBookCommand { get; private set; }
        public ICommand ShowBookCommand { get; private set; }
        
        public BooksViewModel(IEnumerable<BookViewModel> books, BooksPage page)
        {
            Books = books;
            Books = SortBooksBySortMethod();

            AddBookCommand = new Command(AddBookExecute);
            ShowBookCommand = new Command(ShowBookExecute);

            // _page = page;
        }

        private IEnumerable<BookViewModel> SortBooksBySortMethod()
        {
            IEnumerable<BookViewModel> result;

            switch (SortMethod)
            {
                case SortMethod.TitleAsc:
                    result = Books.OrderBy(x => x.Title);
                    break;
                case SortMethod.TitleDesc:
                    result = Books.OrderByDescending(x => x.Title);
                    break;
                case SortMethod.AuthorAsc:
                    result = Books.OrderBy(x => x.Authors);
                    break;
                case SortMethod.AuthorDesc:
                    result = Books.OrderByDescending(x => x.Authors);
                    break;
                case SortMethod.DateAsc:
                    result = Books.OrderBy(x => x.PublishingYear);
                    break;
                case SortMethod.DateDesc:
                    result = Books.OrderByDescending(x => x.PublishingYear);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }

        private void AddBookExecute()
        {
            App.Navigation.PushAsync(new AddBookDataPage());
        }

        private void ShowBookExecute(object obj)
        {
            App.Navigation.PushAsync(new BookPage((int) obj));
        }
        private SortMethod SortMethodFromString(string value)
        {
            SortMethod sortMethod = (SortMethod)Enum.Parse(typeof(SortMethod), value);

            return sortMethod;
        }
    }
}
