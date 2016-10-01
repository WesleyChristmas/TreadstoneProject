using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using PartyCafe.Site.Areas.Admin;
using PartyCafe.Site.DBUtils;
using PartyCafe.Site.Models.Utils;

namespace PartyCafe.Site
{
    public class MvcApplication : HttpApplication
    {
        private Timer _tmr;
        private int _intervalMinutes = 5;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            _tmr = new Timer
            {
                Interval = 60*1000, // Первый запуск
                AutoReset = true
            };
            _tmr.Elapsed += Tmr_Elapsed;
            _tmr.Start();
        }

        private void Tmr_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                _tmr.Stop();
                var reader = new System.Configuration.AppSettingsReader();
                var username = reader.GetValue("EmailOrdersFromUser", typeof(string)).ToString();
                var password = reader.GetValue("EmailOrdersFromPassword", typeof(string)).ToString();
                var host = reader.GetValue("EmailOrdersFromHost", typeof(string)).ToString();
                var port = reader.GetValue("EmailOrdersFromPort", typeof(string)).ToString();
                
                var mailer = new Mailer(username, password, host, port);
                var emails = EmailUtils.GetUnsendedMails();
                foreach (var mail in emails)
                {
                    if (!mailer.SendMail(mail)) continue;
                    EmailUtils.SetMailSended(mail.RecordId);
                }

                _intervalMinutes = (int)reader.GetValue("CheckPeriodMinutes", typeof(int));
                _tmr.Interval = _intervalMinutes * 60 * 1000;
            }
            finally
            {
                _tmr.Start();
            }
        }
    }
}
