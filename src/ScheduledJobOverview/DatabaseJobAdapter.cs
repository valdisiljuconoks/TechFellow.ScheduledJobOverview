using System;
using System.Web;
using System.Web.UI.Adapters;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EPiServer.Shell;

namespace TechFellow.ScheduledJobOverview
{
    public class DatabaseJobAdapter : PageAdapter
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            var overviewButton = new Button
            {
                Text = "Scheduled job overview",
                ToolTip = "Navigate to scheduled job overview page.",
                CssClass = "epi-cmsButton-tools epi-cmsButton-text epi-cmsButton-File"
            };

            overviewButton.Click += GotoOverviewPage;

            var span = new HtmlGenericControl("span");
            span.Attributes.Add("class", "epi-cmsButton");
            span.Attributes.Add("style", "float: right;");
            span.Controls.Add(overviewButton);

            Control.FindControlRecursively("GeneralSettings").Controls.Add(span);

            var statsButton = new Button
            {
                Text = "Show Statistics",
                ToolTip = "Navigate to scheduled job statistics page.",
                CssClass = "epi-cmsButton-tools epi-cmsButton-text epi-cmsButton-Report"
            };

            statsButton.Click += GotoStatisticsPage;

            var span2 = new HtmlGenericControl("span");
            span2.Attributes.Add("class", "epi-cmsButton");
            span2.Attributes.Add("style", "float: right;");
            span2.Controls.Add(statsButton);

            Control.FindControlRecursively("GeneralSettings").Controls.Add(span2);
        }

        private void GotoOverviewPage(object sender, EventArgs eventArgs)
        {
            var url = Paths.ToResource(typeof(DatabaseJobAdapter), "Index.aspx");
            HttpContext.Current.Response.Redirect(url);
        }

        private void GotoStatisticsPage(object sender, EventArgs eventArgs)
        {
            var url = Paths.ToResource(typeof(DatabaseJobAdapter), "Charts.aspx");
            HttpContext.Current.Response.Redirect(url + $"?pluginId={Page.Request["pluginId"]}");
        }
    }
}
