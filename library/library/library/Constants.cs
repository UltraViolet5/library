using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace library
{
    public static class Constants
    {
        public static string BaseAddress = 
            DeviceInfo.Platform == DevicePlatform.Android 
            ? "http://10.0.2.2:8080"
            : "http://localhost:8080";

        public static string BooksUrl = $"{BaseAddress}/api/Books/";
        public static string UsersUrl = $"{BaseAddress}/api/Users/";
        public static string FriendsUrl = $"{BaseAddress}/api/Friends/";
    }
}
