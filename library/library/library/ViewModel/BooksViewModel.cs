using library.Pages;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using library.Model;
using Xamarin.Forms;

namespace library.ViewModel
{
    public class BooksViewModel : BaseViewModel
    {
        

        public IEnumerable<BookViewModel> Books { get; private set; }
        public String Search { get; set; }

        private SortMethod _sortMethod = SortMethod.NameAsc;
        public SortMethod SortMethod
        {
            get => _sortMethod;
            set
            {
                _sortMethod = value;
                RaisePropertyChanged(nameof(SelectedSortMethod));
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
        
        public BooksViewModel(IEnumerable<BookViewModel> books)
        {
            Books = books;

            AddBookCommand = new Command(AddBookExecute);
            ShowBookCommand = new Command(ShowBookExecute);
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
