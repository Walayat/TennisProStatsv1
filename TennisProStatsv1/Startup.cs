using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TennisProStatsv1.Startup))]
namespace TennisProStatsv1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
