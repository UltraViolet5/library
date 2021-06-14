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
            Grid grid = new Grid()
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition() {Width = new GridLength(3, GridUnitType.Star)},
                    new ColumnDefinition() {Width = new GridLength(7, GridUnitType.Star)}
                }
            };

            var imageFrame = new Frame()
            {
                Padding = 0,
                HeightRequest = 100,
                HasShadow = false,
                Content = new Image()
                {
                    Source = "picture.png",
                    Aspect = Aspect.AspectFill
                }
            };
            imageFrame.SetValue(Grid.ColumnProperty, 0);
            grid.Children.Add(imageFrame);

            var contentBox = new StackLayout()
            {
                Padding = new Thickness(15,10),
                Children =
                {
                    new Label()
                    {
                        Text = book.Title,
                        FontSize = 18,
                        FontFamily = Style.MainFont
                    },
                    new Label()
                    {
                        Text = $"{book.Authors}; {book.PublishingYear}",
                        FontFamily = Style.MainFont
                    },
                    new Label()
                    {
                        Text = $"Placed in: {book.Bookcase}",
                        FontFamily = Style.MainFont
                    },
                    new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new CheckBox()
                            {
                                IsChecked = book.Read,
                                Color = Color.Black,
                                IsEnabled = false
                            },
                            new Label()
                            {
                                Text = "read",
                                VerticalOptions = LayoutOptions.Center,
                                FontFamily = Style.MainFont
                            }
                        }
                    },
                    new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new CheckBox()
                            {
                                IsChecked = book.Read,
                                Color = Color.Black,
                                IsEnabled = false
                            },
                            new Label()
                            {
                                Text = "available",
                                VerticalOptions = LayoutOptions.Center,
                                FontFamily = Style.MainFont
                            }
                        }
                    }
                }
            };
            contentBox.SetValue(Grid.ColumnProperty, 1);
            grid.Children.Add(contentBox);

            Frame result = new Frame()
            {
                BackgroundColor = Style.LightGray,
                Padding = 0,
                Margin = new Thickness(20, 10),
                CornerRadius = 15,
                HasShadow = true,
                Content = new StackLayout()
                {
                    Children =
                    {
                        grid
                    }
                }
            };
            return result;
        }

        public override Frame CreateCategoryBtn(CategoryViewModel category)
        {
            return new Frame()
            {
                Padding = new Thickness(15, 5, 15, 5),
                BackgroundColor = Color.LightGray,
                CornerRadius = 7,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Content = new Label
                {
                    Text = category.Name,
                    FontSize = 18
                }
            };
        }
    }
}