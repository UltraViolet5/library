using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using library.Model;
using Newtonsoft.Json;
using Xamarin.Forms.Internals;

namespace library.Services
{
    public class ApiService
    {
        public async Task<Book> GetBook(int bookId)
        {
            Book result = new Book();
            Uri uri = new Uri(string.Format(Constants.BooksUrl + $"/{bookId}", String.Empty));
            try
            {
                using (var client = GetHttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<Book>(content);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tERROR {ex.Message}");
            }

            return result;
        }

        public async Task<List<Book>> GetBooks(string userId)
        {
            List<Book> result = new List<Book>();
            Uri uri = new Uri(string.Format(Constants.BooksUrl + $"/ByUserId/{userId}", String.Empty));
            try
            {
                using (var client = GetHttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(uri);
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
        
        public async Task<List<Book>> GetBooks(string userId, int amount)
        {
            List<Book> result = new List<Book>();
            Uri uri = new Uri(string.Format(Constants.BooksUrl + $"/ByUserId/{userId}/{amount}", String.Empty));
            try
            {
                using (var client = GetHttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(uri);
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

        public async Task<int> GetBooksCount(string userId)
        {
            List<Book> result = new List<Book>();
            Uri uri = new Uri(string.Format(Constants.BooksUrl +
                                            $"/ByUserId/{userId}/Count",
                                            String.Empty));
            try
            {
                using (var client = GetHttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(uri);
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

            return result.Count;
        }

        public async Task<User> GetUser(string id)
        {
            User result = new User();
            try
            {
                Uri uri = new Uri(String.Format(Constants.UsersUrl + "/" + id, String.Empty));
                using (HttpClient client = GetHttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<User>(content);
                    }
                }

                if (result != null)
                    result.Friends = await GetUserFriends(result.Id);
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
                            response = await client.GetAsync(Constants.UsersUrl + "/" + friend.FriendId);
                            if (response.IsSuccessStatusCode)
                            {
                                string content = await response.Content.ReadAsStringAsync();
                                var u = JsonConvert.DeserializeObject<User>(content);
                                users.Add(u);
                            }
                        }
                        else if (friend.FriendId == userId)
                        {
                            response = await client.GetAsync(Constants.UsersUrl + "/" + friend.UserId);
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

        public async Task<IEnumerable<Borrowing>> GetBorrowings(string ownerId, int amount)
        {
            List<Borrowing> result = new List<Borrowing>();
            try
            {
                using (var client = GetHttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(Constants.BorrowingsUrl + $"/ByOwnerId/{ownerId}/{amount}");
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<List<Borrowing>>(content);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tERROR {ex.Message}");
            }

            return result;
        }

        public async Task<IEnumerable<Borrowing>> GetBorrowings(string ownerId)
        {
            List<Borrowing> result = new List<Borrowing>();
            try
            {
                using (var client = GetHttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(Constants.BorrowingsUrl + $"/ByOwnerId/{ownerId}");
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<List<Borrowing>>(content);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tERROR {ex.Message}");
            }

            return result;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            User result = new User();
            try
            {
                Uri uri = new Uri(String.Format(Constants.UsersUrl + $"/byemail/{email}", String.Empty));
                using (HttpClient client = GetHttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<User>(content);
                    }

                    if (result != null)
                        result.Friends = await GetUserFriends(result.Id);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tERROR {ex.Message}");
            }

            return result;
        }

        public async void AddBook(Book book)
        {
            var json = JsonConvert.SerializeObject(book, Formatting.Indented);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = GetHttpClient())
            {
                var response = await client.PostAsync(Constants.BooksUrl, data);
                string result = await response.Content.ReadAsStringAsync();
            }
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
            client.DefaultRequestHeaders.Add("User-Agent", "request");
            return client;
        }

        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public async void RemoveBook(int id)
        {
            using (var client = GetHttpClient())
            {
                var response = await client.DeleteAsync(Constants.BooksUrl + $"/{id}");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Success. Deleted book with id {id}!");
                }
            }
        }
    }
}