var ezineController = angular.module('ezineController', []);

//杂志封面
ezineController.controller('ezineList', function ($scope, $http) {
    var ezine = $http({
        method: 'GET',
        url: '/Ezine/EzineList'
    });
    ezine.success(function (response, status, headers, config) {
        $scope.ezines = response;
    });

    //编辑

    //删除
    $scope.delEzine = function (obj) {
        console.info(obj.EzineId);
        var del = $http({
            method: 'GET',
            url: '/Ezine/Delete',
            params: { 'id': obj.EzineId }
        });
        del.success(function (response, status, headers, config) {
            $scope.result = response;
        });
    }

});
