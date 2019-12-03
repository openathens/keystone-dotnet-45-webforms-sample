using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(KeystoneDotNet45WebFormsSample.Startup))]
namespace KeystoneDotNet45WebFormsSample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
