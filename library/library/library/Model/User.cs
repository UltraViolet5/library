using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace library.Model
{
    public class User : IdentityUser
    {
        public DateTime BirthDate { get; set; }
        public string Localization { get; set; }
        public bool TermsAccepted { get; private set; }
        public List<User> Friends { get; set; }
        public byte[] Photo { get; set; }

        public User()
        {
            Id = Guid.NewGuid().ToString();
            TermsAccepted = true;

            Friends = new List<User>();
        }
    }
}
