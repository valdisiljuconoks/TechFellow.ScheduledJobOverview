using System.Web.Mvc;

namespace TechFellow.ScheduledJobOverview
{
    public class CustomViewEngine : WebFormViewEngine
    {
        public CustomViewEngine()
        {
            ViewLocationFormats = new[] { "~/modules/" + Const.ModuleName + "/Views/{1}/{0}.aspx" };
        }
    }
}
