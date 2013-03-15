using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EPiServer.Security;

namespace TechFellow.ScheduledJobOverview
{
    public class ModuleRoute : Route
    {
        public ModuleRoute(string url) : base(url, new MvcRouteHandler())
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
