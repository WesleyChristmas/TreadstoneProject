using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PartyCafe.Site.Startup))]
namespace PartyCafe.Site
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
