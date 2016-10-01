using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyCafe.Site.DBUtils
{
    public class MailData
    {
        public int RecordId;
        public string ToEmail;
        public string Message;
        public string Subject;
    }

    public static class EmailUtils
    {
        public static void AddEmail(string subject, string message, string to)
        {
            try
            {
                var mail = new Email()
                {
                    To = to,
                    Subject = subject,
                    Message = message,
                    DateCreate = DateTime.Now
                };

                var db = MainUtils.GetDBContext();
                db.Emails.InsertOnSubmit(mail);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
            }
        }

        public static void SetMailSended(int id)
        {
            try
            {
                var db = MainUtils.GetDBContext();
                var email = db.Emails.SingleOrDefault(x => x.IdRecord == id);
                if (email == null) return;

                email.DateSend = DateTime.Now;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
            }

        }

        public static List<MailData> GetUnsendedMails()
        {
            try
            {
                var db = MainUtils.GetDBContext();
                return db.Emails.Where(x => !x.DateSend.HasValue).Select(x => new MailData()
                {
                    RecordId = x.IdRecord,
                    Subject = x.Subject,
                    Message = x.Message,
                    ToEmail = x.To
                }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
    }
}