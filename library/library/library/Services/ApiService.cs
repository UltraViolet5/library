using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using library.Model;
using Newtonsoft.Json;

namespace library.Services
{
    public class ApiService
    {
        private readonly HttpClient _client;

        public ApiService()
        {
#if DEBUG
            HttpClientHandler insecureHandler = GetInsecureHandler();
            _client = new HttpClient(insecureHandler);
#else
            _client = new HttpClient();
#endif
        }

        public async Task<List<Book>> GetBooks()
        {
            List<Book> result = new List<Book>();
            try
            {
                using (_client)
                {
                    HttpResponseMessage response = await _client.GetAsync(Constants.BooksUrl);
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

        public async Task<Book> GetBook(int id)
        {
            Book result = new Book();
            try
            {
                using (_client)
                {
                    HttpResponseMessage response = await _client.GetAsync(Constants.BooksUrl + id);
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
                return null;
            }

            return result;
        }

        public async Task<List<Borrowing>> GetBorrowings()
        {
            List<Borrowing> result = new List<Borrowing>();
            try
            {
                using(_client)
                {
                    HttpResponseMessage response = await _client.GetAsync(Constants.BorrowingsUrl);
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

        public async Task<IEnumerable<Borrowing>> GetBorrowings(string userId)
        {
            List<Borrowing> result = new List<Borrowing>();
            try
            {
                using (_client)
                {
                    HttpResponseMessage response = await _client.GetAsync(Constants.BorrowingsUrl);
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

            return result
                .Where(b => b.Borrower.Id == userId
                            || b.Client.Id == userId);
        }

        public async Task<User> GetUser(string id)
        {
            User result = new User();
            try
            {
                using (_client)
                {
                    HttpResponseMessage response = await _client.GetAsync(Constants.UsersUrl + id);
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
                using (_client)
                {
                    HttpResponseMessage response = await _client.GetAsync(Constants.UsersUrl);
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

        public async Task AddBook(Book book)
        {
            var json = JsonConvert.SerializeObject(book);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (_client)
            {
                var response = await _client.PostAsync(Constants.BooksUrl, data);
                string result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(result);
            }
        }

        public async Task AddBooks(IEnumerable<Book> books)
        {
            var json = JsonConvert.SerializeObject(books);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (_client)
            {
                var response = await _client.PostAsync(Constants.BooksUrl, data);
                string result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(result);
            }
        }

        public async Task AddUser(User user)
        {
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (_client)
            {
                var response = await _client.PostAsync(Constants.UsersUrl, data);
                string result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(result);
            }
        }

        public async Task<List<User>> GetUserFriends(string userId)
        {
            List<User> users = new List<User>();
            List<Friend> friends = new List<Friend>();
            try
            {
                using (_client)
                {
                    HttpResponseMessage response = await _client.GetAsync(Constants.FriendsUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        friends = JsonConvert.DeserializeObject<List<Friend>>(content);
                    }

                    foreach (Friend friend in friends)
                    {
                        if (friend.UserId == userId)
                        {
                            response = await _client.GetAsync(Constants.UsersUrl + friend.FriendId);
                            if (response.IsSuccessStatusCode)
                            {
                                string content = await response.Content.ReadAsStringAsync();
                                var u = JsonConvert.DeserializeObject<User>(content);
                                users.Add(u);
                            }
                        }
                        else if (friend.FriendId == userId)
                        {
                            response = await _client.GetAsync(Constants.UsersUrl + friend.UserId);
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

        public async Task<List<Friend>> GetFriends()
        {
            List<Friend> result = new List<Friend>();
            try
            {
                using (_client)
                {
                    HttpResponseMessage response = await _client.GetAsync(Constants.FriendsUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<List<Friend>>(content);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tERROR {ex.Message}");
            }

            return result;
        }
    }
}