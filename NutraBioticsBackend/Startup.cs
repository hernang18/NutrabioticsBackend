using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NutraBioticsBackend.Startup))]
namespace NutraBioticsBackend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
