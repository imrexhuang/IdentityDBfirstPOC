using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IdentityDBfirstPOC.Startup))]
namespace IdentityDBfirstPOC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
