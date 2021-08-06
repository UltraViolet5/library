using System;
using System.Collections.Generic;
using System.Text;
using library.Model;

namespace library.ViewModel
{
    public class BorrowingViewModel : BaseViewModel
    {
        private readonly Borrowing _borrowing;

        public Borrowing Borrowing => _borrowing;
        public string ReturnDate
        {
            get { return $"{_borrowing.ReturnDate.Year}/" +
                         $"{_borrowing.ReturnDate.Month}/" +
                         $"{_borrowing.ReturnDate.Day}"; }
        }

        public string BookTitle
        {
            get
            {
                var book = App.DbService.GetBook(Borrowing.BookId).Result;
                return book.Title;
            }
        }


        public BorrowingViewModel(Borrowing borrowing)
        {
            _borrowing = borrowing;

            
        }
    }
}