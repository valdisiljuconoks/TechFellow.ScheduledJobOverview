<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="true" CodeBehind="Index.aspx.cs" Inherits="TechFellow.ScheduledJobOverview.modules._protected.TechFellow.ScheduledJobOverview.Index" %>
<%@ Import Namespace="EPiServer" %>
<%@ Import Namespace="EPiServer.Shell" %>
<%@ Import Namespace="TechFellow.ScheduledJobOverview" %>
<%@ Import Namespace="EPiServer.Framework.Web" %>

<asp:content id="Content2" contentplaceholderid="FullRegion" runat="server">
    <style>
        body { padding: 25px; }

        #sch-app-root .red-bold-false {
            color: red;
            font-weight: bold;
        }

        .job-inactive {
            color: darkgrey;
        }
        
        .job-deleted {
            color: grey;
            background-color: darksalmon;
            text-decoration: line-through;
        }
    </style>

    <div class="epi-contentArea">
        <h1 class="EP-prefix">Scheduled Jobs Overview</h1>
    </div>

    <div ng-app="schApp">

        <div ng-controller="scheduledJobsController as vm" id="sch-app-root" ng-cloak ng-init="vm.fetch()" data-details-url="<%= UriSupport.ResolveUrlFromUIBySettings("Admin/DatabaseJob.aspx") %>" data-service-url="<%= Paths.ToResource(typeof (JobRepository), "stores/joboverview/") %>" data-anti-forgery="<%= new AspNetAntiForgery(this).CreateValidationToken() %>">
            <div class="epi-padding" style="display: block;">
                <div style="margin-bottom: 15px;">
                    <div style="display: inline; margin-right: 5px;">
                        <input id="auto-refresh" type="checkbox" ng-model="vm.autoRefresh" style="margin: 5px"/><label for="auto-refresh">Auto refresh</label>
                    </div>
                    <div style="display: inline">
                        <input type="text" style="position: relative; width: 200px;" placeholder="Type to filter..." ng-model="vm.filter"/>
                    </div>
                </div>
                <table class="epi-default" style="border-collapse: collapse; border-style: none;">
                    <thead>
                    <th class="epitableheading" scope="col">Running</th>
                    <th class="epitableheading" scope="col">Name</th>
                    <th class="epitableheading" scope="col">Enabled</th>
                    <th class="epitableheading" scope="col">Interval</th>
                    <th class="epitableheading" scope="col">Successful</th>
                    <th class="epitableheading" scope="col">Last execute date</th>
                    <th class="epitableheading" scope="col">Duration</th>
                    <th class="epitableheading" scope="col">Type Name</th>
                    <th class="epitableheading" scope="col">Description</th>
                    <th class="epitableheading" scope="col">Actions</th>
                    </thead>
                    <tbody>
                    <tr ng-repeat="job in vm.jobs | filter:vm.filter" ng-class="{'job-inactive': !job.isEnabled, 'job-deleted': !job.exists}">
                        <td style="text-align: center">
                            <img src="<%= Paths.ToClientResource(typeof (JobRepository), "spinner.gif") %>" alt="{{job.isRunning}}" title="{{job.lastMessage}}" ng-show="job.isRunning"/>
                        </td>
                        <td style="white-space: nowrap">{{job.name}}</td>
                        <td>
                            <toggle-bool target-prop="{{job.isEnabled}}"/>
                        </td>
                        <td style="white-space: nowrap">{{job.interval}}</td>
                        <td class="red-bold-{{job.isLastExecuteSuccessful}}">
                            <toggle-bool target-prop="{{job.isLastExecuteSuccessful}}"/>
                        </td>
                        <td style="white-space: nowrap">{{job.lastExecuteDisplay}}</td>
                        <td style="white-space: nowrap">{{job.lastDuration}}</td>
                        <td style="white-space: nowrap">{{job.typeName}}</td>
                        <td>{{job.description}}</td>
                        <td style="text-align: center; white-space: nowrap">
                            <span class="epi-cmsButton" ng-show="job.exists">
                                <input type="button" value="" alt="Details" title="Details" ng-click="vm.showDetails(job.id)" class="epi-cmsButton-tools epi-cmsButton-ViewMode">
                            </span>
                            <span class="epi-cmsButton" ng-show="job.exists">
                                <input type="button" value="" alt="Execute manually" title="Execute manually" ng-click="vm.executeJob(job.id)" class="epi-cmsButton-tools epi-cmsButton-MySettings">
                            </span>
                            <span ng-class="{'epi-cmsButtondisabled': !job.isRunning, 'epi-cmsButton': job.isRunning}" ng-show="job.exists">
                                <input type="button" value="" alt="Stop" title="Stop" ng-click="vm.stopJob(job.id)" class="epi-cmsButton-tools epi-cmsButton-Cancel">
                            </span>
                            <span class="epi-cmsButton" ng-show="!job.exists">
                                <input type="button" value="" alt="Delete" title="Delete" ng-click="vm.deleteJob(job.instanceId)" class="epi-cmsButton-tools epi-cmsButton-Delete">
                            </span>
                            <span class="epi-cmsButton">
                                <input type="button" value="" alt="Stats" title="Stats" ng-click="vm.showStats(job.id)" class="epi-cmsButton-tools epi-cmsButton-Report">
                            </span>
                        </td>
                    </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>

    <script src="<%= Paths.ToClientResource(typeof (JobRepository), "angular.min.js") %>"></script>
    <script src="<%= Paths.ToClientResource(typeof (JobRepository), "site.js") %>"></script>

</asp:content>