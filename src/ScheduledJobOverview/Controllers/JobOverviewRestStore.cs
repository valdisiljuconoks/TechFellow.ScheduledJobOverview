using System;
using System.Threading;
using System.Web.Mvc;
using EPiServer.Scheduler;
using EPiServer.Shell.Services.Rest;

namespace TechFellow.ScheduledJobOverview.Controllers
{
    [RestStore("joboverview")]
    public class JobOverviewRestStore : RestControllerBase
    {
        private readonly JobRepository _repository;
        private readonly IScheduledJobExecutor _executor;

        public JobOverviewRestStore(JobRepository repository, IScheduledJobExecutor executor)
        {
            _repository = repository;
            _executor = executor;
        }

        [HttpGet]
        public RestResult Get()
        {
            return Rest(_repository.GetList());
        }

        public RestResult Post(string id)
        {
            if(string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            var job = _repository.GetById(int.Parse(id));

            if(job == null)
                return Rest(null);

            var jobInstance = job.InstanceId != Guid.Empty ? _repository.Get(job.InstanceId) : _repository.Create(job);
            _executor.StartAsync(jobInstance, new JobExecutionOptions { Trigger = ScheduledJobTrigger.User });

            return Rest(null);
        }

        public RestResult Put(string id)
        {
            if(string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            var instance = _repository.GetById(int.Parse(id));
            if(instance != null)
                _executor.Cancel(instance.InstanceId);

            return Rest(null);
        }

        public RestResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            _repository.Delete(Guid.Parse(id));
            return Rest(null);
        }
    }
}
