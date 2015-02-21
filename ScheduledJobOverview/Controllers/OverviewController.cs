using System;
using System.Web.Mvc;

namespace TechFellow.ScheduledJobOverview.Controllers
{
    [Authorize]
    public class OverviewController : Controller
    {
        private readonly JobRepository _repository;

        public OverviewController()
        {
            _repository = new JobRepository();
        }

        [HandleError]
        public RedirectResult Execute(string jobId)
        {
            if (string.IsNullOrEmpty(jobId))
            {
                throw new ArgumentNullException("jobId");
            }

            var job = _repository.GetById(int.Parse(jobId));

            if (job != null)
            {
                var jobInstance = job.InstanceId != Guid.Empty ? _repository.Get(job.InstanceId) : _repository.Create(job);

                if (jobInstance != null)
                {
                    jobInstance.ExecuteManually();
                }
            }

            return Redirect("~/" + Const.ModuleName + "/Overview/Index");
        }

        [HandleError]
        public RedirectResult Stop(string jobId)
        {
            if (string.IsNullOrEmpty(jobId))
            {
                throw new ArgumentNullException("jobId");
            }

            var instance = _repository.GetById(int.Parse(jobId));
            if (instance != null)
            {
                _repository.Get(instance.InstanceId).Stop();
            }

            return Redirect("~/" + Const.ModuleName + "/Overview/Index");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetList()
        {
            var repository = new JobRepository();
            return Json(repository.GetList(), JsonRequestBehavior.AllowGet);
        }

        public ViewResult Index()
        {
            // we need to specify absolute path for the view - may return different view in larger scale applications
            return RuntimeInfo.IsModule() ? View() : View("~/" + Const.ModuleName + "/Views/Overview/Index.aspx");
        }
    }
}
