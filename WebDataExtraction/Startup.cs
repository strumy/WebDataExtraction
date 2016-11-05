using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebDataExtraction.Startup))]
namespace WebDataExtraction
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
