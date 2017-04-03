using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer.Scheduler;
using EPiServer.ServiceLocation;
using EPiServer.Shell.WebForms;

namespace TechFellow.ScheduledJobOverview.modules._protected.TechFellow.ScheduledJobOverview
{
    public partial class Chart : WebFormsBase
    {
        private readonly IScheduledJobLogRepository _logRepo;
        private readonly JobRepository _repo;
        protected IEnumerable<TimeSpan> durations;
        protected string jobName;
        protected IEnumerable<string> times;
        protected int jobId;

        public Chart()
        {
            PreRender += OnPreRender;
            _repo = ServiceLocator.Current.GetInstance<JobRepository>();
            _logRepo = ServiceLocator.Current.GetInstance<IScheduledJobLogRepository>();
        }

        private void OnPreRender(object sender, EventArgs eventArgs)
        {
            var id = Convert.ToInt32(Request["pluginId"]);
            var job = _repo.GetById(id);

            jobName = job.Name;
            jobId = job.Id;

            var logs = _logRepo.GetAsync(job.InstanceId, 0, 10)
                               .GetAwaiter()
                               .GetResult()
                               .PagedResult
                               .ToList();

            times = logs.Select(l => $"\"{l.CompletedUtc:yyyy-MM-dd HH:mm:ss}\"");
            durations = logs.Select(l => l.Duration ?? TimeSpan.Zero);

            DataBind();
        }
    }
}
