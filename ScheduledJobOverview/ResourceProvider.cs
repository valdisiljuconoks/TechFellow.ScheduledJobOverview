using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

namespace TechFellow.ScheduledJobOverview
{
    public class ResourceProvider : VirtualPathProvider
    {
        private static readonly List<string> resources = typeof(ResourceProvider).Assembly.GetManifestResourceNames().ToList();

        public override bool FileExists(string virtualPath)
        {
            return ShouldHandle(virtualPath) || base.FileExists(virtualPath);
        }

        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            return ShouldHandle(virtualPath)
                           ? new CacheDependency((string)null)
                           : base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            return ShouldHandle(virtualPath)
                           ? new ResourceVirtualFile(virtualPath)
                           : base.GetFile(virtualPath);
        }

        private static bool ShouldHandle(string virtualPath)
        {
            return VirtualPathUtility.ToAppRelative(virtualPath).Contains(Const.ModuleName)
                   && resources.Contains(UrlToResourceHelper.TranslateToResource(virtualPath));
        }
    }
}
