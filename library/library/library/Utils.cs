using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading.Tasks;
using library.Model;
using Plugin.Media;
using Plugin.Media.Abstractions;
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

        public static string GetCurrentUserEmail()
        {
            return (string) App.Current.Properties["UserEmail"];
        }

        public static ImageSource BytesToImageSource(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                return null;

            return ImageSource.FromStream(() => new MemoryStream(bytes));
        }

        public static async Task<byte[]> TakePhoto()
        {
            var photo = await CrossMedia.Current.TakePhotoAsync(
                new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                {
                    CompressionQuality = 80,
                    PhotoSize = PhotoSize.Medium
                });

            // ImageSource from stream: ImageSource.FromStream((() => photo.GetStream()));

            if (photo == null)
            {
                return null;
            }

            return File.ReadAllBytes(photo.Path);
        }


        public static byte[] ImageDataFromResource(string r, Assembly assembly)
        {
            // Ensure "this" is an object that is part of your implementation within your Xamarin forms project
            // get assembly by: var assembly = this.GetType().GetTypeInfo().Assembly;
            byte[] buffer = null;

            using (Stream s = assembly.GetManifestResourceStream(r))
            {
                if (s != null)
                {
                    long length = s.Length;
                    buffer = new byte[length];
                    s.Read(buffer, 0, (int)length);
                }
            }

            return buffer;
        }
    }
}