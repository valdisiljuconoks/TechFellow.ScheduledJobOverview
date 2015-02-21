<%@ Page Title="" Language="C#" MasterPageFile="../Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Assembly Name="TechFellow.ScheduledJobOverview" %>
<%@ Import Namespace="TechFellow.ScheduledJobOverview" %>
<%@ Import Namespace="EPiServer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">Scheduled Job Overview</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #sch-app-root .red-bold-false {
            color: red;
            font-weight: bold;
        }
    </style>
    <div ng-controller="scheduledJobsController as vm" id="sch-app-root" ng-cloak ng-init="vm.fetch()" data-details-url="<%= UriSupport.ResolveUrlFromUIBySettings("Admin/DatabaseJob.aspx") %>" data-service-url="<%= Page.GetResourceUrl("Overview") %>">
        <div class="epi-padding" style="display: block;">
            <div style="margin-bottom: 15px;">
                <div style="display: inline; margin-right: 5px;">
                    <input id="auto-refresh" type="checkbox" ng-model="vm.autoRefresh" style="margin: 5px"/><label for="auto-refresh">Auto refresh</label>
                </div>
                <div style="display: inline">
                    <input type="text" style="width: 200px; position: relative;" placeholder="Type to filter..." ng-model="vm.filter"/>
                </div>
            </div>
            <table class="epi-default" style="border-collapse: collapse; border-style: none; width: 95%">
                <thead>
                <th class="epitableheading" scope="col">Running</th>
                <th class="epitableheading" scope="col">Name</th>
                <th class="epitableheading" scope="col">Description</th>
                <th class="epitableheading" scope="col">Enabled</th>
                <th class="epitableheading" scope="col">Interval</th>
                <th class="epitableheading" scope="col">Successful last execute</th>
                <th class="epitableheading" scope="col">Last execute date</th>
                <th class="epitableheading" scope="col">Actions</th>
                </thead>
                <tbody>
                <tr ng-repeat="job in vm.jobs | filter:vm.filter">
                    <td style="text-align: center">
                        <img src="<%= Page.ClientScript.GetImageIncludes("spinner.gif") %>" alt="{{job.IsRunning}}" ng-show="job.IsRunning"/>
                    </td>
                    <td style="white-space: nowrap">{{job.Name}}</td>
                    <td>{{job.Description}}</td>
                    <td class="red-bold-{{job.IsEnabled}}">
                        <toggle-bool target-prop="{{job.IsEnabled}}"/>
                    </td>
                    <td style="white-space: nowrap">{{job.Interval}}</td>
                    <td class="red-bold-{{job.IsLastExecuteSuccessful}}">
                        <toggle-bool target-prop="{{job.IsLastExecuteSuccessful}}"/>
                    </td>
                    <td style="white-space: nowrap">{{job.LastExecuteDisplay}}</td>
                    <td style="text-align: center; white-space: nowrap">
                        <span class="epi-cmsButton">
                                <input type="button" value="" alt="Details" title="Details" ng-click="vm.showDetails(job.Id)" class="epi-cmsButton-tools epi-cmsButton-ViewMode">
                            </span>
                        <span class="epi-cmsButton">
                                <input type="button" value="" alt="Execute manually" title="Execute manually" ng-click="vm.executeJob(job.Id)" class="epi-cmsButton-tools epi-cmsButton-MySettings">
                            </span>
                        <span class="epi-cmsButton">
                                <input type="button" value="" alt="Stop" title="Stop" ng-click="vm.stopJob(job.Id)" class="epi-cmsButton-text epi-cmsButton-tools epi-cmsButton-Cancel">
                            </span>
                    </td>
                </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>