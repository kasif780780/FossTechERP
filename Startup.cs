using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FossERp.Startup))]
namespace FossERp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
