using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using PartyCafe.Site.DBUtils;

namespace PartyCafe.Site.Models.Utils
{


    public class Mailer
    {
        private readonly string _username;
        private readonly string _password;
        private readonly string _hostname;
        private readonly string _port;
        private readonly SmtpClient _client;

        public Mailer(string username, string password, string hostname, string port)
        {
            _username = username;
            _password = password;
            _hostname = hostname;
            _port = port;

            _client = GetSmtpClient();
        }

        public bool SendMail(MailData data)
        {
            try
            {
                var mail = new MailMessage(_username, data.ToEmail)
                {
                    Subject = data.Subject,
                    Body = data.Message
                };
                _client.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private SmtpClient GetSmtpClient()
        {
            var sc = new SmtpClient(_hostname, Convert.ToInt32(_port))
            {
                Credentials = new System.Net.NetworkCredential()
                {
                    UserName = _username,
                    Password = _password
                },
                EnableSsl = true
            };
            return sc;
        }
    }
}