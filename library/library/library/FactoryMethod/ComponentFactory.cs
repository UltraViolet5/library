using System;
using library.Model;
using Xamarin.Forms;
using library.ViewModel;
using Xamarin.Forms.Shapes;
using Rectangle = Xamarin.Forms.Rectangle;

namespace library.FactoryMethod
{
    public class ComponentFactory : IComponentFactory
    {
        public Label GetLabel(string text,
            int fontSize = 16,
            TextAlignment hAlignment = TextAlignment.Start)
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

        public Label GetLabel(string text,
            Thickness margin,
            int fontSize = 16,
            TextAlignment hAlignment = TextAlignment.Start)
        {
            return new Label()
            {
                Padding = margin,
                Text = text,
                FontFamily = Style.MainFont,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = hAlignment,
                FontSize = fontSize,
                Margin = margin
            };
        }

        public Label GetLabel(string text,
            string binding,
            int fontSize = 16,
            TextAlignment hAlignment = TextAlignment.Start)
        {
            var result = new Label()
            {
                Padding = new Thickness(10, 10),
                Text = text,
                FontFamily = Style.MainFont,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = hAlignment,
                FontSize = fontSize
            };

            result.SetBinding(Label.TextProperty, binding);

            return result;
        }

        /// <summary>
        /// Create header with label and plus button on right.
        /// </summary>
        /// <param name="text">Label text</param>
        /// <param name="plusCommand">Plus button command</param>
        /// <returns></returns>
        public Grid GetHeader(string text, string plusCommand)
        {
            var result = new Grid()
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition() {Width = new GridLength(8, GridUnitType.Star)},
                    new ColumnDefinition() {Width = new GridLength(2, GridUnitType.Star)}
                }
            };

            var label = GetLabel(text, fontSize: Style.BigFont);
            label.SetValue(Grid.ColumnProperty, 0);
            result.Children.Add(label);

            var icon = GetButtonWithIcon("plus.png",
                plusCommand, size: 20);
            icon.SetValue(Grid.ColumnProperty, 1);
            result.Children.Add(icon);

            return result;
        }

        public Label GetValidationLabel(string msg, string visibleBinding, Color color)
        {
            var result = new Label()
            {
                Text = msg,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = color,
                FontFamily = Style.MainFont
            };
            result.SetBinding(Label.IsVisibleProperty, visibleBinding);

            return result;
        }

        public Button GetButton(string text,
            string command = null,
            object commandParameter = null)
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

            if (commandParameter != null)
                result.CommandParameter = commandParameter;

            return result;
        }

        public Button GetButton(string text,
            Color color,
            string command = null,
            object commandParameter = null)
        {
            var result = new Button()
            {
                Text = text,
                FontFamily = Style.MainFont,
                CornerRadius = Style.MediumCornerRadius,
                BackgroundColor = color,
            };

            if (!String.IsNullOrWhiteSpace(command))
                result.SetBinding(Button.CommandProperty, command);

            if (commandParameter != null)
                result.CommandParameter = commandParameter;

            return result;
        }

        public StackLayout GetSwitch(string text)
        {
            return new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    GetLabel(text, Style.MediumFont),
                    new Switch()
                }
            };
        }

        public Frame GetPhotoBox(string plusBtnCommand, string plusBtnParameter,
            string plusBtnIsEnabledBinding, string photoSourceBinding)
        {
            var absoluteLayout = new AbsoluteLayout();

            var img = new Image()
            {
                Scale = 1,
                Aspect = Aspect.AspectFill
            };
            img.SetBinding(Image.SourceProperty, photoSourceBinding);

            var frame = new Frame()
            {
                WidthRequest = Style.PhotoBoxSize,
                HeightRequest = Style.PhotoBoxSize,
                Padding = new Thickness(0),
                Content = img,
            };
            ;

            var addBtn = new Image()
            {
                Source = "plusfilled.png",
                WidthRequest = Style.ImageButtonSize,
                HeightRequest = Style.ImageButtonSize,
            };
            var tapGesture = new TapGestureRecognizer();
            tapGesture.SetBinding(TapGestureRecognizer.CommandProperty, plusBtnCommand);
            tapGesture.SetBinding(TapGestureRecognizer.CommandParameterProperty, plusBtnParameter);
            addBtn.GestureRecognizers.Add(tapGesture);
            addBtn.SetBinding(Image.IsEnabledProperty, plusBtnIsEnabledBinding);

            absoluteLayout.Children.Add(frame);
            absoluteLayout.Children.Add(addBtn,
                new Rectangle(0.9, 0.9, addBtn.Width, addBtn.Height),
                AbsoluteLayoutFlags.PositionProportional);

            var result = new Frame()
            {
                WidthRequest = Style.PhotoBoxSize,
                HeightRequest = Style.PhotoBoxSize,
                CornerRadius = Style.BigCornerRadius,
                Padding = new Thickness(0),
                Margin = new Thickness(40, 20),
                HasShadow = true,
                BorderColor = Style.LightGray,
                HorizontalOptions = LayoutOptions.Center,
                Content = absoluteLayout
            };

            return result;
        }

        public Frame GetBookCard(BookViewModel book)
        {
            Grid grid = new Grid()
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition() {Width = new GridLength(3, GridUnitType.Star)},
                    new ColumnDefinition() {Width = new GridLength(7, GridUnitType.Star)}
                }
            };

            var img = new Image()
            {
                Source = book.PhotoSource,
                Aspect = Aspect.AspectFill
            };

            var imageFrame = new Frame()
            {
                Padding = 0,
                HeightRequest = 100,
                HasShadow = false,
                Content = img,
            };
            imageFrame.SetValue(Grid.ColumnProperty, 0);
            grid.Children.Add(imageFrame);

            var contentBox = new StackLayout()
            {
                Padding = new Thickness(10, 10),
                Children =
                {
                    GetLabel(book.Title, new Thickness( 0), fontSize: 18),
                    GetLabel($"{book.Authors}; {book.PublishingYear}", new Thickness(0),
                        fontSize: Style.SmallFont),
                    GetLabel($"Placed in: {book.Bookcase}", new Thickness(0), fontSize: Style.SmallFont),
                    GetLabel($"Category: {Enum.GetName(typeof(Category), book.Category)}",
                        new Thickness( 0),
                        fontSize: Style.SmallFont),
                    new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Margin = new Thickness( 0),
                        Padding = new Thickness(0),
                        Children =
                        {
                            new CheckBox()
                            {
                                IsChecked = book.Read,
                                Color = Color.Gray,
                                IsEnabled = false,
                                Margin = new Thickness(0)
                            },
                            new Label()
                            {
                                Text = "read",
                                VerticalOptions = LayoutOptions.Center,
                                FontFamily = Style.MainFont,
                                FontSize = Style.SmallFont,
                                Margin = new Thickness(0)
                            }
                        }
                    },
                    new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Margin = new Thickness(0),
                        Padding = new Thickness(0),
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
                                FontFamily = Style.MainFont,
                                FontSize = Style.SmallFont
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
            tapGesture.CommandParameter = book;
            result.GestureRecognizers.Add(tapGesture);

            return result;
        }


        public Frame GetCategoryBtn(string category)
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

        public Frame GetMateIcon(UserViewModel user, string boundCommand = null)
        {
            var image = new Image()
            {
                Source = user.PhotoSource,
                Margin = 0,
                HeightRequest = Style.MateIconSize,
                WidthRequest = Style.MateIconSize,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Aspect = Aspect.AspectFill
            };

            var result = new Frame()
            {
                CornerRadius = Style.MediumCornerRadius,
                WidthRequest = Style.MateIconSize,
                HeightRequest = Style.MateIconSize,
                Padding = 0,
                HasShadow = false,
                Content = image
            };

            if (!String.IsNullOrWhiteSpace(boundCommand))
            {
                var tapGesture = new TapGestureRecognizer();
                tapGesture.SetBinding(TapGestureRecognizer.CommandProperty, boundCommand);
                result.GestureRecognizers.Add(tapGesture);
            }

            return result;
        }

        public Frame GetRentalBtn(BorrowingViewModel borrowing)
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
            grid.Children.Add(
                new Image()
                {
                    Source = "calender.png",
                    WidthRequest = 15,
                    HeightRequest = 15,
                    HorizontalOptions = LayoutOptions.Start
                }
            );

            var stack = new StackLayout()
            {
                Children =
                {
                    new Label()
                    {
                        Text = $"{borrowing.BookTitle}",
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        HorizontalTextAlignment = TextAlignment.Start,
                        FontFamily = Style.MainFont,
                        FontSize = Style.MediumFont
                    },
                    new Label()
                    {
                        Text = $"Return at {borrowing.ReturnDate}",
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        HorizontalTextAlignment = TextAlignment.Start,
                        FontFamily = Style.MainFont,
                        FontSize = Style.SmallFont
                    },
                }
            };
            stack.SetValue(Grid.ColumnProperty, 1);
            grid.Children.Add(stack);

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
                Padding = new Thickness(15, 5),
                CornerRadius = Style.SmallCornerRadius,
                Content = grid
            };

            var tapGest = new TapGestureRecognizer();
            tapGest.SetBinding(TapGestureRecognizer.CommandProperty, new Binding("ShowBorrowingCommand"));
            tapGest.CommandParameter = borrowing.Borrowing;
            frame.GestureRecognizers.Add(tapGest);

            return frame;
        }


        public Frame GetEntry(string binding,
            string placeholder = "",
            bool isPassword = false)
        {
            var newFrame = new Frame
            {
                CornerRadius = Style.MediumCornerRadius,
                Padding = 0,
            };

            var entry = new Entry
            {
                BackgroundColor = Color.White,
                FontFamily = Style.MainFont,
                HorizontalTextAlignment = TextAlignment.Center,
                Placeholder = placeholder,
                IsPassword = isPassword
            };
            entry.SetBinding(Entry.TextProperty, binding);

            newFrame.Content = entry;
            return newFrame;
        }

        public Line GetLine()
        {
            // TODO Not work
            return new Line()
            {
                Stroke = new SolidColorBrush(new Color(0, 0, 0)),
                StrokeThickness = 1,
                StrokeLineCap = PenLineCap.Round,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                X1 = 0, X2 = 0,
                Y1 = 1, Y2 = 1,
                Margin = new Thickness(20)
            };
        }

        public StackLayout GetLabelWithBinding(string label, string binding)
        {
            var result = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Children =
                {
                    GetLabel(label),
                }
            };

            var boundLabel = GetLabel("");
            boundLabel.SetBinding(Label.TextProperty, binding);
            result.Children.Add(boundLabel);

            return result;
        }

        public StackLayout GetCheckBox(string label, string binding)
        {
            var result = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };

            var checkBox = new CheckBox()
            {
                Color = new Color(0, 0, 0)
            };
            checkBox.SetBinding(CheckBox.IsCheckedProperty, binding);

            result.Children.Add(checkBox);
            result.Children.Add(GetLabel(label));

            return result;
        }

        public StackLayout GetDropDown(string labelText, string bindingSource, string bindingSelected)
        {
            var result = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            var label = GetLabel(labelText, fontSize: Style.BigFont, hAlignment: TextAlignment.Center);
            label.HorizontalOptions = LayoutOptions.End;
            result.Children.Add(label);

            var frame = new Frame()
            {
                Padding = new Thickness(0),
                CornerRadius = Style.MediumCornerRadius,
                Margin = new Thickness(0, 5)
            };

            var picker = new Picker()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Title = labelText,
                FontFamily = Style.MainFont,
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor = Style.LightGray,
                FontSize = Style.SmallFont,
                WidthRequest = 150
            };
            picker.SetBinding(Picker.ItemsSourceProperty, bindingSource);
            picker.SetBinding(Picker.SelectedItemProperty, bindingSelected);
            frame.Content = picker;

            result.Children.Add(frame);

            return result;
        }

        public Image GetButtonWithIcon(string pictureName, string tapBinding,
            object commandParameter = null, int size = -1)
        {
            var result = new Image()
            {
                Source = pictureName,
                HorizontalOptions = LayoutOptions.End,
                Margin = new Thickness(0),
                WidthRequest = Style.ImageButtonSize
            };
            if (size > 0)
                result.WidthRequest = size;

            var tapGesture = new TapGestureRecognizer();
            tapGesture.SetBinding(TapGestureRecognizer.CommandProperty, tapBinding);
            tapGesture.CommandParameter = commandParameter;
            result.GestureRecognizers.Add(tapGesture);

            return result;
        }

        public Frame GetMateCard(UserViewModel mate)
        {
            Grid grid = new Grid()
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition() {Width = new GridLength(8, GridUnitType.Star)},
                    new ColumnDefinition() {Width = new GridLength(2, GridUnitType.Star)}
                }
            };

            var imageFrame = GetMateIcon(mate);
            imageFrame.HorizontalOptions = LayoutOptions.CenterAndExpand;
            var imageContainer = new StackLayout()
            {
                Padding = 10,
                Children =
                {
                    imageFrame
                }
            };

            imageContainer.SetValue(Grid.ColumnProperty, 1);
            grid.Children.Add(imageContainer);

            var contentBox = new StackLayout()
            {
                Padding = new Thickness(15, 10),
                Children =
                {
                    GetLabel(mate.UserName),
                    GetLabel(mate.Email, fontSize: Style.SmallFont),
                    GetLabel("Birth date: " + mate.BirthDate, fontSize: Style.SmallFont),
                    GetLabel($"Localization: {mate.Localization}", fontSize: Style.SmallFont),
                    GetLabel($"Books count: {mate.BooksCount}"),
                }
            };
            contentBox.SetValue(Grid.ColumnProperty, 0);
            grid.Children.Add(contentBox);

            Frame result = new Frame()
            {
                BackgroundColor = Color.White,
                Padding = 0,
                Margin = new Thickness(0, 10),
                CornerRadius = Style.BigCornerRadius,
                HasShadow = true,
                Content = new StackLayout()
                {
                    Children =
                    {
                        grid,
                        GetButton("Books",
                            command: "ShowBooksCommand",
                            commandParameter: mate.Id)
                    }
                }
            };

            return result;
        }

        public Frame GetDatePicker(string binding)
        {
            var datePicker = new DatePicker()
            {
                FontFamily = Style.MainFont,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            datePicker.SetBinding(DatePicker.DateProperty, binding);

            var result = new Frame()
            {
                Padding = new Thickness(0),
                CornerRadius = Style.MediumCornerRadius,
                Margin = new Thickness(0, 10),
                BackgroundColor = Style.LightGray,
                Content = datePicker
            };

            return result;
        }
    }
}