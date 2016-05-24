using Microsoft.AspNet.Identity;
using System;

namespace PartyCafe.Site.Identity
{
    public class PartyCafeUser : IUser
    {
        Guid _id;
        string _name;
        string _password;

        public PartyCafeUser(string name)
        {
            _id = Guid.NewGuid();
            _name = name;
            _password = String.Empty;
        }

        public PartyCafeUser(Guid id, string name, string password)
        {
            _id = id;
            _name = name;
            _password = password;
        }

        public string Id
        {
            get
            {
                return _id.ToString();
            }
        }

        public string UserName
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
        }
    }
}