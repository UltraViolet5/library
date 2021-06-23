using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using library.Model;

namespace library.Infrastructure.Daos.Implementations
{
    class UserDaoMemory : IUserDao
    {
        private List<User> _data = new List<User>();

        private static UserDaoMemory _s;

        public static UserDaoMemory S
        {
            get
            {
                if (_s == null)
                    _s = new UserDaoMemory();

                return _s;
            }

            private set => _s = value;
        }

        private UserDaoMemory()
        {
        }

        public void Add(User item)
        {
            _data.Add(item);
        }

        public void Remove(int id)
        {
            throw new InvalidOperationException("User have string id. Use proper method for it.");
        }

        public User Get(int id)
        {
            throw new InvalidOperationException("User have string id. Use proper method for it.");
        }

        public void Remove(string id)
        {
            var toRemove = _data.First(x => x.Id == id);
            if (toRemove == null)
                throw new ArgumentException($"No found Category with id {id}");

            _data.Remove(toRemove);
        }

        public User Get(string id)
        {
            var result = _data.First(x => x.Id == id);
            if (result == null)
                throw new ArgumentException($"No found Category with id {id}");

            return result;
        }

        public IEnumerable<User> GetAll()
        {
            return _data;
        }
    }
}
