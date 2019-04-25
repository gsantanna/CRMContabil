using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(bie.focuscrm.ui.mvc.Startup))]
namespace bie.focuscrm.ui.mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
