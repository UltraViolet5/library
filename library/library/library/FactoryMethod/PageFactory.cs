using Xamarin.Forms;
using System;
using System.Collections.Generic;

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
                        _componentFactory.GetLabel("Title", hAlignment:TextAlignment.Center),
                        _componentFactory.GetEntry("Title"),
                        _componentFactory.GetValidationLabel("Title must be filled.",
                            "TitleValidation_ShowMsg", Color.Red),
                        _componentFactory.GetLabel("Authors", hAlignment:TextAlignment.Center),
                        _componentFactory.GetEntry("Authors"),
                        _componentFactory.GetValidationLabel("Authors must be filled.",
                            "AuthorsValidation_ShowMsg", Color.Red),
                        _componentFactory.GetLine(),
                        _componentFactory.GetLabelWithBinding("Owner:", "Owner.UserName"),
                        _componentFactory.GetLabelWithBinding("Placed at:", "BookCase.Name"),
                        _componentFactory.GetCheckBox("read", "Read"),
                        _componentFactory.GetCheckBox("available", "Available"),
                        _componentFactory.GetDropDown("Category:", "Categories", "SelectedCategory"),
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
                            Margin = new Thickness(0,100,0,20)
                        },
                        _componentFactory.GetLabel("Login", 
                            hAlignment:TextAlignment.Center, 
                            fontSize:Style.MediumFont),
                        _componentFactory.GetEntry("Email","e-mail"),
                        _componentFactory.GetEntry("Password", "password", true),
                        _componentFactory.GetValidationLabel("Incorrect login or password.",
                            "LoginValidation_ShowMsg", Color.Red),
                        _componentFactory.GetNextButton("ToMainPageCommand"),
                        _componentFactory.GetLabel("or", Style.MediumFont,
                            TextAlignment.Center),
                        _componentFactory.GetButton("register", "RegisterCommand")
                    }
                }
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
