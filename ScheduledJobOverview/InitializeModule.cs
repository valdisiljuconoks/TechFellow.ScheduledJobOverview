using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Web.Hosting;
using InitializationModule = EPiServer.Web.InitializationModule;

namespace TechFellow.ScheduledJobOverview
{
    [ModuleDependency(typeof(InitializationModule))]
    [InitializableModule]
    public class InitializeModule : IInitializableModule
    {
        public void Preload(string[] parameters)
        {
        }

        public void Initialize(InitializationEngine context)
        {
#if !ADDON
            GenericHostingEnvironment.Instance.RegisterVirtualPathProvider(new ResourceProvider());
            RouteTable.Routes.MapRoute("ScheduledJobOverviewPlugin",
                                       "modules/" + Const.ModuleName + "/{controller}/{action}",
                                       new { controller = "Overview", action = "Index" });

            ViewEngines.Engines.Add(new CustomViewEngine());
#endif
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}
