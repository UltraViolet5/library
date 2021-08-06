using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using library.Model;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace library.Services
{
    public class ApiService
    {
        public async Task<List<Book>> GetBooks()
        {
            List<Book> result = new List<Book>();
            Uri uri = new Uri(String.Format(Constants.BooksUrl, String.Empty));
            try
            {
                using (var client = GetHttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(Constants.BooksUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<List<Book>>(content);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tERROR {ex.Message}");
            }

            return result;
        }

        public async Task<User> GetUser(string id)
        {
            User result = new User();
            try
            {
                using (HttpClient client = GetHttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(Constants.UsersUrl + id);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<User>(content);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tERROR {ex.Message}");
            }

            return result;
        }

        public async Task<List<User>> GetUsers()
        {
            List<User> result = new List<User>();
            try
            {
                using (HttpClient client = GetHttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(Constants.UsersUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<List<User>>(content);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tERROR {ex.Message}");
            }

            return result;
        }

        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }

        public async Task<List<User>> GetUserFriends(string userId)
        {
            List<User> users = new List<User>();
            List<Friends> friends = new List<Friends>();
            try
            {
                using (var client = GetHttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(Constants.FriendsUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        friends = JsonConvert.DeserializeObject<List<Friends>>(content);
                    }

                    foreach (Friends friend in friends)
                    {
                        if (friend.UserId == userId)
                        {
                            response = await client.GetAsync(Constants.UsersUrl + friend.FriendId);
                            if (response.IsSuccessStatusCode)
                            {
                                string content = await response.Content.ReadAsStringAsync();
                                var u = JsonConvert.DeserializeObject<User>(content);
                                users.Add(u);
                            }
                        }
                        else if (friend.FriendId == userId)
                        {
                            response = await client.GetAsync(Constants.UsersUrl + friend.UserId);
                            if (response.IsSuccessStatusCode)
                            {
                                string content = await response.Content.ReadAsStringAsync();
                                var u = JsonConvert.DeserializeObject<User>(content);
                                users.Add(u);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tERROR {ex.Message}");
            }

            return users;
        }

        private HttpClient GetHttpClient()
        {
            HttpClient client;

#if DEBUG
            HttpClientHandler insecureHandler = GetInsecureHandler();
            client = new HttpClient(insecureHandler);
#else
            client = new HttpClient();
#endif
            return client;
        }
    }
}