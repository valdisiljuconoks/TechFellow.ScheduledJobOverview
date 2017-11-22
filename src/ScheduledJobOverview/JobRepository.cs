using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer.DataAbstraction;
using EPiServer.PlugIn;
using EPiServer.Scheduler;
using TechFellow.ScheduledJobOverview.Models;

namespace TechFellow.ScheduledJobOverview
{
    public class JobRepository
    {
        private readonly IScheduledJobRepository _repo;
        private readonly IScheduledJobLogRepository _logRepo;

        public JobRepository(IScheduledJobRepository repo, IScheduledJobLogRepository logRepo)
        {
            _repo = repo;
            _logRepo = logRepo;
        }

        public IEnumerable<JobDescriptionViewModel> GetList()
        {
            var plugIns = PlugInLocator.Search(new ScheduledPlugInAttribute()).ToList();

            // get actual activated jobs (ran at least once)
            var actual = from job in _repo.List()
                          let type = Type.GetType($"{job.TypeName}, {job.AssemblyName}")
                          select new JobDescriptionViewModel
                                 {
                                     Id = -1,
                                     InstanceId = job.ID,
                                     Name = job.Name,
                                     Exists = false,
                                     TypeName = job.TypeName,
                                     AssemblyName = job.AssemblyName
                          };

            // get all scheduled jobs (even inactive)
            var plugins = (from plugin in plugIns
                           let job = _repo.List().FirstOrDefault(j => j.TypeName == plugin.TypeName && j.AssemblyName == plugin.AssemblyName)
                           let attr = plugin.GetAttribute<ScheduledPlugInAttribute>()
                           let lastLog = _logRepo.GetAsync(job.ID, 0, 1).GetAwaiter().GetResult().PagedResult.FirstOrDefault()
                    select new JobDescriptionViewModel
                    {
                        Id = plugin.ID,
                        InstanceId = job != null ? job.ID : Guid.Empty,
                        Name = attr.DisplayName,
                        Description = attr.Description.Cut(150),
                        IsEnabled = job != null && job.IsEnabled,
                        Interval = job != null ? $"{job.IntervalLength} ({job.IntervalType})" : "",
                        IsLastExecuteSuccessful = job != null && !job.HasLastExecutionFailed ? true : (bool?)null,
                        LastExecute = job != null ? (job.LastExecution != DateTime.MinValue ? job.LastExecution : (DateTime?)null) : null,
                        LastMessage = job.LastExecutionMessage,
                        LastDuration = lastLog!= null ? (lastLog.Duration.HasValue ? $"{lastLog.Duration.Value.Milliseconds}ms" : string.Empty) : string.Empty,
                        AssemblyName = plugin.AssemblyName,
                        TypeName = plugin.TypeName,
                        IsRunning = job != null && job.IsRunning,
                        IsRestartable = job.Restartable
                    }).OrderBy(j => j.Name).ToList();

            // return distinct list
            return plugins.Concat(actual)
                .GroupBy(j => j.TypeName)
                .Select(g => g.First())
                .ToList();
        }

        public ScheduledJob Get(Guid instanceId)
        {
            return _repo.Get(instanceId);
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
                result.NextExecution = DateTime.Today;

            _repo.Save(result);

            return result;
        }

        public JobDescriptionViewModel GetById(int id)
        {
            return GetList().FirstOrDefault(j => j.Id == id);
        }

        public void Delete(Guid id)
        {
            _repo.Delete(id);
        }

        public JobDescriptionViewModel GetByInstanceId(Guid id)
        {
            return GetList().FirstOrDefault(j => j.InstanceId == id);
        }
    }
}
