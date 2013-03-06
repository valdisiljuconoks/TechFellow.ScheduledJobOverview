using EPiServer.Shell;

namespace TechFellow.ScheduledJobOverview
{
    public class ModuleUriHelper
    {
        public static string ModulePath
        {
            get
            {
                return Paths.ToResource(typeof(ModuleUriHelper), "");
            }
        }
    }
}