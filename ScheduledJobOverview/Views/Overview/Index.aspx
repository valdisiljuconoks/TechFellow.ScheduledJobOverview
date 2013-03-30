<%@ Page Title="" Language="C#" MasterPageFile="../Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Assembly Name="TechFellow.ScheduledJobOverview" %>
<%@ Import Namespace="EPiServer" %>
<%@ Import Namespace="EPiServer.Shell.Web.Mvc.Html" %>
<%@ Import Namespace="TechFellow.ScheduledJobOverview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">Scheduled Job Overview</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #sch-app-root .red-bold-false {
            color: red;
            font-weight: bold;
        }
    </style>
    <div data-ng-controller="scheduledJobsController" id="sch-app-root" data-details-url="<%= UriSupport.ResolveUrlFromUIBySettings("Admin/DatabaseJob.aspx") %>">
        <div class="epi-padding" style="display: block;">
            <input id="auto-refresh" type="checkbox" data-ng-model="autoRefresh"  style="margin: 5px"/><label for="auto-refresh">Auto refresh</label>
            <table class="epi-default" style="border-collapse: collapse; border-style: none; width: 95%">
                <tbody>
                    <th class="epitableheading" scope="col">Running</th>
                    <th class="epitableheading" scope="col">Name</th>
                    <th class="epitableheading" scope="col">Description</th>
                    <th class="epitableheading" scope="col">Enabled</th>
                    <th class="epitableheading" scope="col">Interval</th>
                    <th class="epitableheading" scope="col">Successful last execute</th>
                    <th class="epitableheading" scope="col">Last execute date</th>
                    <th class="epitableheading" scope="col">Details</th>
                    <tr data-ng-repeat="job in jobs">
                        <td style="text-align: center"><img src="<%= Page.ClientScript.GetImageIncludes("spinner.gif") %>" alt="{{job.isRunning}}" data-ng-show="job.isRunning" /></td>
                        <td style="white-space: nowrap">{{job.name}}</td>
                        <td>{{job.description}}</td>
                        <td class="red-bold-{{job.isEnabled}}"><toggle-bool target-prop="{{job.isEnabled}}" /></td>
                        <td style="white-space: nowrap">{{job.interval}}</td>
                        <td class="red-bold-{{job.isLastExecuteSuccessful}}"><toggle-bool target-prop="{{job.isLastExecuteSuccessful}}" /></td>
                        <td style="white-space: nowrap">{{job.lastExecuteDisplay}}</td>
                        <td style="text-align: center"><span class="epi-cmsButton">
                            <input type="button" value="" data-ng-click="showDetails(job.id)" class="epi-cmsButton-tools epi-cmsButton-ViewMode"></span>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
