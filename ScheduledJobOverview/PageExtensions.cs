using System.Web.UI;
using EPiServer.Shell;

namespace TechFellow.ScheduledJobOverview
{
    public static class PageExtensions
    {
        public static string GetResourceUrl(this Page page, string url)
        {
            return RuntimeInfo.IsModule()
                           ? Paths.ToClientResource(Const.ModuleName, url + "/")
                           : string.Format("/modules/{0}/{1}/", Const.ModuleName, url);
        }
    }
}
