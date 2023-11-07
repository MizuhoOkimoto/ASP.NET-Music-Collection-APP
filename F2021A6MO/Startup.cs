using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(F2021A6MO.Startup))]

namespace F2021A6MO
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
