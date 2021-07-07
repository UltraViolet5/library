using Xamarin.Forms;

namespace library
{
    public static class Style
    {
        public static bool DarkMode { get; set; } = false;

        public static string MainFont => "News701";
        public static Color LightGray => Xamarin.Forms.Color.FromHex("#e6e6e6");
        public static int SmallCornerRadius => 8;
        public static int MediumCornerRadius => 12;
        public static int BigCornerRadius => 15;
        public static int MateIconSize => 50;
        public static int SmallFont => 12;
        public static int MediumFont => 16;
        public static int BigFont => 24;
        public static int PagePadding => 20;
        public static int PhotoBoxSize => 150;
        public static int ImageButtonSize => 40;
    }
}
