using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(partycafev2.Startup))]
namespace partycafev2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
