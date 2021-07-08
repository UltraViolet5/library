using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace library.FactoryMethod
{
    public interface IPageFactory
    {
        ScrollView GetSettingsPage(bool darkMode = false);
        ScrollView GetBookPage(bool darkMode = false);
        ScrollView GetLoginPage(bool darkMode = false);
    }
}
