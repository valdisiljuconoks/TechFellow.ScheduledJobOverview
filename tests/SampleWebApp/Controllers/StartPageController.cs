using System.Web.Mvc;
using EPiServer.DataAbstraction;
using EPiServer.ServiceLocation;
using SampleWebApp.Models.Pages;
using SampleWebApp.Models.ViewModels;
using EPiServer.Web;
using EPiServer.Web.Mvc;

namespace SampleWebApp.Controllers
{
    public class StartPageController : PageControllerBase<StartPage>
    {

        internal static Injected<ServiceAccessor<ScheduledJobRepository>> Repository { get; set; }

        public ActionResult Index(StartPage currentPage)
        {

            var repo = Repository.Service();

            var model = PageViewModel.Create(currentPage);

            if (SiteDefinition.Current.StartPage.CompareToIgnoreWorkID(currentPage.ContentLink)) // Check if it is the StartPage or just a page of the StartPage type.
            {
                //Connect the view models logotype property to the start page's to make it editable
                var editHints = ViewData.GetEditHints<PageViewModel<StartPage>, StartPage>();
                editHints.AddConnection(m => m.Layout.Logotype, p => p.SiteLogotype);
                editHints.AddConnection(m => m.Layout.ProductPages, p => p.ProductPageLinks);
                editHints.AddConnection(m => m.Layout.CompanyInformationPages, p => p.CompanyInformationPageLinks);
                editHints.AddConnection(m => m.Layout.NewsPages, p => p.NewsPageLinks);
                editHints.AddConnection(m => m.Layout.CustomerZonePages, p => p.CustomerZonePageLinks);
            }

            return View(model);
        }

    }
}
