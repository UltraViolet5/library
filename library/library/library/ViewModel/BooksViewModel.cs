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
        public event EventHandler OnCategoryFilterChange;

        /// <summary>
        /// Parent Instance
        /// </summary>
        // private readonly BooksPage _page;
        public List<BookViewModel> Books { get; private set; }

        public List<BookViewModel> AllBooks { get; private set; }
        public String Search { get; set; }

        #region Sorting

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
        #endregion

        #region Category

        public string SelectedCategory
        {
            get => Category.ToString();
            set
            {
                Category = CategoryFromString(value);
                RaisePropertyChanged(nameof(Category));
                RaisePropertyChanged(nameof(SelectedSortMethod));
            }
        }

        private CategoryForFiltering _category = CategoryForFiltering.NoFilter;
        public CategoryForFiltering Category
        {
            get => _category;
            set
            {
                _category = value;
                FilterBooksByCategory();

                RaisePropertyChanged(nameof(SelectedCategory));

                OnCategoryFilterChange?.Invoke(this, EventArgs.Empty);
            }
        }
        
        public List<string> Categories => new List<string>(Enum.GetNames(typeof(CategoryForFiltering)));

        #endregion

        public ICommand AddBookCommand { get; private set; }
        public ICommand ShowBookCommand { get; private set; }

        private readonly BooksPage _page;

        public BooksViewModel(IEnumerable<BookViewModel> books, BooksPage page)
        {
            Books = books.ToList();
            AllBooks = books.ToList();
            Books = SortBooksBySortMethod();

            AddBookCommand = new Command(AddBookExecute);
            ShowBookCommand = new Command(ShowBookExecute);

            _page = page;
        }
        
        private void AddBookExecute()
        {
            var addBookPage = new AddBookDataPage();
            addBookPage.AddBookViewModel.OnBookAdded += HandleBookAdded;
            App.Navigation.PushAsync(addBookPage);
        }

        private void ShowBookExecute(object obj)
        {
            App.Navigation.PushAsync(new BookPage((BookViewModel) obj));
        }
        private SortMethod SortMethodFromString(string value)
        {
            SortMethod sortMethod = (SortMethod)Enum.Parse(typeof(SortMethod), value);

            return sortMethod;
        }

        private CategoryForFiltering CategoryFromString(string value)
        {
            CategoryForFiltering category = (CategoryForFiltering)Enum.Parse(typeof(CategoryForFiltering), value);

            return category;
        }

        private void HandleBookAdded(object sender, EventArgs e)
        {
            Books.Add(new BookViewModel((Book) sender));
            _page.RefreshBooks();
        }

        private List<BookViewModel> SortBooksBySortMethod()
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

            return result.ToList();
        }

        private void FilterBooksByCategory()
        {
            if (Category != CategoryForFiltering.NoFilter)
            {
                Category category = (Category)Category;
                Books = AllBooks.Where(b => b.Category == category).ToList();
            }
            else
            {
                Books = AllBooks.Where(x => true).ToList();
            }
        }
    }
}
