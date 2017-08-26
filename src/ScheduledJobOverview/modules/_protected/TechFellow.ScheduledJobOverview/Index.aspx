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

        .showArrow {
            position: relative;
            padding-right: 12px;
            display: inline-block;
        }

        .showArrow .arrow {
            border: solid black;
            border-width: 0 2px 2px 0;
            position: absolute;
            padding: 2px;
            right: 2px;
            top: 4px;
        }

        .up {
            transform: rotate(-135deg);
            -webkit-transform: rotate(-135deg);
        }

        .down {
            transform: rotate(45deg);
            -webkit-transform: rotate(45deg);
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
                    <th class="epitableheading" scope="col">
                      <a href="#" ng-click="vm.setSort('isRunning')" ng-class="{'showArrow':vm.isSortType('isRunning')}">
                        Running
                        <span ng-class="{'arrow up': vm.sortReverse, 'arrow down': !vm.sortReverse}"></span>
                      </a>
                    </th>
                    <th class="epitableheading" scope="col">
                      <a href="#" ng-click="vm.setSort('name')" ng-class="{'showArrow':vm.isSortType('name')}">
                    	Name
                    	<span ng-class="{'arrow up': vm.sortReverse, 'arrow down': !vm.sortReverse}"></span>
                      </a>
                    </th>
                    <th class="epitableheading" scope="col">
                      <a href="#" ng-click="vm.setSort('isEnabled')" ng-class="{'showArrow':vm.isSortType('isEnabled')}">
                        Enabled
                        <span ng-class="{'arrow up': vm.sortReverse, 'arrow down': !vm.sortReverse}"></span>
                      </a>
                    </th>
                    <th class="epitableheading" scope="col">
                      <a href="#" ng-click="vm.setSort('interval')" ng-class="{'showArrow':vm.isSortType('interval')}">
                        Interval
                        <span ng-class="{'arrow up': vm.sortReverse, 'arrow down': !vm.sortReverse}"></span>
                      </a>
                    </th>
                    <th class="epitableheading" scope="col">
                      <a href="#" ng-click="vm.setSort('isLastExecuteSuccessful')" ng-class="{'showArrow':vm.isSortType('isLastExecuteSuccessful')}">
                        Successful
                        <span ng-class="{'arrow up': vm.sortReverse, 'arrow down': !vm.sortReverse}"></span>
                      </a>
                    </th>
                    <th class="epitableheading" scope="col">
                      <a href="#" ng-click="vm.setSort('isRestartable')" ng-class="{'showArrow':vm.isSortType('isRestartable')}">
                        Restartable
                        <span ng-class="{'arrow up': vm.sortReverse, 'arrow down': !vm.sortReverse}"></span>
                      </a>
                    </th>
                    <th class="epitableheading" scope="col">
                      <a href="#" ng-click="vm.setSort('lastExecuteDisplay')" ng-class="{'showArrow':vm.isSortType('lastExecuteDisplay')}">
                        Last execute date
                        <span ng-class="{'arrow up': vm.sortReverse, 'arrow down': !vm.sortReverse}"></span>
                      </a>
                    </th>
                    <th class="epitableheading" scope="col">
                      <a href="#" ng-click="vm.setSort('lastDuration')" ng-class="{'showArrow':vm.isSortType('lastDuration')}">
                        Duration
                        <span ng-class="{'arrow up': vm.sortReverse, 'arrow down': !vm.sortReverse}"></span>
                      </a>
                    </th>
                    <th class="epitableheading" scope="col">
                      <a href="#" ng-click="vm.setSort('typeName')" ng-class="{'showArrow':vm.isSortType('typeName')}">
                        Type Name
                        <span ng-class="{'arrow up': vm.sortReverse, 'arrow down': !vm.sortReverse}"></span>
                      </a>
                    </th>
                    <th class="epitableheading" scope="col">Description</th>
                    <th class="epitableheading" scope="col">Actions</th>
                    </thead>
                    <tbody>
                    <tr ng-repeat="job in vm.jobs | filter:vm.filter | orderBy:vm.sortType" ng-class="{'job-inactive': !job.isEnabled, 'job-deleted': !job.exists}">
                        <td style="text-align: center">
                            <img src="<%= Paths.ToClientResource(typeof (JobRepository), "spinner.gif") %>" alt="{{job.isRunning}}" title="{{job.lastMessage}}" ng-show="job.isRunning"/>
                        </td>
                        <td style="white-space: nowrap">{{job.name}}</td>
                        <td><toggle-bool target-prop="{{job.isEnabled}}"/></td>
                        <td style="white-space: nowrap">{{job.interval}}</td>
                        <td class="red-bold-{{job.isLastExecuteSuccessful}}">
                            <toggle-bool target-prop="{{job.isLastExecuteSuccessful}}"/>
                        </td>
                        <td><toggle-bool target-prop="{{job.isRestartable}}"/></td>
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