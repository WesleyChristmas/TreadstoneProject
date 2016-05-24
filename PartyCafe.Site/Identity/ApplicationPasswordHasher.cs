using System;
using Microsoft.AspNet.Identity;
using System.Security.Cryptography;
using System.Text;

namespace PartyCafe.Site.Identity
{
    public class ApplicationPasswordHasher : PasswordHasher
    {
        public override string HashPassword(string password)
        {
            var hasher = MD5.Create();
            var data = hasher.ComputeHash(Encoding.UTF8.GetBytes(password ?? String.Empty));

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            var hash = HashPassword(providedPassword);

            if (hash == hashedPassword)
            {
                return PasswordVerificationResult.Success;
            }
            else
            {
                return PasswordVerificationResult.Failed;
            }
        }
    }
}