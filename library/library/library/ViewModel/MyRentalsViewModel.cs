using library.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace library.ViewModel
{
    public class MyRentalsViewModel : BaseViewModel
    {
        public IEnumerable<Borrowing> Borrowings { get; set; }

        public Borrowing Borrowing { get; set; }

        public List<Data> Mysource { get; set; }

        public MyRentalsViewModel()
        {
           Borrowings = App.DbService.GetBorrowings();
           listViewItemsAdd();
        }

        private void listViewItemsAdd()
        {
            Mysource = new List<Data>();

            foreach (var borrowing in Borrowings)
            {
                Mysource.Add(new Data { Title = borrowing.Book.Title, Author = borrowing.Book.Authors, RentalEnd = borrowing.ReturnDate });
            }

           
        }
 

    }

    public class Data
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime RentalEnd { get; set; }
    }


}
