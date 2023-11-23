using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InvoiceProject2.Startup))]
namespace InvoiceProject2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
