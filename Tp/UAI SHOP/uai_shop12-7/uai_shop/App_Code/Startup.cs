using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(uai_shop.Startup))]
namespace uai_shop
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
