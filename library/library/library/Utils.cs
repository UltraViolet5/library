using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using library.Model;
using Xamarin.Forms;

namespace library
{
    public static class Utils
    {
        public static void SaveUserInSession(User user)
        {
            Application.Current.Properties["IsLoggedIn"] = true;
            Application.Current.Properties["UserId"] = user.Id;
            Application.Current.Properties["UserEmail"] = user.Email;
            App.CurrentUser = user;
        }

        public static void RemoveUserFromSession()
        {
            Application.Current.Properties["IsLoggedIn"] = false;
            Application.Current.Properties["UserId"] = "";
            Application.Current.Properties["UserEmail"] = "";
            App.CurrentUser = null;
        }

        public static User GetCurrentUser()
        {
            var userEmail = (string) App.Current.Properties["UserEmail"];
            return App.DbService.GetUser(userEmail);
        }
    }
}
