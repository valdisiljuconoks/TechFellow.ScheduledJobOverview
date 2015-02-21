;
(function() {
    angular.module('schApp', [])
        .directive('toggleBool', function() {
            return {
                restrict: 'E',
                link: function(scope, element, attr) {
                    attr.$observe('targetProp', function() {
                        if (attr.targetProp == "true") {
                            element.html('Yes');
                        } else {
                            if (attr.targetProp != "") {
                                element.html('No');
                            }
                        }
                    });
                }
            };
        })
        .controller('scheduledJobsController', [
            '$scope', '$http', '$window', '$timeout', 'serviceUrl', 'detailsUrl', function($scope, $http, $window, $timeout, serviceUrl, detailsUrl) {
                var self = this;
                $scope.self = self;
                self.autoRefresh = true;
                self.filter = null;

                self.fetch = function() {
                    if (self.autoRefresh) {
                        $http.get(serviceUrl + 'GetList').success(function(data) {
                            try {
                                self.jobs = angular.fromJson(data);
                            } catch (e) {
                                // error may occur when service returns html for login page instead of json (unauthorized access, session expired, etc)
                            }
                        });
                        $timeout(function() { self.fetch(); }, 5000);
                    }
                };

                self.showDetails = function(id) {
                    $window.location.href = detailsUrl + '?pluginId=' + id;
                };

                self.executeJob = function(id) {
                    $window.location.href = serviceUrl + '/Execute/?jobId=' + id;
                };

                self.stopJob = function (id) {
                    $window.location.href = serviceUrl + '/Stop/?jobId=' + id;
                };

                $scope.$watch('self.autoRefresh', function(newValue) {
                    if (newValue) {
                        self.fetch();
                    }
                });

                $scope.$watch('self.filter', function(newValue) {
                    
                });
            }
        ])
        .value('serviceUrl', $('#sch-app-root').attr('data-service-url'))
        .value('detailsUrl', $('#sch-app-root').attr('data-details-url'));
})();