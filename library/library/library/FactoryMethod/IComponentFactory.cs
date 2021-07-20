using System;
using System.Collections.Generic;
using System.Text;
using library.Model;
using Xamarin.Forms;
using library.ViewModel;
using Xamarin.Forms.Shapes;

namespace library.FactoryMethod
{
    public interface IComponentFactory
    {
        Label GetLabel(string text, int fontSize,
            TextAlignment hAlignment = TextAlignment.Start);

        Grid GetHeader(string text, string plusCommand);
        Label GetLabel(string text,
            string binding,
            int fontSize = 16,
            TextAlignment hAlignment = TextAlignment.Start);
        Line GetLine();
        StackLayout GetLabelWithBinding(string label, string binding);
        StackLayout GetCheckBox(string label, string binding);
        StackLayout GetDropDown(string labelText, string bindingSource, string bindingSelected);
        Button GetButton(string text,
            string command = null,
            object commandParameter = null);
        Image GetButtonWithIcon(string pictureName, string tapBinding,
            object commandParameter = null, int size = -1);
        Label GetValidationLabel(string msg, string visibleBinding, Color color);
        StackLayout GetSwitch(string text);
        Frame GetPhotoBox(string plusBtnCommand, string plusBtnParameter,
            string plusBtnIsEnabledBinding, string photoSourceBinding);
        Frame GetBookCard(BookViewModel book);
        Frame GetCategoryBtn(string category);
        Frame GetMateIcon(UserViewModel user);
        Frame GetRentalBtn(BorrowingViewModel borrowing);
        Frame GetEntry(string binding, string placeholder = "",
            bool isPassword = false);

        Frame GetMateCard(UserViewModel mate);
        Frame GetDatePicker(string binding);
    };
}
