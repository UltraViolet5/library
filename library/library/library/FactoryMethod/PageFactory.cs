using Xamarin.Forms;

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
                        _componentFactory.CreateLabel("Settings", Style.BigFont),
                        _componentFactory.CreateSwitch("DarkMode"),
                        _componentFactory.CreateButton("Privacy policy"),
                        _componentFactory.CreateButton("Authors"),
                        _componentFactory.CreateButton("Logout", "LogoutCommand"),
                    }
                }
            };

            return result;
        }
    }
}
