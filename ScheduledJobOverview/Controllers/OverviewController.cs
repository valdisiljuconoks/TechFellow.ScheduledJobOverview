using System.Web.Mvc;

namespace TechFellow.ScheduledJobOverview.Controllers
{
    public class OverviewController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            var repository = new JobRepository();
            return View(repository.GetList());
        }
    }
}
