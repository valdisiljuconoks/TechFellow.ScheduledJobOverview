using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Web.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
            RouteTable.Routes.MapRoute("ScheduledJobPlugin",
                                       "modules/" + Const.ModuleName + "/{controller}/{action}",
                                       new { controller = "Overview", action = "Index" });

            RouteTable.Routes.MapHttpRoute("ScheduledJobDefaultApi",
                                           "modules/" + Const.ModuleName + "/api/{controller}/{id}",
                                           new { id = RouteParameter.Optional });

            ViewEngines.Engines.Add(new CustomViewEngine());
#endif

            var formatters = GlobalConfiguration.Configuration.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}
