using Xamarin.Forms;
using library.ViewModel;

namespace library.FactoryMethod
{
    public class ComponentFactory : ComponentFactoryBase
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

            var tapGesture = new TapGestureRecognizer();
            tapGesture.SetBinding(TapGestureRecognizer.CommandProperty, "ShowBookCommand");
            tapGesture.CommandParameter = book.Id;
            result.GestureRecognizers.Add(tapGesture);

            return result;
        }

        public override Frame CreateCategoryBtn(string category)
        {
            return new Frame()
            {
                Padding = new Thickness(15, 5, 15, 5),
                BackgroundColor = Color.LightGray,
                CornerRadius = 7,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Content = new Label
                {
                    Text = category,
                    FontSize = 18,
                    FontFamily = Style.MainFont
                }
            };
        }

        public override Frame CreateMateIcon(UserViewModel user)
        {
            return new Frame()
            {
                CornerRadius = 10,
                WidthRequest = 50,
                HeightRequest = 50,
                Padding = 0,
                HasShadow = false,
                Content = new Image()
                {
                    Source = "user.png",
                    Margin = 0,
                    HeightRequest = 50,
                    WidthRequest = 50,
                    HorizontalOptions = LayoutOptions.StartAndExpand
                }
            };
        }

        public override Frame CreateRentalBtn(BorrowingViewModel borrowing)
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
                CornerRadius = 7,
                Content = grid
            };

            return frame;
        }

        public override Frame CreateFrameWithEntry()
        {
            var newFrame = new Frame
            {
                CornerRadius = 12,
                Margin = 0.10,
                Content = new Entry
                {
                    BackgroundColor = Color.White,
                    WidthRequest = 150,
                    FontFamily = "News701"
                }
            };

            return newFrame;
        }
    }
}