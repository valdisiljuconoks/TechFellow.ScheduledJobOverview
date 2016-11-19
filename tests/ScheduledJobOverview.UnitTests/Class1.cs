using Xunit;

namespace ScheduledJobOverview.UnitTests
{
    public class Class1
    {
        [Fact]
        public void Test1()
        {
            BaseClass a = new ChildClass();
            var result = a.GetString();

            Assert.Equal("child", result);
        }
    }

    public class ChildClass : BaseClass
    {
        public new string GetString()
        {
            return "child";
        }
    }

    public class BaseClass
    {
        public string GetString()
        {
            return "base";
        }
    }
}
