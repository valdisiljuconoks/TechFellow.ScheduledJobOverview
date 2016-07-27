using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer.DataAbstraction;
using EPiServer.PlugIn;
using TechFellow.ScheduledJobOverview.Models;

namespace TechFellow.ScheduledJobOverview
{
    public class JobRepository
    {
        private readonly ScheduledJobRepository _actualRepository;

        public JobRepository()
        {
            _actualRepository = new ScheduledJobRepository();
        }

        public List<JobDescriptionViewModel> GetList()
        {
            var plugIns = PlugInLocator.Search(new ScheduledPlugInAttribute()).ToList();

            return (from plugin in plugIns
                    let job = _actualRepository.List().FirstOrDefault(j => j.TypeName == plugin.TypeName && j.AssemblyName == plugin.AssemblyName)
                    let attr = plugin.GetAttribute<ScheduledPlugInAttribute>()
                    select new JobDescriptionViewModel
                    {
                        Id = plugin.ID,
                        InstanceId = job != null ? job.ID : Guid.Empty,
                        Name = attr.DisplayName,
                        Description = attr.Description,
                        IsEnabled = (job != null && job.IsEnabled),
                        Interval = job != null ? string.Format("{0} ({1})", job.IntervalLength, job.IntervalType) : "",
                        IsLastExecuteSuccessful = (job != null && !job.HasLastExecutionFailed ? true : (bool?)null),
                        LastExecute = job != null ? job.LastExecution : (DateTime?)null,
                        AssemblyName = plugin.AssemblyName,
                        TypeName = plugin.TypeName,
                        IsRunning = job != null && ScheduledJob.IsJobRunning(job.ID),
                    }).OrderBy(j => j.Name).ToList();
        }

        public ScheduledJob Get(Guid instanceId)
        {
            return _actualRepository.Get(instanceId);
        }

        public ScheduledJob Create(JobDescriptionViewModel job)
        {
            var result = new ScheduledJob
            {
                IntervalType = ScheduledIntervalType.Days,
                IsEnabled = false,
                Name = job.Name,
                MethodName = "Execute",
                TypeName = job.TypeName,
                AssemblyName = job.AssemblyName,
                IsStaticMethod = true
            };

            if (result.NextExecution == DateTime.MinValue)
            {
                result.NextExecution = DateTime.Today;
            }

            _actualRepository.Save(result);

            return result;
        }

        public JobDescriptionViewModel GetById(int id)
        {
            return GetList().FirstOrDefault(j => j.Id == id);
        }
    }
}
