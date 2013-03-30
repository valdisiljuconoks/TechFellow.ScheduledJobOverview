using System.Text;
using System.Web.UI;

namespace TechFellow.ScheduledJobOverview
{
    public static class ClientScriptManagerExtensions
    {
        public static string GetImageIncludes(this ClientScriptManager clientScript, string file)
        {
            return clientScript.GetWebResourceUrl(typeof(InitializeModule), Const.ModuleName + ".Images." + file);
        }

        public static string GetJavascriptIncludes(this ClientScriptManager clientScript, string file)
        {
            var builder = new StringBuilder();
#if !ADDON
            builder.Append("<script type=\"text/javascript\" src=\"");
            builder.Append(clientScript.GetWebResourceUrl(typeof(InitializeModule), Const.ModuleName + ".Scripts." + file));
            builder.Append("\"></script>");
#else
            builder.Append(Paths.ToClientResource(typeof(ClientScriptManagerExtensions), "Scripts/" + file));
#endif
            return builder.ToString();
        }
    }
}
