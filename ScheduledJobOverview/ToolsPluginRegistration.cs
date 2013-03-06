using EPiServer.PlugIn;

namespace TechFellow.ScheduledJobOverview
{
#if ADDON
    [GuiPlugIn(DisplayName = "Scheduled jobs overview", UrlFromModuleFolder = "Overview", Area = PlugInArea.AdminMenu)]
#else
    [GuiPlugIn(DisplayName = "Scheduled jobs overview", Url = "~/modules/" + Const.ModuleName + "/Overview", Area = PlugInArea.AdminMenu)]
#endif
    public class ToolsPluginRegistration
    {
    }
}
