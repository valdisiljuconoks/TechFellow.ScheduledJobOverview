using System;
using EPiServer.UI;
using TechFellow.ScheduledJobOverview.Models;

namespace TechFellow.ScheduledJobOverview
{
    public partial class overview : SystemPageBase
    {
        protected JobDescriptionViewModel Item
        {
            get
            {
                return Page.GetDataItem() as JobDescriptionViewModel;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (IsPostBack)
            {
                return;
            }

            var repository = new JobRepository();
            this.rptJobs.DataSource = repository.GetList();
            this.rptJobs.DataBind();
        }
    }
}
