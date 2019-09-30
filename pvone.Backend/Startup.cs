using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(pvone.Backend.Startup))]
namespace pvone.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
