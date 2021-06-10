using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using library.ViewModel;

namespace library.FactoryMethod
{
    public abstract class ComponentFactoryBase
    {
        public abstract Frame CreateBookCard(BookViewModel book);
    }
}
