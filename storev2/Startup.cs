using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(storev2.Startup))]
namespace storev2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
