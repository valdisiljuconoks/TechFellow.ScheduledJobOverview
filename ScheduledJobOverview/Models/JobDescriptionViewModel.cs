using System;

namespace TechFellow.ScheduledJobOverview.Models
{
    public class JobDescriptionViewModel
    {
        public int Id { get; set; }
        public Guid InstanceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AssemblyName { get; set; }
        public string TypeName { get; set; }
        public DateTime? LastExecute { get; set; }
        public bool IsEnabled { get; set; }
        public bool? IsLastExecuteSuccessful { get; set; }
        public string Interval { get; set; }
    }
}
