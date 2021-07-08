using System;
using System.Collections.Generic;
using System.Text;
using library.Model;
using Xamarin.Forms;
using library.ViewModel;

namespace library.FactoryMethod
{
    public abstract class ComponentFactoryBase
    {
        public abstract Frame CreateBookCard(BookViewModel book);
        public abstract Frame CreateBookCard(Book book);
        public abstract Frame CreateCategoryBtn(string category);
        public abstract Frame CreateMateIcon(UserViewModel user);
        public abstract Frame CreateRentalBtn(BorrowingViewModel borrowing);
        public abstract Frame CreateFrameWithEntry();
        public abstract Frame CreateRentalBtn(Borrowing borrowing);


    }
}
