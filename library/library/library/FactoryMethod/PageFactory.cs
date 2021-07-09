using System.Collections.Generic;
using library.ViewModel;
using Xamarin.Forms;
using System;
using System.Collections.Generic;
using library.Model;

namespace library.FactoryMethod
{
    class PageFactory : IPageFactory
    {
        private readonly ComponentFactory _componentFactory;

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
                        _componentFactory.GetPhotoBox(),
                        _componentFactory.GetLabel("Title", hAlignment: TextAlignment.Center),
                        _componentFactory.GetEntry("Title"),
                        _componentFactory.GetValidationLabel("Title must be filled.",
                            "TitleValidation_ShowMsg", Color.Red),
                        _componentFactory.GetLabel("Authors", hAlignment: TextAlignment.Center),
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

        public ScrollView GetMyRentalsPage ()
        {
            
            var borrowingElements = getBorrowingElemnts();
            var layout = new StackLayout();

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
            BooksViewModel booksViewModel)
        {
            var stackLayout = new StackLayout()
            {
                Padding = Style.PagePadding,
                Children =
                {
                    _componentFactory.GetHeader(
                        $"{booksOwner.UserName} Books",
                        "AddBookCommand"),
                    _componentFactory.GetEntry("Search",
                        "search"),
                    _componentFactory.GetDropDown("Sort: ",
                        "SortMethods", "SelectedSortMethod")
                    // IMPORTANT!!!
                    // I using forth child to place bookCards, Speak with Jarek
                    // if you want do add next child here
                }
            };

            var result = new ScrollView()
            {
                Content = stackLayout,
            };

            return result;
        }
        /// <summary>
        /// List book cards in stackLayout
        /// </summary>
        /// <param name="stackLayout"></param>
        /// <param name="books"></param>
        public void GetListBookCards(ref StackLayout stackLayout,
            IEnumerable<BookViewModel> books)
        {
            foreach (var book in books)
            {
                stackLayout.Children.Add(_componentFactory.GetBookCard(book));
            }
        }

        private List<StackLayout> getBorrowingElemnts()
        {
            var borrowing = App.DbService.GetBorrowings();

            List<StackLayout> borrowingElemnts = new List<StackLayout>();

            foreach (var item in borrowing)
            {
                var NewLayout = new StackLayout()
                {
                    Children =
                    {
                        _componentFactory.GetBookCard(item.Book),
                        _componentFactory.GetRentalBtn(item)
                    }
                };

                
                borrowingElemnts.Add(NewLayout);
            }

            return borrowingElemnts;
        }
    }
}