using EPiServer.PlugIn;

namespace TechFellow.ScheduledJobOverview
{
    [GuiPlugIn(DisplayName = "Scheduled Jobs Overview", UrlFromModuleFolder = "index.aspx", Area = PlugInArea.AdminMenu)]
    public class ToolsPluginRegistration { }
}
