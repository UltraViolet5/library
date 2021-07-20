using System;
using System.Collections.Generic;
using System.Text;
using library.Model;
using Xamarin.Forms;

namespace library.FactoryMethod
{
    public interface IPageFactory
    {
        ScrollView GetSettingsPage(bool darkMode = false);
        ScrollView GetBookPage(bool darkMode = false);
        ScrollView GetLoginPage(bool darkMode = false);
        ScrollView GetMyRentalsPage(IEnumerable<Borrowing> borrowings);
    }
}
