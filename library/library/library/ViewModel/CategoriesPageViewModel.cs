using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using library.Model;
using Xamarin.Forms;

namespace library.ViewModel
{
    public class CategoriesPageViewModel : BaseViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; private set; }

        public ICommand AddCategoryCommand { get; private set; }
        
        public CategoriesPageViewModel()
        {
            Categories = App.DbService.GetCategories().Select(x => new CategoryViewModel(x));

            AddCategoryCommand = new Command(AddCategoryExecute);
        }

        private void AddCategoryExecute()
        {
            // TODO Add input box for name of category
        }
    }
}
