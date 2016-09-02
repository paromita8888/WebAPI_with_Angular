(function () {
    'use strict';

    angular.module('sampleAngularApp')
    .controller('navBarController', ['$scope','$http','$location', function ($scope,$http,$location) {
        $scope.menuOpen = false;

        //code to show the user name or backto map link
        $scope.isMapView = false;

        var appurl = $location.path();
        if (appurl != '/') {
            $scope.isMapView = false;
        }
        else {
            $scope.isMapView = true;
        }

        //code to get the user name from API
        $http.get("/api/users").then(function (response) {
            $scope.userName = response.data;

        });

        $scope.$on('$routeChangeSuccess', function (e, current, previous) {
            if (current.templateUrl != 'UI/views/main.html') {
                $scope.isMapView = false;
            }
            else {
                $scope.isMapView = true;
            }

        });

    }]);


})();