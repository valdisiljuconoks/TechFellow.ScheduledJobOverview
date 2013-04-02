﻿using System.Web.UI;
using EPiServer.Shell;

namespace TechFellow.ScheduledJobOverview
{
    public static class ClientScriptManagerExtensions
    {
        public static string GetImageIncludes(this ClientScriptManager clientScript, string file)
        {
            return RuntimeInfo.IsModule()
                           ? Paths.ToClientResource(typeof(ClientScriptManagerExtensions), "Images/" + file)
                           : clientScript.GetWebResourceUrl(typeof(InitializeModule), ResourceProvider.CreateResourceUrl("Images", file));
        }

        public static string GetJavascriptIncludes(this ClientScriptManager clientScript, string file)
        {
            return string.Format("<script type=\"text/javascript\" src=\"{0}\"></script>",
                                 RuntimeInfo.IsModule()
                                         ? Paths.ToClientResource(typeof(ClientScriptManagerExtensions), "Scripts/" + file)
                                         : clientScript.GetWebResourceUrl(typeof(InitializeModule), ResourceProvider.CreateResourceUrl("Scripts", file)));
        }
    }
}
