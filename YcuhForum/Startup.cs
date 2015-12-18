using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YcuhForum.Startup))]
namespace YcuhForum
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
