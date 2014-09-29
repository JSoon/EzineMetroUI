/**
 * SJ Ezine JavaScript Library v 0.0.1
 * jQuery v 2.1.1
 * jQuery UI v 1.11.0
 * jQuery mousewheel v 3.1.12
 * Metro UI v 2.0.31
 * AngularJS 1.3.0-beta.17
 * Animate.css
 *
 * Copyright 2014 J.Soon Personal
 * Released under the MIT license
 *
 * Update Date: 2014-09-29 16:07 pm
 */

if (window.SJ === undefined) {
    window.SJ = {}
}

SJ.ezine = {
    /**
     * 初始化
     */
    init: function () {
        // this.freshDisabled();
        this.scrollHorizOn();
        // this.ajaxLoad( '#sjTileArea' );
    },
    /**
     * ajax loading tips
     */
    ajaxLoadingTips: function (tSwitch) {
        if (tSwitch === 'show') {
            $('#sjLoading').remove(); // Make sure there is no #sjLoading existed in the DOM at the beginning
            var sjLoading = $('<div id="sjLoading"><span>数据加载中，请稍后 ...</span></div>');
            $('body').append(sjLoading);
            var txtHeight = $('#sjLoading > span').outerHeight();
            var top = $(window).height() / 2 - txtHeight / 2;
            $('#sjLoading > span').css('margin-top', top);
            $('#sjLoading').show();
            $('.hint').remove(); // 修复页面切换时 .hint 的不消失 bug
        } else if (tSwitch === 'hide') {
            $('#sjLoading').delay(500).fadeOut(function () {
                $(this).remove();
            });
        }
    },
    /**
     * ajax 加载页面
     */
    ajaxLoad: function (vessel) {
        $(vessel).on('click', 'a[data-url]', function () {
            $(vessel).removeClass('animated bounceInRight bounceInLeft');
            var url = $(this).attr('data-url'),
                dir = $(this).attr('data-direction');
            if (url) {
                $.ajax({
                    url: url,
                    dataType: 'html'
                }).done(function (data, textStatus, jqXHR) {
                    // console.log( $( '.dhint' ) );
                    $('.hint').remove(); // 修复页面切换时 .hint 的不消失 bug
                    if (dir === 'rtl') { // 判断页面进入方向，赋予相应的 CSS3 动画
                        $(vessel).html(data).addClass('animated bounceInLeft');
                    } else {
                        $(vessel).html(data).addClass('animated bounceInRight');
                    }
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    // console.log( errorThrown );
                });
            } else {
                // console.log( 'no url' );
            }
        });
    },
    /**
     * 禁止 F5 和 Ctrl+R 刷新
     */
    freshDisabled: function () {
        $(document).bind('keydown keyup', function (e) {
            if (e.which === 116) { // F5
                alert('禁止快捷键刷新页面');
                return false;
            }
            if (e.which === 82 && e.ctrlKey) { // Ctrl + R
                alert('禁止快捷键刷新页面');
                return false;
            }
        });
    },
    /**
     * 生成随机数
     */
    randomNum: function (scope) {
        var scope = scope || 100000; // 默认随机数范围 0 到 100,000
        return Math.floor(Math.random() * scope);
    },
    /**
     * Determine ie browser version from script @ http:*support.microsoft.com/kb/167820
     */
    msie: function () {
        var ua = window.navigator.userAgent;
        var msie = ua.indexOf("MSIE ");
        if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./)) { // If Internet Explorer, return version number
            // alert(parseInt(ua.substring(msie + 5, ua.indexOf(".", msie))));
            return true;
        } else { // If another browser, return 0
            // alert('otherbrowser');
            return false;
        }
    },
    /**
     * 给鼠标滚轮绑定横向滑动事件
     */
    scrollHorizOn: function (speed) {
        var that = this;
        $('body').on('mousewheel', function (event, delta) {
            var s = speed || 30;
            var ifIE = that.msie();
            if (ifIE === true) {
                document.documentElement.scrollLeft -= ( delta * s );
            } else {
                document.body.scrollLeft -= ( delta * s );
            }
            event.preventDefault();
        });
    },
    scrollHorizOff: function (speed) {
        var that = this;
        console.log('off mouseweel');
        $('body').off('mousewheel');
    }
}
SJ.ezine.init();

/**
 * metroEzine module
 *
 * description
 */
var ezineApp = angular.module('metroEzine', [
    'ngRoute',
    'metroEzineServices',
    'metroEzineHomePage',
    'metroEzineContentsPage',
    'metroEzineArticlePage'
]);
/**
 * ezineApp routes
 */
ezineApp.config([ '$routeProvider',
    function ($routeProvider) {
        $routeProvider.when('/ezines', {
            templateUrl: 'home.html',
            controller: 'homeController'
        }).when('/ezines/:ezineId', {
            templateUrl: 'contents.html',
            controller: 'contentsController'
        }).when('/ezines/:ezineId/:articleId', {
            templateUrl: 'tmpl-1.html',
            controller: 'articleController'
        }).otherwise({
            redirectTo: '/ezines'
        });
    }
]);
/**
 * ezineApp directive
 * set the page animate direction
 */
ezineApp.directive('a', [ 'pageDirection',
    function (pageDirection) {
        return {
            restrict: 'E',
            link: function ($scope, iElm, iAttrs, controller) {
                if (iAttrs.direction) {
                    iElm.on('click', function (e) {
                        // e.preventDefault();
                        pageDirection.set(iAttrs.direction);
                    });
                }
            }
        };
    }
]);

/**
 * metroEzineServices module
 *
 * shared services here
 */
var ezineServices = angular.module('metroEzineServices', []);
ezineServices.config([ '$httpProvider',
    function ($httpProvider) {
        // register the interceptor as a service, intercepts ALL angular ajax http calls
        $httpProvider.interceptors.push('httpInterceptor');
    }
]);
ezineServices.run([ '$http',
    function ($http) {
        // set ajax http cache to false globally
        $http.defaults.cache = false;
    }
]);
ezineServices.factory('httpInterceptor', [ '$q', '$document',
    function ($q, $document) {
        var dir = '';
        return {
            'request': function (config) {
                // show the loading layer
                SJ.ezine.ajaxLoadingTips('show');
                return config;
            },
            'requestError': function (rejection) {
                // do something on error
                if (canRecover(rejection)) {
                    return responseOrNewPromise;
                }
                return $q.reject(rejection);
            },
            'response': function (response) {
                // hide the loading layer
                SJ.ezine.ajaxLoadingTips('hide');
                return response;
            },
            'responseError': function (rejection) {
                // do something on error
                if (canRecover(rejection)) {
                    return responseOrNewPromise;
                }
                return $q.reject(rejection);
            }
        };
    }
]);
/**
 * page load direction animation
 */
ezineServices.factory('pageDirection', function () {
    var dir = 'rtl'; // init dir
    return {
        set: function (direction) {
            dir = direction;
        },
        get: function () {
            return dir;
        }
    };
});

/**
 * metroEzineHomePage module
 *
 * description
 */
var ezineHome = angular.module('metroEzineHomePage', []);
/**
 * home page controller
 */
ezineHome.controller('homeController', [ '$scope', '$http', 'pageDirection',
    function ($scope, $http, pageDirection) {
        // add animate to the #sjTileArea after page loaded
        var direction = pageDirection.get();
        // add horizontal scroll event
        SJ.ezine.scrollHorizOn();
        if (direction === 'ltr') {
            angular.element('#sjTileArea').addClass('animated bounceInLeft');
        } else if (direction === 'rtl') {
            angular.element('#sjTileArea').addClass('animated bounceInRight');
        }
        // get ezine-list
        $http({
            method: 'GET',
            url: '../js/ezine-list.json',
            params: {
                '_': Math.random()
            }
            // cache: false // not work, don't know why
        }).then(function (success) {
            $scope.ezines = success.data;
        }, function (fail) {

        });
    }
]);

/**
 * metroEzineContentsPage module
 *
 * description
 */
var ezineContents = angular.module('metroEzineContentsPage', []);
/**
 * contents page controller
 */
ezineContents.controller('contentsController', [ '$scope', '$routeParams', '$http', 'pageDirection',
    function ($scope, $routeParams, $http, pageDirection) {
        // add animate to the #sjTileArea after page loaded
        var direction = pageDirection.get();
        // add horizontal scroll event
        SJ.ezine.scrollHorizOn();
        if (direction === 'ltr') {
            angular.element('#sjTileArea').addClass('animated bounceInLeft');
        } else if (direction === 'rtl') {
            angular.element('#sjTileArea').addClass('animated bounceInRight');
        }
        $scope.ezineId = $routeParams.ezineId;
        // get ezine-contents
        $http({
            method: 'GET',
            url: '../js/ezine-contents.json',
            params: {
                'id': $scope.ezineId,
                '_': Math.random()
            }
        }).then(function (success) {
            $scope.articles = success.data;
            // chapter filter
            var indexedChapters = [];
            $scope.articlesToFilter = function () {
                // this will reset the list of indexed chapters each time when the list is looped again
                indexedChapters = [];
                return $scope.articles;
            }
            $scope.filterChapters = function (article) {
                var chapterIsNew = ( indexedChapters.indexOf(article.chapter) == -1 );
                if (chapterIsNew) {
                    indexedChapters.push(article.chapter);
                }
                return chapterIsNew;
            }
        }, function (fail) {

        });
    }
]);
/**
 * contents page filter
 */
ezineContents.filter('articleSrc', function () {
    return function (articleSrc) {
        if (articleSrc === true) {
            articleSrc = '原';
        } else {
            articleSrc = '转';
        }
        return articleSrc;
    };
});
/**
 * contents page directive
 */
ezineContents.directive('tileAreaDirective',
    function () {
        // Runs during compile
        return {
            // name: '',
            // priority: 1,
            // terminal: true,
            // scope: {}, // {} = isolate, true = child, false/undefined = no change
            // controller: function($scope, $element, $attrs, $transclude) {},
            // require: 'ngModel', // Array = multiple requires, ? = optional, ^ = check parent elements
            restrict: 'A', // E = Element, A = Attribute, C = Class, M = Comment
            // template: '',
            // templateUrl: '',
            // replace: true,
            // transclude: true,
            // compile: function(tElement, tAttrs, function transclude(function(scope, cloneLinkingFn){ return function linking(scope, elm, attrs){}})),
            link: function ($scope, iElm, iAttrs, controller) {
                // if the outer loop is the last loop AND the inner loop is ended, then import metro.min.js to make metro hint works for sure
                if (!$scope.$parent.$$nextSibling && $scope.$last) {
                    angular.element('#sjTileArea').after('<script src="../lib/metro-ui/2.0.31/min/metro.min.js"></script>');
                }
            }
        };
    }
);

/**
 * metroEzineArticlePage module
 *
 * description
 */
var ezineArticle = angular.module('metroEzineArticlePage', []);
/**
 * article page controller
 */
ezineArticle.controller('articleController', [ '$scope', '$routeParams', 'pageDirection',
    function ($scope, $routeParams, pageDirection) {
        // add animate to the #sjTileArea after page loaded
        var direction = pageDirection.get();
        // remove horizontal scroll event
        SJ.ezine.scrollHorizOff();
        if (direction === 'ltr') {
            angular.element('#sjTileArea').addClass('animated bounceInLeft');
        } else if (direction === 'rtl') {
            angular.element('#sjTileArea').addClass('animated bounceInRight');
        }
        $scope.ezineId = $routeParams.ezineId; // get ezine id in the route
    }
]);