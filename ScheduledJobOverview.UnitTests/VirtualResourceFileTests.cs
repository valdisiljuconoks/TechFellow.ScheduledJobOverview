using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechFellow.ScheduledJobOverview;

namespace ScheduledJobOverview.UnitTests
{
    [TestClass]
    public class VirtualResourceFileTests
    {
        [TestMethod]
        public void GetVirtualFile_Subdirecotry_Success()
        {
            var file = new ResourceVirtualFile("~/modules/" + Const.ModuleName + "/Views/Overview/Index.aspx");
            Assert.IsNotNull(file.Open());
        }
    }
}
