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
        .controller('scheduledJobsController', function($scope, $http, $window, $timeout, serviceUrl, detailsUrl) {
            $scope.fetch = function() {
                if ($scope.autoRefresh) {
                    $http.get(serviceUrl + 'GetList').success(function(data) { $scope.jobs = data; });
                    $timeout(function() { $scope.fetch(); }, 5000);
                }
            };

            $scope.showDetails = function(id) {
                $window.location.href = detailsUrl + '?pluginId=' + id;
            };

            $scope.$watch('autoRefresh', function(newValue) {
                if (newValue) {
                    $scope.fetch();
                }
            });

            $scope.autoRefresh = true;

        })
        .value('serviceUrl', $('#sch-app-root').attr('data-service-url'))
        .value('detailsUrl', $('#sch-app-root').attr('data-details-url'));
})();