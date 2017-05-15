using System;
using System.Threading;
using EPiServer.DataAbstraction;
using EPiServer.PlugIn;
using EPiServer.Scheduler;

namespace SampleWebApp
{
    [ScheduledPlugIn(DisplayName = "ScheduledJob1",
        GUID = "26FD80FC-92E9-4FED-B910-6C4DFA88AA5B",
        Restartable = true,
        DefaultEnabled = true,
        IntervalLength = 30,
        IntervalType = ScheduledIntervalType.Seconds)]
    public class ScheduledJob1 : ScheduledJobBase
    {
        private bool _stopSignaled;

        public ScheduledJob1()
        {
            IsStoppable = true;
        }

        public override void Stop()
        {
            _stopSignaled = true;
        }

        public override string Execute()
        {
            OnStatusChanged($"Starting execution of {this.GetType()}");

            for (var i = 0; i < 1000; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(30));
            }

            if(_stopSignaled)
            {
                return "Stop of job was called";
            }

            return "Change to message that describes outcome of execution";
        }
    }
}
