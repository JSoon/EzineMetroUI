angular.module('ezineFilter', []).filter('formatterDate', function () {
    return function (input) {
        var date = new Date(parseInt(input.substr(6)));
        return format(new Date(date), 'yyyy-MM-dd');
    }
});

/**
*
* 格式化  Date(1319878800000+0800)）   时间
*/
var format = function (time, format) {
    var t = new Date(time);
    var tf = function (i) { return (i < 10 ? '0' : '') + i };
    return format.replace(/yyyy|MM|dd|HH|mm|ss/g, function (a) {
        switch (a) {
            case 'yyyy':
                return tf(t.getFullYear());
                break;
            case 'MM':
                return tf(t.getMonth() + 1);
                break;
            case 'mm':
                return tf(t.getMinutes());
                break;
            case 'dd':
                return tf(t.getDate());
                break;
            case 'HH':
                return tf(t.getHours());
                break;
            case 'ss':
                return tf(t.getSeconds());
                break;
        }
    })
}