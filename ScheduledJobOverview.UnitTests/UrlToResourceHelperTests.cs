using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechFellow.ScheduledJobOverview;

namespace ScheduledJobOverview.UnitTests
{
    [TestClass]
    public class UrlToResourceHelperTests
    {
        [TestMethod]
        public void Translation_WithFolders_Success()
        {
            var resource = UrlToResourceHelper.TranslateToResource("~/modules/" + Const.ModuleName + "/Views/Overview/Index.aspx");
            Assert.AreEqual("TechFellow.ScheduledJobOverview.Views.Overview.Index.aspx", resource);
        }
    }
}
