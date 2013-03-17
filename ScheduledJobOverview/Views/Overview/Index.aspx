<%@ Page Title="" Language="C#" MasterPageFile="../Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<TechFellow.ScheduledJobOverview.Models.JobDescriptionViewModel>>" %>
<%@ Assembly Name="TechFellow.ScheduledJobOverview" %>
<%@ Import Namespace="EPiServer" %>
<%@ Import Namespace="EPiServer.Shell.Web.Mvc.Html" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">Scheduled Job Overview</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function showDetails(ix) {
            window.location = '<%= UriSupport.ResolveUrlFromUIBySettings("Admin/DatabaseJob.aspx") + "?pluginId=" %>' + ix;
        }
    </script>
    <div class="epi-padding" style="display: block;">
        <table class="epi-default" style="border-collapse: collapse; border-style: none; width: 95%">
            <tbody>
                <th class="epitableheading" scope="col">Name</th>
                <th class="epitableheading" scope="col">Description</th>
                <th class="epitableheading" scope="col">Enabled</th>
                <th class="epitableheading" scope="col">Interval</th>
                <th class="epitableheading" scope="col">Successful last execute</th>
                <th class="epitableheading" scope="col">Last execute date</th>
                <th class="epitableheading" scope="col">Details</th>
                <% foreach (var item in Model) { %>
                <tr>
                    <td style="white-space: nowrap"><%= item.Name %></td>
                    <td><%= item.Description %></td>
                    <td <%= !item.IsEnabled ? " style=\"color: red; font-weight: bold\"" : "" %>><%= item.IsEnabled ? "Yes" : "No" %></td>
                    <td style="white-space: nowrap"><%= item.Interval %></td>
                    <td <%= item.IsLastExecuteSuccessful.HasValue && !item.IsLastExecuteSuccessful.Value ? " style=\"color: red; font-weight: bold\"" : "" %>><%= item.IsLastExecuteSuccessful.HasValue ? (item.IsLastExecuteSuccessful.Value ? "Yes" : "No") : "" %></td>
                    <td style="white-space: nowrap"><%= (item.LastExecute.HasValue && item.LastExecute.Value != DateTime.MinValue) ? item.LastExecute.Value.ToString("yyyy-MM-dd mm:ss") : "" %></td>
                    <td><span class="epi-cmsButton"><input type="button" value="" onclick=" showDetails(<%= item.Id %>); return false; " class="epi-cmsButton-tools epi-cmsButton-ViewMode"></span></td>
                </tr>
                <% } %>
            </tbody>
        </table>
    </div>
</asp:Content>