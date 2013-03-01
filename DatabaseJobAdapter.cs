using System;
using System.Web;
using System.Web.UI.Adapters;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TechFellow.ScheduledJobOverview
{
    public class DatabaseJobAdapter : PageAdapter
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            var navigateButton = new Button
                                     {
                                             Text = "Scheduled job overview",
                                             ToolTip = "Navigate to scheduled job overview page.",
                                             CssClass = "epi-cmsButton-tools epi-cmsButton-text epi-cmsButton-Report"
                                     };

            navigateButton.Click += NavigateButtonOnClick;

            var htmlGenericControl3 = new HtmlGenericControl("span");
            htmlGenericControl3.Attributes.Add("class", "epi-cmsButton");
            htmlGenericControl3.Attributes.Add("style", "float: right;");
            htmlGenericControl3.Controls.Add(navigateButton);

            Control.FindControlRecursively("GeneralSettings").Controls.Add(htmlGenericControl3);
        }

        private void NavigateButtonOnClick(object sender, EventArgs eventArgs)
        {
            HttpContext.Current.Response.RedirectToRoute("ScheduledJobPlugin");
        }
    }
}
