using System;
using System.Threading;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Personalization;
using EPiServer.PlugIn;
using EPiServer.Scheduler;

namespace SampleWebApp
{
    [ScheduledPlugIn(DisplayName = "ScheduledJob2")]
    public class ScheduledJob2 : ScheduledJobBase
    {
        private readonly IContentTypeRepository _repo;
        private bool _stopSignaled;

        public ScheduledJob2(IContentTypeRepository repo)
        {
            _repo = repo;
            IsStoppable = true;
        }

        /// <summary>
        /// Called when a user clicks on Stop for a manually started job, or when ASP.NET shuts down.
        /// </summary>
        public override void Stop()
        {
            _stopSignaled = true;
        }

        /// <summary>
        /// Called when a scheduled job executes
        /// </summary>
        /// <returns>A status message to be stored in the database log and visible from admin mode</returns>
        public override string Execute()
        {
            //Call OnStatusChanged to periodically notify progress of job for manually started jobs
            OnStatusChanged(String.Format("Starting execution of {0}", this.GetType()));

            //Add implementation
            Thread.Sleep(TimeSpan.FromSeconds(10));

            //For long running jobs periodically check if stop is signaled and if so stop execution
            if (_stopSignaled)
            {
                return "Stop of job was called";
            }

            return "Change to message that describes outcome of execution";
        }
    }
}
