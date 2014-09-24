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

    // 杂志章节控制器
    ezineController.controller('SectionCtrl', ['$scope', '$http', function ($scope, $http) {
        // 初始化
        $scope.sections = [
            {
                id: '1',
                placeholder: '章节名称'
            }
        ];

        // 动态生成章节名称 model
        $scope.formData = {};
        // 添加章节
        $scope.addChapter = function () {
            var sectionsNum = $scope.sections.length;
            var newSection = {
                id: sectionsNum + 1,
                placeholder: '章节名称'
            };
            $scope.sections.push(newSection);
        }

        $scope.sectionModel = {};
        //保存章节
        $scope.saveSections = function () {
            var hdEzine = angular.element('#hdEzineId').val();
            var temp = '';
            for (key in $scope.formData) {
                temp += $scope.formData[key] + ',';
            }
            var postData = { 'EzineId': hdEzine, 'Name': temp };
            var save = $http({
                method: 'POST',
                data: postData,
                url: '/Manage/Section/Save'
            }).success(function (response, status, headers, config) {
                console.info(response);
            });
        }
    }]);


    //新增文章控制器
    ezineController.controller("articleCtrl", ['$scope', '$http', function ($scope, $http) {
        var hdEzineId = angular.element('#EzineId').val();
        //获取所有的章节
        $http({
            method: 'GET',
            params: { 'id': hdEzineId },
            url: '/Manage/Section/SectionsById'
        }).success(function (response, status, headers, config) {
            $scope.sections = response;
        });

        //设置默认选择名称
        $scope.sectionName = { id: 0, Name: '请选择章节' };
        //设置选择框
        $scope.setSelect = function (section) {
            $scope.sectionName = section;
            $scope.SectionId = section.Id;
        }
    }]);

})();