using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WorkFlowSpy.Startup))]
namespace WorkFlowSpy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
