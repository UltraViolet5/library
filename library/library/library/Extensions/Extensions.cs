using System;
using System.Collections.Generic;
using System.Text;
using library.Model;
using library.ViewModel;

namespace library.Extensions
{
    public static class Extensions
    {
         public static BookViewModel ToBookViewModel (this Book book)
        {
            return new BookViewModel(book);
        }
    }
}
