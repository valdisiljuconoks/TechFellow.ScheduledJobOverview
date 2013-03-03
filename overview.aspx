<%@ Page Language="C#" EnableEventValidation="false" EnableViewState="false" Inherits="TechFellow.ScheduledJobOverview.overview" MasterPageFile="/modules/TechFellow.ScheduledJobOverview/TechFellow.ScheduledJobOverview.Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainRegion" runat="server">
    <script type="text/javascript">
        function showDetails(ix) {
            window.location = '<%= ResolveUrlFromUI("Admin/DatabaseJob.aspx") + "?pluginId="%>' + ix;
        }
    </script>
    <div id="FullRegion_MainRegion_Log" class="epi-padding" style="display: block;">
        <asp:Repeater ID="rptJobs" runat="server">
            <HeaderTemplate>
                <table class="epi-default" style="border-collapse: collapse; border-style: None;">
                <tbody>
                <th class="epitableheading" scope="col">Name</th>
                <th class="epitableheading" scope="col">Description</th>
                <th class="epitableheading" scope="col">Enabled</th>
                <th class="epitableheading" scope="col">Interval</th>
                <th class="epitableheading" scope="col">Successful last execute</th>
                <th class="epitableheading" scope="col">Last execute date</th>
                <th class="epitableheading" scope="col">Details</th>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td style="white-space: nowrap"><%# Item.Name %></td>
                    <td><%# Item.Description %></td>
                    <td <%# !Item.IsEnabled ? " style=\"color: red; font-weight: bold\"" : "" %>><%# Item.IsEnabled ? "Yes" : "No" %></td>
                    <td style="white-space: nowrap"><%# Item.Interval %></td>
                    <td <%# Item.IsLastExecuteSuccessful.HasValue && !Item.IsLastExecuteSuccessful.Value ? " style=\"color: red; font-weight: bold\"" : "" %>><%# Item.IsLastExecuteSuccessful.HasValue ? (Item.IsLastExecuteSuccessful.Value ? "Yes" : "No") : "" %></td>
                    <td style="white-space: nowrap"><%# (Item.LastExecute.HasValue && Item.LastExecute.Value != DateTime.MinValue) ? Item.LastExecute.Value.ToString("yyyy-MM-dd mm:ss") : "" %></td>
                    <td><span class="epi-cmsButton"><input type="button" value="" onclick="showDetails(<%# Item.Id %>); return false;" class="epi-cmsButton-tools epi-cmsButton-ViewMode"></span></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
            </tbody>
            </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>