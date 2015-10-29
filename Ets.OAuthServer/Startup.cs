using Owin;

namespace Ets.OAuthServer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            EngineInit();
            ConfigureAuth(app);
        }
    }
}
