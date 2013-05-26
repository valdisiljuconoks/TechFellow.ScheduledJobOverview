using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Web.Hosting;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using InitializationModule = EPiServer.Web.InitializationModule;

namespace TechFellow.ScheduledJobOverview
{
    /// <summary>
    ///     This class is created just for workaround as EPiServer does not expose events around default route registration process.
    /// </summary>
    public class WorkaroundRouteRegistrationHttpModule : IHttpModule
    {
        private static bool isInitialized;

        public void Init(HttpApplication context)
        {
            if (isInitialized)
            {
                return;
            }

            RouteTable.Routes.MapRoute("ScheduledJobOverviewPlugin",
                                       "modules/" + Const.ModuleName + "/{controller}/{action}/{id}",
                                       new { controller = "Overview", action = "Index", id = UrlParameter.Optional },
                                       new[] { "TechFellow.ScheduledJobOverview.Controllers" })
                      .DataTokens["UseNamespaceFallback"] = false;

            lock (context)
            {
                isInitialized = true;
            }
        }

        public void Dispose()
        {
        }
    }

    [ModuleDependency(typeof(InitializationModule))]
    [InitializableModule]
    public class InitializeModule : IInitializableModule
    {
        public void Preload(string[] parameters)
        {
        }

        public void Initialize(InitializationEngine context)
        {
            if (RuntimeInfo.IsModule())
            {
                return;
            }

#if !ADDON && !CMS6
            DynamicModuleUtility.RegisterModule(typeof(WorkaroundRouteRegistrationHttpModule));
#endif
            GenericHostingEnvironment.Instance.RegisterVirtualPathProvider(new ResourceProvider());
            ViewEngines.Engines.Add(new CustomViewEngine());
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}

namespace Microsoft.Web.Infrastructure.DynamicModuleHelper
{
}
