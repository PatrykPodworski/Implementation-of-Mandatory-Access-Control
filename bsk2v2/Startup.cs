using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(bsk2v2.Startup))]
namespace bsk2v2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
