using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using library.ViewModel;

namespace library.FactoryMethod
{
    class ComponentFactory : ComponentFactoryBase
    {
        public override Frame CreateBookCard(BookViewModel book)
        {
            Frame result = new Frame()
            {
                BackgroundColor = Color.LightGray,
                Margin = new Thickness(20, 10),
                CornerRadius = 5,
                HasShadow = true,
                Content = new StackLayout()
                {
                    Children =
                    {
                        new Label() {Text = book.Title},
                        new Label() {Text = book.Authors}
                    }

                }
            };
            return result;
        }

        public override Frame CreateCategoryBtn(CategoryViewModel category)
        {
            return new Frame()
            {
                Padding = new Thickness(15,0,15,0),
                CornerRadius = 8,
                Content = new Label
                {
                    Text = category.Name
                }
            };
        }
    }
}
