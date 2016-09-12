using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestASP.Startup))]
namespace TestASP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
