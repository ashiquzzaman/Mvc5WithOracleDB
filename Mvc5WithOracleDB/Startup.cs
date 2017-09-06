using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mvc5WithOracleDB.Startup))]
namespace Mvc5WithOracleDB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
