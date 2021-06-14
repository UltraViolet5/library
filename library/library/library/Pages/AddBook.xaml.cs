using library.ViewModel;
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
    public partial class AddBook : TabbedPage
    {

        public static AddBookViewModel AddBookViewModel;

        public AddBook()
        {
            InitializeComponent();

            AddBookViewModel = new AddBookViewModel();

        }
    }
}