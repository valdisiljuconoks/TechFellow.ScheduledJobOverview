using System;

namespace TechFellow.ScheduledJobOverview
{
    internal static class UrlToResourceHelper
    {
        public static string TranslateToResource(string url)
        {
            url = url.Substring(url.IndexOf(Const.ModuleName, StringComparison.Ordinal));
            if (url.StartsWith("/"))
            {
                url = url.Substring(1);
            }

            return url.Replace('/', '.');
        }
    }
}
