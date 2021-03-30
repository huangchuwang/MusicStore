using System.Web;
using System.Web.Mvc;

namespace NetMvcMusicStory.Client
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new MyFilter1Attribute());
        }
    }
}
