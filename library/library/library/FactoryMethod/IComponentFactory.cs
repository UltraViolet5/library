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
        Label GetLabel(string text, int fontSize,
            TextAlignment hAlignment = TextAlignment.Start);
        Button GetButton(string text, string command);
        Label GetValidationLabel(string msg, string visibleBinding, Color color);
        StackLayout GetSwitch(string text);
        Frame GetPhotoBox();
        Frame GetBookCard(BookViewModel book);
        Frame GetCategoryBtn(string category);
        Frame GetMateIcon(UserViewModel user);
        Frame GetRentalBtn(BorrowingViewModel borrowing);
        Frame GetEntry(string binding, string placeholder = "",
            bool isPassword = false);
    }
}
