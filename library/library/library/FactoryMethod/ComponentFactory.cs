﻿using System;
using Xamarin.Forms;
using library.ViewModel;
using Xamarin.Forms.Shapes;
using Rectangle = Xamarin.Forms.Rectangle;
using library.Model;
using System.Collections.Generic;

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
                    new ColumnDefinition() {Width = new GridLength(5, GridUnitType.Star)},
                    new ColumnDefinition() {Width = new GridLength(5, GridUnitType.Star)}
                }
            };

            var label = GetLabel(text);
            label.SetValue(Grid.ColumnProperty, 0);
            result.Children.Add(label);

            var icon = GetButtonWithIcon("plus.png", 
                plusCommand);
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

        public Frame GetPhotoBox()
        {
            var absoluteLayout = new AbsoluteLayout();

            var image = new Frame()
            {
                WidthRequest = Style.PhotoBoxSize,
                HeightRequest = Style.PhotoBoxSize,
                Padding = new Thickness(0),
                Content = new Image()
                {
                    Source = "picture.png",
                    Scale = 1,
                    Aspect = Aspect.Fill
                }
            };

            var addBtn = new Image()
            {
                Source = "plusfilled.png",
                WidthRequest = Style.ImageButtonSize,
                HeightRequest = Style.ImageButtonSize,
            };

            absoluteLayout.Children.Add(image);
            absoluteLayout.Children.Add(addBtn,
                new Rectangle(0, 0, 0.9, 0.9),
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
                Padding = new Thickness(15, 10),
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

        public Frame GetBookCard(Book book)
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
                Padding = new Thickness(15, 10),
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

        public Frame GetMateIcon(UserViewModel user)
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
                Padding = new Thickness(15, 5),
                CornerRadius = Style.SmallCornerRadius,
                Content = grid
            };

            return frame;
        }

        public Frame GetRentalBtn(Borrowing borrowing)
        {
            var grid = new Grid()
            {
                ColumnDefinitions =
                {

                    new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition() { Width = new GridLength(5, GridUnitType.Star) },
                    new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) },

                },
                RowDefinitions =
                {
                    new RowDefinition(),
                    new RowDefinition()
                }
                
                
            };
            grid.Children.Add(new Image()
            {
                Source = "calender.png",
                WidthRequest = 15,
                HeightRequest = 15,
                HorizontalOptions = LayoutOptions.Start
            });

            var titleLabel = new Label()
            {
                Text = borrowing.Book.Title,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                HorizontalTextAlignment = TextAlignment.Start,
                FontFamily = Style.MainFont
            };
            titleLabel.SetValue(Grid.ColumnProperty, 1);
            titleLabel.SetValue(Grid.RowProperty, 0);
            grid.Children.Add(titleLabel);


            var label = new Label()
            {
                Text = $"Return at {borrowing.ReturnDate}",
                HorizontalOptions = LayoutOptions.StartAndExpand,
                HorizontalTextAlignment = TextAlignment.Start,
                FontFamily = Style.MainFont
            };
            label.SetValue(Grid.ColumnProperty, 1);
            label.SetValue(Grid.RowProperty, 1);
            grid.Children.Add(label);




            var frame = new Frame()
            {
                Padding = new Thickness(15, 5),
                CornerRadius = Style.SmallCornerRadius,
                Content = grid,


            };

            var tapgest = new TapGestureRecognizer();
            tapgest.SetBinding(TapGestureRecognizer.CommandProperty, new Binding("ShowBorrowingCommand"));
            tapgest.CommandParameter = borrowing;
            frame.GestureRecognizers.Add(tapgest);


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
            object commandParameter = null)
        {
            var result = new Image()
            {
                Source = pictureName,
                HorizontalOptions = LayoutOptions.End,
                Margin = new Thickness(0),
                WidthRequest = Style.ImageButtonSize
            };
            var tapGesture = new TapGestureRecognizer();
            tapGesture.SetBinding(TapGestureRecognizer.CommandProperty, tapBinding);
            tapGesture.CommandParameter = commandParameter;
            result.GestureRecognizers.Add(tapGesture);

            return result;
        }

        public List<Frame> GetBorrowingElements()
        {
            var borrowings = App.DbService.GetBorrowings();

            List<Frame> borrowingElements = new List<Frame>();

            foreach (var borrowing in borrowings)
            {
                Frame frame = GetRentalBtn(borrowing);
                borrowingElements.Add(frame);
            }

            return borrowingElements;
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
                            "ShowBooksCommand",
                            mate.Id)
                    }
                }
            };

            return result;
        }
    }
}