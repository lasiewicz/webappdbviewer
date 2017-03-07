using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DBDeploymentsProgress.Startup))]
namespace DBDeploymentsProgress
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
