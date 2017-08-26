;(function() {
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
        .constant('serviceUrl', $('#sch-app-root').attr('data-service-url'))
        .constant('detailsUrl', $('#sch-app-root').attr('data-details-url'))
        .constant('antiForgeryToken', $('#sch-app-root').attr('data-anti-forgery'))

        .controller('scheduledJobsController', ['$scope', '$http', '$window', '$timeout', 'serviceUrl', 'detailsUrl', 'antiForgeryToken', function ($scope, $http, $window, $timeout, serviceUrl, detailsUrl, antiForgeryToken) {

            var vm = this;

            vm.autoRefresh = true;

            vm.fetch = fetch;
            vm.showDetails = showDetails;
            vm.executeJob = executeJob;
            vm.stopJob = stopJob;
            vm.deleteJob = deleteJob;
            vm.showStats = showStats;
            vm.setSort = setSort;
            vm.isSortType = isSortType;
            vm.sortType = ['-exists','+name']; // set the default sort type
            vm.currentSortId = 'name'; // set the default sort type
            vm.sortReverse = false;  // set the default sort order

            $http.defaults.headers.common['X-EpiRestAntiForgeryToken'] = antiForgeryToken;

            function fetch() {
                if (vm.autoRefresh) {
                    $http.get(serviceUrl + 'get',
                    {
                        transformResponse: [function(value) {
                            return value.slice(4);
                        }]
                    }).success(function(data) {
                        try {
                            vm.jobs = angular.fromJson(data);
                        } catch (e) {
                            // error may occur when service returns html for login page instead of json (unauthorized access, session expired, etc)
                        }
                    });
                    $timeout(function() { vm.fetch(); }, 5000);
                }
            };

            function showDetails(id) {
                $window.location.href = detailsUrl + '?pluginId=' + id;
            };

            function executeJob(id) {
                $http.post(serviceUrl + '/' + id);
                vm.fetch();
            };

            function stopJob(id) {
                $http.put(serviceUrl + '/' + id);
                vm.fetch();
            };

            function deleteJob(id) {
                $http.delete(serviceUrl + '/' + id);
                vm.fetch();
            };

            function showStats(id) {
                $window.location.href = 'charts.aspx?pluginId=' + id;
            }

            function setSort(sortId) {
                if (vm.currentSortId == sortId) {
                    vm.sortReverse = !vm.sortReverse;
                    vm.sortType = ['-exists', (vm.sortReverse ? '-' : '+') + vm.currentSortId]
                } else {
                    vm.currentSortId = sortId;
                    vm.sortReverse = false;
                    vm.sortType = ['-exists', '+' + vm.currentSortId]
                }
            }

            function isSortType(sortId) {
                return (vm.currentSortId == sortId);
            }

            $scope.$watch('vm.autoRefresh', function(newValue) {
                if (newValue) {
                    vm.fetch();
                }
            });
        }]);
})();