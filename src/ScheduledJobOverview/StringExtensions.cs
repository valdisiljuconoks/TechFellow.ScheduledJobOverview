namespace TechFellow.ScheduledJobOverview
{
    internal static class StringExtensions
    {
        public static string Cut(this string target, int count)
        {
            if (string.IsNullOrEmpty(target))
                return target;

            if (target.Length <= count)
                return target;

            return target.Substring(0, count) + "...";
        }
    }
}