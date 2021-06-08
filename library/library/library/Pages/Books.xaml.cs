using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace library.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Books : ContentPage
    {
        public Books()
        {
            InitializeComponent();


            btnAddBook.Clicked += (s,e) => Navigation.PushAsync(new AddBook());


        }
    }
}