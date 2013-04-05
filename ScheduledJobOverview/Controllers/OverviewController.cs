using System.Web.Mvc;

namespace TechFellow.ScheduledJobOverview.Controllers
{
    public class OverviewController : Controller
    {
        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetList()
        {
            var repository = new JobRepository();
            return Json(repository.GetList(), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}
