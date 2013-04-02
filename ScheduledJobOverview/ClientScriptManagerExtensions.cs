using System.Text;
using System.Web.UI;
using EPiServer.Shell;

namespace TechFellow.ScheduledJobOverview
{
    public static class ClientScriptManagerExtensions
    {
        public static string GetImageIncludes(this ClientScriptManager clientScript, string file)
        {
#if !ADDON
            return clientScript.GetWebResourceUrl(typeof(InitializeModule), Const.ModuleName + ".Images." + file);
#else
            return Paths.ToClientResource(typeof(ClientScriptManagerExtensions), "Images/" + file);
#endif
        }

        public static string GetJavascriptIncludes(this ClientScriptManager clientScript, string file)
        {
            var builder = new StringBuilder();
            builder.Append("<script type=\"text/javascript\" src=\"");
#if !ADDON
            builder.Append(clientScript.GetWebResourceUrl(typeof(InitializeModule), Const.ModuleName + ".Scripts." + file));
#else
            builder.Append(Paths.ToClientResource(typeof(ClientScriptManagerExtensions), "Scripts/" + file));
#endif
            builder.Append("\"></script>");
            return builder.ToString();
        }

        public static string GetResourceUrl(this ClientScriptManager clientScript, string url)
        {
#if !ADDON
            return string.Format("/modules/{0}/{1}/", Const.ModuleName, url);
#else
            return Paths.ToClientResource(typeof(ClientScriptManagerExtensions), url + "/");
#endif
        }
    }
}
