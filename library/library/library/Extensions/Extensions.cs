using System;
using System.Collections.Generic;
using System.Text;
using library.Model;
using library.ViewModel;

namespace library.Extensions
{
    public static class Extensions
    {
        public static BookViewModel MapToBookViewModel(this Book book)
        {
            return new BookViewModel(book)
            {
                Title = book.Title,
                Authors = book.Authors,
                Available = book.Available,
                Bookcase = book.Bookcase,
                Categories = book.Categories,
                Owner = book.Owner,
                Picture = book.Picture,
                Read = book.Read,
                Votes = book.Votes
            };
        }
    }
}
