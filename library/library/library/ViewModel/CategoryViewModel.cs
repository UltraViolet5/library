using System;
using System.Collections.Generic;
using System.Text;
using library.Model;

namespace library.ViewModel
{
    public class CategoryViewModel : BaseViewModel
    {
        private readonly Category _category;

        public string Name
        {
            get => _category.Name;
            set
            {
                _category.Name = value;
                RaisePropertyChanged();
            }
        }

        public CategoryViewModel(Category category)
        {
            _category = category;
        }
    }
}
