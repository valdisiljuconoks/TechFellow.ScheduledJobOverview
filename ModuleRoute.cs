using System.Web;
using System.Web.Routing;
using EPiServer.Security;

namespace TechFellow.ScheduledJobOverview
{
    public class ModuleRoute : Route
    {
        public ModuleRoute(string url, string physicalFile) : base(url, new PageRouteHandler(physicalFile, false))
        {
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if (!httpContext.Request.RawUrl.Contains(Const.ModuleName) || !PrincipalInfo.HasAdminAccess)
            {
                return null;
            }

            var routeData = new RouteData(this, RouteHandler);
            return routeData;
        }
    }
}
