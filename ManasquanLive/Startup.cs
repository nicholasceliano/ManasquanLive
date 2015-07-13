using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ManasquanLive.Startup))]
namespace ManasquanLive
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
