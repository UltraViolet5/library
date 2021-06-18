using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using library.Pages;
using library.Services;
using Xamarin.Forms;
using library.Model;
using library.FactoryMethod;


namespace library.ViewModel
{
    public class AddBookViewModel : BaseViewModel
    {
        public static ZXing.Result Result = null;

        public ICommand SaveButton { get; set; }
        public ICommand AddAutor { get; set; }

        public string Title { get; set; }
        public List<Author> Autors;
        public DatePicker PublishingYear { get; set; }
        public List<Category> Categories;

       

        public AddBookViewModel()
        {
            RaisePropertyChanged("Result");

            SaveButton = new Command(SaveButtonExecute);
            AddAutor = new Command(AddAutorExecute);
            

        }

        private void AddAutorExecute(object obj)
        {
            
        }
        

        private void SaveButtonExecute(object obj)
        {
            var NewBook = new Book();
            
            
           

        }

        

        

       
    }
}
