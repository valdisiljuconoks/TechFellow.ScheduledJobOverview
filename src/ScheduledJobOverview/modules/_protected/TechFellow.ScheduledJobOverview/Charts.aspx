<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="true" CodeBehind="Chart.aspx.cs" Inherits="TechFellow.ScheduledJobOverview.modules._protected.TechFellow.ScheduledJobOverview.Chart" %>
<%@ Import Namespace="EPiServer" %>
<%@ Import Namespace="EPiServer.Shell" %>
<%@ Import Namespace="TechFellow.ScheduledJobOverview" %>
<%@ Import Namespace="EPiServer.Framework.Web" %>

<asp:content id="Content2" contentplaceholderid="FullRegion" runat="server">
    
    <style>
        body { padding: 25px; }
    </style>

    <div class="epi-contentArea">
        <h1 class="EP-prefix"><%= jobName %> (last 10 cycles)</h1>
    </div>
    
    <div class="epi-padding" style="display: block;">
        <h2 class="EP-prefix">Execution Duration (ms)</h2>
        <canvas id="myChart" width="1000" height="600"></canvas>
        <div style="float: right;">
            <span class="epi-cmsButton">
                <input type="button" value="Show details" alt="Details" title="Details" class="epi-cmsButton-tools epi-cmsButton-text epi-cmsButton-ViewMode" onclick="showDetails();">
            </span>
            <span class="epi-cmsButton">
                <input type="button" value="Scheduled job overview" class="epi-cmsButton-tools epi-cmsButton-text epi-cmsButton-File" onclick="showOverview();">
            </span>
        </div>
    </div>

    <script src="<%= Paths.ToClientResource(typeof (JobRepository), "chart.min.js") %>"></script>
    
    <script>
        var ctx = document.getElementById("myChart");
        var data = {
            labels: [<%= string.Join(", ", times) %>],
            datasets: [
                {
                    fill: false,
                    lineTension: 0.1,
                    backgroundColor: "rgba(75,192,192,0.4)",
                    borderColor: "rgba(75,192,192,1)",
                    borderCapStyle: 'butt',
                    borderDash: [],
                    borderDashOffset: 0.0,
                    borderJoinStyle: 'miter',
                    pointBorderColor: "rgba(75,192,192,1)",
                    pointBackgroundColor: "#fff",
                    pointBorderWidth: 1,
                    pointHoverRadius: 5,
                    pointHoverBackgroundColor: "rgba(75,192,192,1)",
                    pointHoverBorderColor: "rgba(220,220,220,1)",
                    pointHoverBorderWidth: 2,
                    pointRadius: 1,
                    pointHitRadius: 10,
                    data: [<%= string.Join(", ", durations.Select(d => d.TotalMilliseconds.ToString())) %>],
                    spanGaps: false
                }
            ]
        };

        var myChart = new Chart(ctx, {
            type: 'line',
            data: data,
            options: {
                maintainAspectRatio: false,
                legend: {
                    display: false
                }
            }
        });


        function showDetails() {
            window.location.href = '<%= UriSupport.ResolveUrlFromUIBySettings("Admin/DatabaseJob.aspx") %>?pluginId=<%= jobId %>';
        }

        function showOverview() {
            window.location.href = '<%= Paths.ToResource(typeof(DatabaseJobAdapter), "Index.aspx") %>';
        }
    </script>

</asp:content>