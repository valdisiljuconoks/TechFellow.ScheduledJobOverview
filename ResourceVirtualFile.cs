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
            var param = this.fileName.Split(new[] { '/' });
            var viewName = param[3];

            return GetType().Assembly.GetManifestResourceStream(viewName);
        }
    }
}
