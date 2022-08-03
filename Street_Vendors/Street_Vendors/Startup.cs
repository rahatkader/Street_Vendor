using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Street_Vendors.Startup))]
namespace Street_Vendors
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
