using EPiServer.ServiceLocation;
using EPiServer.Shell.Modules;

namespace TechFellow.ScheduledJobOverview
{
    public static class RuntimeInfo
    {
        private static bool isInitialized;
        private static bool isModule;
        private static readonly object syncRoot = new object();

        public static bool IsModule()
        {
            if (isInitialized)
            {
                return isModule;
            }

            if (ServiceLocator.Current == null)
            {
                return isModule;
            }

            lock (syncRoot)
            {
                ShellModule module;
                isModule = ServiceLocator.Current.GetInstance<ModuleTable>().TryGetModule(typeof(RuntimeInfo).Assembly, out module);
                isInitialized = true;
            }

            return isModule;
        }
    }
}
