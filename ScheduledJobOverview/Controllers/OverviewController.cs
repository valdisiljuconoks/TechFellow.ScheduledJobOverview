using System;
using System.Linq;
using System.Web.Mvc;
using EPiServer.DataAbstraction;

namespace TechFellow.ScheduledJobOverview.Controllers
{
    [Authorize]
    public class OverviewController : Controller
    {
        //public ActionResult ExportToCSV()
        //{
        //    Response.Clear();
        //    Response.AddHeader("content-disposition", "attachment;filename=prenumeranter.xls");
        //    Response.Charset = "UTF8";
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    Response.ContentType = "application/vnd.xls";
        //    StringWriter stringWrite = new StringWriter();
        //    HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        //    //grdSubscribers.RenderControl(htmlWrite);
        //    Response.Write(stringWrite.ToString());
        //    Response.End();
        //    return null;
        //}

        public RedirectResult Execute(string jobId)
        {
            if (string.IsNullOrEmpty(jobId))
            {
                throw new ArgumentNullException("jobId");
            }

            var repository = new JobRepository();
            var job = repository.GetList().FirstOrDefault(j => j.Id == int.Parse(jobId));

            if (job != null)
            {
                try
                {
                    ScheduledJob jobInstance;
                    if (job.InstanceId != Guid.Empty)
                    {
                        jobInstance = ScheduledJob.Load(job.InstanceId);
                    }
                    else
                    {
                        jobInstance = new ScheduledJob
                                          {
                                                  IntervalType = ScheduledIntervalType.Days,
                                                  IsEnabled = false,
                                                  Name = job.Name,
                                                  MethodName = "Execute",
                                                  TypeName = job.TypeName,
                                                  AssemblyName = job.AssemblyName,
                                                  IsStaticMethod = true
                                          };

                        if (jobInstance.NextExecution == DateTime.MinValue)
                        {
                            jobInstance.NextExecution = DateTime.Today;
                        }

                        jobInstance.Save();
                    }

                    if (jobInstance != null)
                    {
                        jobInstance.ExecuteManually();
                    }
                }
                catch
                {
                }
            }

            return Redirect("~/modules/" + Const.ModuleName + "/Overview/Index");
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
            return RuntimeInfo.IsModule() ? View() : View("~/modules/" + Const.ModuleName + "/Views/Overview/Index.aspx");
        }
    }
}
