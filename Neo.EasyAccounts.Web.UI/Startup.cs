using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Neo.EasyAccounts.Web.UI.Startup))]
namespace Neo.EasyAccounts.Web.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
