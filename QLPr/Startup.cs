using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QLPr.Startup))]
namespace QLPr
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
