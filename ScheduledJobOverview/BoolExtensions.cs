namespace TechFellow.ScheduledJobOverview
{
    public static class BoolExtensions
    {
        public static string ToYesNo(this bool val)
        {
            return val ? "Yes" : "No";
        }
    }
}
