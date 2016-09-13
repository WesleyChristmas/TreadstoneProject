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
    public class MvcApplication : System.Web.HttpApplication
    {
        private Timer tmr;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            tmr = new Timer(5 * 60 * 1000);
            tmr.AutoReset = true;
            tmr.Elapsed += Tmr_Elapsed;
            tmr.Start();
        }

        private void Tmr_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                tmr.Stop();
                var reader = new System.Configuration.AppSettingsReader();
                var username = reader.GetValue("EmailOrdersFromUser", typeof(string)).ToString();
                var password = reader.GetValue("EmailOrdersFromPassword", typeof(string)).ToString();
                var host = reader.GetValue("EmailOrdersFromHost", typeof(string)).ToString();
                var port = reader.GetValue("EmailOrdersFromPort", typeof(string)).ToString();

                Mailer mailer = new Mailer(username, password, host, port);

                var emails = EmailUtils.GetUnsendedMails();
                foreach (var mail in emails)
                {
                    if (!mailer.SendMail(mail)) continue;
                    EmailUtils.SetMailSended(mail.RecordId);
                }
            }
            finally
            {
                tmr.Start();
            }
        }
    }
}
