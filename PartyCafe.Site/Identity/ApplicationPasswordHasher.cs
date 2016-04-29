using System;
using Microsoft.AspNet.Identity;

namespace PartyCafe.Site.Identity
{
    public class ApplicationPasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            throw new NotImplementedException();
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            throw new NotImplementedException();
        }
    }
}