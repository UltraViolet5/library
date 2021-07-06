using System;
using System.Collections.Generic;
using System.Text;
using library.Model;
using Xamarin.Forms;
using library.ViewModel;

namespace library.FactoryMethod
{
    public interface IComponentFactory
    {
        Label CreateLabel(string text, int fontSize = 12,
            TextAlignment hAlignment = TextAlignment.Start);
        Button CreateButton(string text, string command);
        StackLayout CreateSwitch(string text);
        Frame CreateBookCard(BookViewModel book);
        Frame CreateCategoryBtn(string category);
        Frame CreateMateIcon(UserViewModel user);
        Frame CreateRentalBtn(BorrowingViewModel borrowing);
        Frame CreateFrameWithEntry();
    }
}
