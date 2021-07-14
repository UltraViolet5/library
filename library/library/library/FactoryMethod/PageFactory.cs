using System.Collections.Generic;
using library.ViewModel;
using Xamarin.Forms;
using System;
using System.Collections.Generic;
using library.Model;
using library.ViewModel;

namespace library.FactoryMethod
{
    public class PageFactory : IPageFactory
    {
        private readonly IComponentFactory _componentFactory;

        public PageFactory()
        {
            _componentFactory = new ComponentFactory();
        }

        public ScrollView GetSettingsPage(bool darkMode = false)
        {
            var result = new ScrollView()
            {
                Content = new StackLayout()
                {
                    Padding = Style.PagePadding,
                    Children =
                    {
                        _componentFactory.GetLabel("Settings", Style.BigFont),
                        _componentFactory.GetSwitch("DarkMode"),
                        _componentFactory.GetButton("Privacy policy"),
                        _componentFactory.GetButton("Authors"),
                        _componentFactory.GetButton("Logout", "LogoutCommand"),
                    }
                }
            };

            return result;
        }

        public ScrollView GetBookPage(bool darkMode = false)
        {
            var result = new ScrollView()
            {
                Content = new StackLayout()
                {
                    Padding = Style.PagePadding,
                    Children =
                    {
                        _componentFactory.GetPhotoBox("AddPhotoCommand", 
                            "Photo", "AddPhotoIsEnabled", 
                            "PhotoSource"),
                        _componentFactory.GetLabel("Title", Style.BigFont, hAlignment: TextAlignment.Center),
                        _componentFactory.GetEntry("Title"),
                        _componentFactory.GetValidationLabel("Title must be filled.",
                            "TitleValidation_ShowMsg", Color.Red),
                        _componentFactory.GetLabel("Authors", Style.BigFont, hAlignment: TextAlignment.Center),
                        _componentFactory.GetEntry("Authors"),
                        _componentFactory.GetValidationLabel("Authors must be filled.",
                            "AuthorsValidation_ShowMsg", Color.Red),
                        _componentFactory.GetLine(),
                        _componentFactory.GetLabelWithBinding("Owner:", "Owner.UserName"),
                        _componentFactory.GetLabelWithBinding("Placed at:", "BookCase.Name"),
                        _componentFactory.GetCheckBox("read", "Read"),
                        _componentFactory.GetCheckBox("available", "Available"),
                        _componentFactory.GetDropDown("Category:", "Category", "SelectedCategory"),
                        _componentFactory.GetButton("Save changes", "SaveChangesCommand"),
                    }
                }
            };

            return result;
        }

        public ScrollView GetLoginPage(bool darkMode = false)
        {
            var result = new ScrollView()
            {
                Content = new StackLayout()
                {
                    Padding = Style.PagePadding * 3,
                    Children =
                    {
                        new Image()
                        {
                            Source = "logo.png",
                            WidthRequest = Style.PhotoBoxSize,
                            HorizontalOptions = LayoutOptions.Center,
                            Margin = new Thickness(0, 100, 0, 20)
                        },
                        _componentFactory.GetLabel("Login",
                            hAlignment: TextAlignment.Center,
                            fontSize: Style.MediumFont),
                        _componentFactory.GetEntry("Email", "e-mail"),
                        _componentFactory.GetEntry("Password", "password", true),
                        _componentFactory.GetValidationLabel("Incorrect login or password.",
                            "LoginValidation_ShowMsg", Color.Red),
                        _componentFactory.GetButtonWithIcon("arrowfilled.png", "ToMainPageCommand"),
                        _componentFactory.GetLabel("or", Style.MediumFont,
                            TextAlignment.Center),
                        _componentFactory.GetButton("register", "RegisterCommand")
                    }
                }
            };

            return result;
        }

        public View GetMatesPage(MatesViewModel matesViewModel)
        {
            var stackLayout = new StackLayout()
            {
                Padding = Style.PagePadding,
            };
            stackLayout.Children.Add(_componentFactory.GetLabel("Mates",
                Style.BigFont));

            foreach (UserViewModel mate in matesViewModel.Mates)
            {
                stackLayout.Children.Add(_componentFactory.GetMateCard(mate));
            }

            var result = new ScrollView()
            {
                Content = stackLayout,
            };

            return result;
        }

        public ScrollView GetMyRentalsPage()
        {
            var borrowingElements = _componentFactory.GetBorrowingElements();
            var layout = new StackLayout() {Padding = Style.PagePadding};

            foreach (var borrowing in borrowingElements)
            {
                layout.Children.Add(borrowing);
            }

            var result = new ScrollView()
            {
                Content = layout
            };


            return result;
        }

        public ScrollView GetBooksPage(User booksOwner,
            BooksViewModel booksViewModel, bool addBookButton = false)
        {
            View label;

            if (addBookButton)
                label = _componentFactory.GetHeader(
                    $"{booksOwner.UserName} Books",
                    "AddBookCommand");
            else
                label = _componentFactory.GetLabel($"{booksOwner.UserName} Books", 
                    Style.BigFont);

            var stackLayout = new StackLayout()
            {
                Padding = Style.PagePadding,
                Children =
                {
                    label,
                    _componentFactory.GetEntry("Search",
                        "search"),
                    _componentFactory.GetDropDown("Sort: ",
                        "SortMethods", "SelectedSortMethod")
                    // IMPORTANT!!!
                    // I used forth child to place bookCards, Speak with Jarek
                    // if you want do add next child here
                }
            };

            var result = new ScrollView()
            {
                Content = stackLayout,
            };

            return result;
        }

        public ScrollView GetUserPage(bool darkMode = false)
        {
            var result = new ScrollView()
            {
                Content = new StackLayout()
                {
                    Padding = Style.PagePadding,
                    Children =
                    {
                        _componentFactory.GetLabel("User",
                            fontSize: Style.BigFont),
                        _componentFactory.GetPhotoBox("AddPhotoCommand",
                            "Photo", "AddPhotoIsEnabled",
                            "PhotoSource"),
                        _componentFactory.GetLabel("User name",
                            fontSize: Style.MediumFont, TextAlignment.Center),
                        _componentFactory.GetEntry("UserName", "name"),
                        _componentFactory.GetValidationLabel("User name cant't be empty.",
                            "UserNameValidation_ShowMsg", Style.ValidationErrorColor),
                        _componentFactory.GetLabel("Email", Style.MediumFont, TextAlignment.Center),
                        _componentFactory.GetEntry("Email", "e-mail"),
                        _componentFactory.GetValidationLabel("Incorrect email.",
                            "EmailValidation_ShowMsg", Style.ValidationErrorColor),
                        _componentFactory.GetLabel("Birth date", Style.MediumFont, TextAlignment.Center),
                        _componentFactory.GetDatePicker("BirthDate"),
                        _componentFactory.GetLabel("Localization", fontSize: Style.MediumFont, TextAlignment.Center),
                        _componentFactory.GetEntry("Localization", "localization"),
                        _componentFactory.GetValidationLabel("Localization can't be empty.",
                            "LocalizationValidation_ShowMsg", Style.ValidationErrorColor),
                        _componentFactory.GetLabel("Password",
                            Style.MediumFont, TextAlignment.Center),
                        _componentFactory.GetEntry("Password", "password", true),
                        _componentFactory.GetValidationLabel("To change password, provide correct previous password.",
                            "PasswordValidation_ShowMsg", Style.ValidationErrorColor),
                        _componentFactory.GetLabel("New Password",
                            Style.MediumFont, TextAlignment.Center),
                        _componentFactory.GetEntry("NewPassword", "new password", true),
                        _componentFactory.GetValidationLabel("Password can't be empty.",
                            "NewPasswordValidation_ShowMsg", Style.ValidationErrorColor),
                        _componentFactory.GetLabel("Confirm Password",
                            Style.MediumFont, TextAlignment.Center),
                        _componentFactory.GetEntry("ConfirmPassword", "confirm password", true),
                        _componentFactory.GetValidationLabel("Password no match.",
                            "PasswordConfirmValidation_ShowMsg", Style.ValidationErrorColor),
                        _componentFactory.GetValidationLabel("Data updated.", "DataUpdated_ShowMsg",
                            Style.ValidationSuccessColor),
                        _componentFactory.GetButton("Save changes", "SaveChangesCommand"),
                    }
                }
            };

            return result;
        }

        /// <summary>
        /// List book cards in stackLayout. Partial render of books page and main page.
        /// </summary>
        /// <param name="stackLayout"></param>
        /// <param name="books"></param>
        public void ListBookCards(ref StackLayout stackLayout,
            IEnumerable<BookViewModel> books)
        {
            int index = 0;
            foreach (var book in books)
            {
                stackLayout.Children.Add(_componentFactory.GetBookCard(book))
                    ;
                index++;
            }
        }
    }
}