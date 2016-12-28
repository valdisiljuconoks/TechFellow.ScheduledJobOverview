using System;
using EPiServer.Shell.WebForms;

namespace TechFellow.ScheduledJobOverview.modules._protected.TechFellow.ScheduledJobOverview
{
    public partial class Index : WebFormsBase
    {
        public Index()
        {
            PreRender += OnPreRender;
        }

        private void OnPreRender(object sender, EventArgs eventArgs)
        {
            DataBind();
        }
    }
}
