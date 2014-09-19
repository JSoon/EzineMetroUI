(function () {

    var ezineController = angular.module('ezineController', []);

    //杂志封面
    ezineController.controller('ezineList', function ($scope, $http) {
        GetEzineData($scope, $http);
        //删除
        $scope.delEzine = function (obj) {
            var del = $http({
                method: 'GET',
                url: '/Ezine/Delete',
                params: { 'id': obj.EzineId }
            });
            del.success(function (response, status, headers, config) {
                $scope.result = response;
                if (response === 'true') {
                    GetEzineData($scope, $http);
                }
            });
        }
    });

    //获取所欲数据
    function GetEzineData($scope, $http) {
        var ezine = $http({
            method: 'GET',
            url: '/Ezine/EzineList'
        });
        ezine.success(function (response, status, headers, config) {
            $scope.ezines = response;
        });
    }

    // 杂志章节
    sectionController.inject = ['$scope', '$http']; // 依赖注入
    ezineController.controller('SectionCtrl', sectionController);

    function sectionController($scope, $http) {
        // 初始化
        $scope.sections = [
            {
                id: '1',
                placeholder: '章节名称'
            }
        ];

        // 添加章节
        $scope.addChapter = function () {
            var sectionsNum = $scope.sections.length;
            var newSection = {
                id: sectionsNum + 1,
                placeholder: '章节名称'
            };
            $scope.sections.push(newSection);
        }

        //$scope.$watch('');

        //保存章节
        $scope.saveSections = function () {
            var save = $http({
                method: 'POST',
                url: '/Manage/Section/Save'
            }).success(function (response, status, headers, config) {
                console.info(response);
            });
        }
    }

})();