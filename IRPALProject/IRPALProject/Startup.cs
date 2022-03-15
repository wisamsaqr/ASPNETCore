using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IRPALProject.Startup))]
namespace IRPALProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
