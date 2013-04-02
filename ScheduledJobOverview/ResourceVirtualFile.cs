using System.IO;
using System.Web;
using System.Web.Hosting;

namespace TechFellow.ScheduledJobOverview
{
    internal class ResourceVirtualFile : VirtualFile
    {
        private readonly string fileName;

        public ResourceVirtualFile(string virtualPath) : base(virtualPath)
        {
            this.fileName = VirtualPathUtility.ToAppRelative(virtualPath);
        }

        public override Stream Open()
        {
            return GetType().Assembly.GetManifestResourceStream(ResourceProvider.TranslateToResource(this.fileName));
        }
    }
}
