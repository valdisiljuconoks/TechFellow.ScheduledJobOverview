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
        public List<JobDescriptionViewModel> GetList()
        {
            var plugIns = PlugInLocator.Search(new ScheduledPlugInAttribute()).ToList();

            return (from plugin in plugIns
                    let job = ScheduledJob.List().FirstOrDefault(j => j.TypeName == plugin.TypeName &&
                                                                      j.AssemblyName == plugin.AssemblyName)
                    let attr = plugin.GetAttribute<ScheduledPlugInAttribute>()
                    select new JobDescriptionViewModel
                               {
                                       Id = plugin.ID,
                                       InstanceId = job != null ? job.ID : Guid.Empty,
                                       Name = attr.DisplayName,
                                       Description = attr.Description,
                                       IsEnabled = job != null && job.IsEnabled,
                                       Interval = job != null ? string.Format("{0} ({1})", job.IntervalLength, job.IntervalType) : "",
                                       IsLastExecuteSuccessful = (job != null && !job.HasLastExecutionFailed ? true : (bool?)null),
                                       LastExecute = job != null ? job.LastExecution : (DateTime?)null,
                                       AssemblyName = plugin.AssemblyName,
                                       TypeName = plugin.TypeName,
                               }).OrderBy(j => j.Name).ToList();
        }
    }
}
