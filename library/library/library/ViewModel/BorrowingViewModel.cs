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

        private string _bookTitle;
        public string BookTitle => _bookTitle;

        public BorrowingViewModel(Borrowing borrowing)
        {
            _borrowing = borrowing;
            _bookTitle = borrowing.BookTitle;
        }
    }
}