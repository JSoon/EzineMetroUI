var ezineController = angular.module('ezineController', []);

ezineController.controller('ezineList', function ($scope, $http) {
    var ezine = $http({
        method: 'GET',
        url: '/Ezine/EzineList'
    });
    ezine.success(function (response, status, headers, config) {
        console.info(response)
        $scope.ezines = response;
    });

});
