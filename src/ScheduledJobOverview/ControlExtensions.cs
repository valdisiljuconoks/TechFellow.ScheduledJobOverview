using System.Linq;
using System.Web.UI;

namespace TechFellow.ScheduledJobOverview
{
    public static class ControlExtensions
    {
        public static Control FindControlRecursively(this Control root, string controlId)
        {
            return controlId.Equals(root.ID)
                           ? root
                           : (root.Controls).Cast<Control>().Select((control => control.FindControlRecursively(controlId))).FirstOrDefault((c => c != null));
        }
    }
}
