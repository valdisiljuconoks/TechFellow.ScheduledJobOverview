using System.Web.Mvc;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using SampleWebApp.Business.Rendering;
using EPiServer.Web.Mvc;
using EPiServer.Web.Mvc.Html;

namespace SampleWebApp.Business.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class DependencyResolverInitialization : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<IContentRenderer, ErrorHandlingContentRenderer>();
            context.Services.AddSingleton<ContentAreaRenderer, AlloyContentAreaRenderer>();

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(context.StructureMap()));
        }

        public void Initialize(InitializationEngine context)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}
