using System;
using System.Collections.Generic;
using System.Text;
using library.Model;

namespace library.ViewModel
{
    public class BorrowingViewModel : BaseViewModel
    {
        private readonly Borrowing _borrowing;

        public string ReturnDate
        {
            get { return $"{_borrowing.ReturnDate.Year}/" +
                         $"{_borrowing.ReturnDate.Month}/" +
                         $"{_borrowing.ReturnDate.Day}"; }
        }

        public BorrowingViewModel(Borrowing borrowing)
        {
            _borrowing = borrowing;
        }
    }
}