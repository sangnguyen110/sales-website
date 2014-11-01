using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HoangViet.Startup))]
namespace HoangViet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
