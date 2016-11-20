using System;
using System.Web.Mvc;
using EPiServer.Shell.Services.Rest;

namespace TechFellow.ScheduledJobOverview.Controllers
{
    [RestStore("joboverview")]
    public class JobOverviewRestStore : RestControllerBase
    {
        private readonly JobRepository _repository;

        public JobOverviewRestStore()
        {
            _repository = new JobRepository();
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
            jobInstance?.ExecuteManually();

            return Rest(null);
        }

        public RestResult Put(string id)
        {
            if(string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            var instance = _repository.GetById(int.Parse(id));
            if(instance != null)
                _repository.Get(instance.InstanceId).Stop();

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
