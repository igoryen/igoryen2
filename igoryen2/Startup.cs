using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(igoryen2.Startup))]
namespace igoryen2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
