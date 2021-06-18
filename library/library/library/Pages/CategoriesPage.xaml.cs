using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using library.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace library.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CategoriesPage : ContentPage
    {
        public readonly CategoriesPageViewModel CategoriesPageViewModel;

		public CategoriesPage ()
		{
			InitializeComponent ();

            CategoriesPageViewModel = new CategoriesPageViewModel();
        }

        /// <summary>
        /// Add book categories to main page.
        /// </summary>
        private void DisplayCategories()
        {
            foreach (CategoryViewModel category in CategoriesPageViewModel.Categories)
            {
                var categoriesUI = App.ComponentFactory.CreateCategoryBtn(category);
                var tapGest = new TapGestureRecognizer();
                tapGest.SetBinding(TapGestureRecognizer.CommandProperty, "ShowBooksByCategory");

                categoriesUI.GestureRecognizers.Add(tapGest);
                Categories.Children.Add(categoriesUI);
            }
        }
    }
}