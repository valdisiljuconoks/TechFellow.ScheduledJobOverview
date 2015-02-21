using System;
using System.Threading;
using EPiServer.BaseLibrary.Scheduling;
using EPiServer.DataAbstraction;
using EPiServer.PlugIn;

namespace ScheduledJobOverview.Sandbox
{
    [ScheduledPlugIn(DisplayName = "Scheduled Job1", Description = "This is a description", HelpFile = "This is a help file", DefaultEnabled = true,
        IntervalLength = 5, IntervalType = ScheduledIntervalType.Minutes)]
    public class ScheduledJob1 : JobBase
    {
        private bool _stopSignaled;

        public ScheduledJob1()
        {
            IsStoppable = true;
        }

        /// <summary>
        ///     Called when a user clicks on Stop for a manually started job, or when ASP.NET shuts down.
        /// </summary>
        public override void Stop()
        {
            _stopSignaled = true;
        }

        /// <summary>
        ///     Called when a scheduled job executes
        /// </summary>
        /// <returns>A status message to be stored in the database log and visible from admin mode</returns>
        public override string Execute()
        {
            //Call OnStatusChanged to periodically notify progress of job for manually started jobs
            OnStatusChanged(String.Format("Starting execution of {0}", this.GetType()));

            //Add implementation
            WaitHandle wh = new AutoResetEvent(false);
            wh.WaitOne(TimeSpan.FromSeconds(30));

            //For long running jobs periodically check if stop is signaled and if so stop execution
            if (_stopSignaled)
            {
                return "Stop of job was called";
            }

            return "Change to message that describes outcome of execution";
        }
    }
}
