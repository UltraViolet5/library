using System;
using Xamarin.Forms;
using library.ViewModel;

namespace library.FactoryMethod
{
    public class ComponentFactory : IComponentFactory
    {
        public Label CreateLabel(string text, int fontSize = 12, TextAlignment hAlignment = TextAlignment.Start)
        {
            return new Label()
            {
                Padding = new Thickness(10, 10),
                Text = text,
                FontFamily = Style.MainFont,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = hAlignment,
                FontSize = fontSize
            };
        }

        public Button CreateButton(string text, string command = null)
        {
            var result = new Button()
            {
                Text = text,
                FontFamily = Style.MainFont,
                CornerRadius = Style.MediumCornerRadius,
                BackgroundColor = Style.LightGray,
            };
            if (!String.IsNullOrWhiteSpace(command))
                result.SetBinding(Button.CommandProperty, command);
            
            return result;
        }

        public StackLayout CreateSwitch(string text)
        {
            return new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    CreateLabel(text, Style.MediumFont),
                    new Switch()
                }
            };
        }

        public Frame CreateBookCard(BookViewModel book)
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
                                Color = Color.Gray,
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
                                IsChecked = book.Available,
                                Color = Color.Gray,
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
                ClassId = book.Id.ToString(),
                BackgroundColor = Style.LightGray,
                Padding = 0,
                Margin = new Thickness(0, 10),
                CornerRadius = Style.BigCornerRadius,
                HasShadow = true,
                Content = new StackLayout()
                {
                    Children =
                    {
                        grid
                    }
                }
            };

            var tapGesture = new TapGestureRecognizer();
            tapGesture.SetBinding(TapGestureRecognizer.CommandProperty, "ShowBookCommand");
            tapGesture.CommandParameter = book.Id;
            result.GestureRecognizers.Add(tapGesture);

            return result;
        }

        public Frame CreateCategoryBtn(string category)
        {
            return new Frame()
            {
                Padding = new Thickness(15, 5, 15, 5),
                BackgroundColor = Color.LightGray,
                CornerRadius = Style.SmallCornerRadius,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Content = new Label
                {
                    Text = category,
                    FontSize = 18,
                    FontFamily = Style.MainFont
                }
            };
        }

        public Frame CreateMateIcon(UserViewModel user)
        {
            return new Frame()
            {
                CornerRadius = Style.MediumCornerRadius,
                WidthRequest = Style.MateIconSize,
                HeightRequest = Style.MateIconSize,
                Padding = 0,
                HasShadow = false,
                Content = new Image()
                {
                    Source = "user.png",
                    Margin = 0,
                    HeightRequest = Style.MateIconSize,
                    WidthRequest = Style.MateIconSize,
                    HorizontalOptions = LayoutOptions.StartAndExpand
                }
            };
        }

        public Frame CreateRentalBtn(BorrowingViewModel borrowing)
        {
            var grid = new Grid()
            {
                ColumnDefinitions = 
                {
                    new ColumnDefinition() {Width = new GridLength(1, GridUnitType.Star)},
                    new ColumnDefinition() {Width = new GridLength(5, GridUnitType.Star)},
                    new ColumnDefinition() {Width = new GridLength(2, GridUnitType.Star)}
                }
            };
            grid.Children.Add(new Image()
            {
                Source = "calender.png",
                WidthRequest = 15,
                HeightRequest = 15,
                HorizontalOptions = LayoutOptions.Start
            });

            var label = new Label()
            {
                Text = $"Return at {borrowing.ReturnDate}",
                HorizontalOptions = LayoutOptions.StartAndExpand,
                HorizontalTextAlignment = TextAlignment.Start,
                FontFamily = Style.MainFont
            };
            label.SetValue(Grid.ColumnProperty, 1);
            grid.Children.Add(label);

            var arrow = new Image()
            {
                Source = "arrow.png",
                HorizontalOptions = LayoutOptions.End,
                WidthRequest = 10
            };
            arrow.SetValue(Grid.ColumnProperty, 2);
            grid.Children.Add(arrow);

            var frame = new Frame()
            {
                Padding = new Thickness(15,5),
                CornerRadius = Style.SmallCornerRadius,
                Content = grid
            };

            return frame;
        }

        public Frame CreateFrameWithEntry()
        {
            var newFrame = new Frame
            {
                CornerRadius = Style.MediumCornerRadius,
                Margin = 0.10,
                Content = new Entry
                {
                    BackgroundColor = Color.White,
                    WidthRequest = 150,
                    FontFamily = Style.MainFont
                }
            };

            return newFrame;
        }
    }
}