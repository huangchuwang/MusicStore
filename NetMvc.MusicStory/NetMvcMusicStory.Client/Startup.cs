using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NetMvcMusicStory.Client.Startup))]
namespace NetMvcMusicStory.Client
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
