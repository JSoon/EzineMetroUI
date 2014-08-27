/**
 * SJ Ezine JavaScript Library v 0.0.1
 * jQuery v 2.1.1
 * jQuery UI v 1.11.0
 * jQuery mousewheel v 3.1.12
 * Metro UI v 2.0.31
 * Animate.css
 *
 * Copyright 2014 J.Soon Personal
 * Released under the MIT license
 *
 * Date: 2014-07-18 18:03 pm
 */

if ( window.SJ === undefined ) {
    window.SJ = {};
}

SJ.ezine = {
    /**
     * 初始化
     */
    init: function() {
        // this.freshDisabled();
        this.scrollHoriz();
        this.ajaxLoad( '#sjTileArea' );
    },
    /**
     * ajax loading tips
     */
    ajaxLoadingTips: function( tSwitch ) {
        if ( tSwitch === 'show' ) {
            var sjLoading = $( '<div id="sjLoading"><span>数据加载中，请稍后 ...</span></div>' );
            $( 'body' ).append( sjLoading );
            var txtHeight = $( '#sjLoading > span' ).outerHeight();
            var top = $( window ).height() / 2 - txtHeight / 2; // 这里必须事先确定图片高度
            $( '#sjLoading > span' ).css( 'margin-top', top );
            $( '#sjLoading' ).show();
        } else if ( tSwitch === 'hide' ) {
            $( '#sjLoading' ).delay( 500 ).fadeOut( function() {
                $( this ).remove();
            } );
        }
    },
    /**
     * ajax 加载页面
     */
    ajaxLoad: function( vessel ) {
        $( vessel ).on( 'click', 'a[data-url]', function() {
            $( vessel ).removeClass( 'animated bounceInRight bounceInLeft' );
            var url = $( this ).attr( 'data-url' ),
                dir = $( this ).attr( 'data-direction' );
            if ( url ) {
                $.ajax( {
                    url: url,
                    dataType: 'html'
                } ).done( function( data, textStatus, jqXHR ) {
                    // console.log( $( '.dhint' ) );
                    $( '.hint' ).remove(); // 修复页面切换时 .hint 的不消失 bug
                    if ( dir === 'rtl' ) { // 判断页面进入方向，赋予相应的 CSS3 动画
                        $( vessel ).html( data ).addClass( 'animated bounceInLeft' );
                    } else {
                        $( vessel ).html( data ).addClass( 'animated bounceInRight' );
                    }
                } ).fail( function( jqXHR, textStatus, errorThrown ) {
                    // console.log( errorThrown );
                } );
            } else {
                // console.log( 'no url' );
            }
        } );
    },
    /**
     * 禁止 F5 和 Ctrl+R 刷新
     */
    freshDisabled: function() {
        $( document ).bind( 'keydown keyup', function( e ) {
            if ( e.which === 116 ) { // F5
                alert( '禁止快捷键刷新页面' );
                return false;
            }
            if ( e.which === 82 && e.ctrlKey ) { // Ctrl + R
                alert( '禁止快捷键刷新页面' );
                return false;
            }
        } );
    },
    /**
     * 生成随机数
     */
    randomNum: function( scope ) {
        var scope = scope || 100000; // 默认随机数范围 0 到 100,000
        return Math.floor( Math.random() * scope );
    },
    /**
     * Determine ie browser version from script @ http:*support.microsoft.com/kb/167820
     */
    msie: function() {
        var ua = window.navigator.userAgent;
        var msie = ua.indexOf( "MSIE " );
        if ( msie > 0 || !!navigator.userAgent.match( /Trident.*rv\:11\./ ) ) { // If Internet Explorer, return version number
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
    scrollHoriz: function( speed ) {
        var that = this;
        $( 'body' ).mousewheel( function( event, delta ) {
            var s = speed || 30;
            var ifIE = that.msie();
            if ( ifIE === true ) {
                document.documentElement.scrollLeft -= ( delta * s );
            } else {
                document.body.scrollLeft -= ( delta * s );
            }
            event.preventDefault();
        } );
    }
}
SJ.ezine.init();

/**
 * metroEzine Module
 *
 * Description
 */
var ezineApp = angular.module( 'metroEzine', [
    'ngRoute',
    'metroEzineServices',
    'metroEzineControllers'
] );

/**
 * metroEzine Routes
 */
ezineApp.config( [ '$routeProvider',
    function( $routeProvider ) {
        $routeProvider.when( '/ezines', {
            templateUrl: 'home.html',
            controller: 'indexController'
        } ).when( '/ezines/:ezineId', {
            templateUrl: 'contents.html',
            controller: 'contentsController'
        } ).otherwise( {
            redirectTo: '/ezines'
        } );
    }
] );

/**
 * metroEzineServices Module
 *
 * Description
 */
var ezineServices = angular.module( 'metroEzineServices', [] );

ezineServices.config( [ '$httpProvider',
    function( $httpProvider ) {
        // Register the interceptor as a service, intercepts ALL angular ajax http calls
        $httpProvider.interceptors.push( 'httpInterceptor' );
    }
] );
ezineServices.run( [ '$http',
    function( $http ) {
        // Set ajax http cache to false globally
        $http.defaults.cache = false;
    }
] );
ezineServices.factory( 'httpInterceptor', [ '$q',
    function( $q ) {
        return {
            'request': function( config ) {
                // do something on success
                SJ.ezine.ajaxLoadingTips( 'show' );
                return config;
            },
            'requestError': function( rejection ) {
                // do something on error
                if ( canRecover( rejection ) ) {
                    return responseOrNewPromise;
                }
                return $q.reject( rejection );
            },
            'response': function( response ) {
                // do something on success
                SJ.ezine.ajaxLoadingTips( 'hide' );
                return response;
            },
            'responseError': function( rejection ) {
                // do something on error
                if ( canRecover( rejection ) ) {
                    return responseOrNewPromise;
                }
                return $q.reject( rejection );
            }
        }
    }
] );

/**
 * metroEzineControllers Module
 *
 * Description
 */
var ezineControllers = angular.module( 'metroEzineControllers', [] );
/**
 * Index page controller
 */
ezineControllers.controller( 'indexController', [ '$scope', '$http',
    function( $scope, $http ) {
        // Add animate to the #sjTileArea after index page loaded
        angular.element( '#sjTileArea' ).addClass( 'animated bounceInLeft' );
        // Get ezine-list
        $http( {
            method: 'GET',
            url: '/js/ezine-list.json',
            params: {
                '_': Math.random()
            }
            // cache: false // not work, don't know why
        } ).then( function( success ) {
            $scope.ezines = success.data;
        }, function( fail ) {

        } );
    }
] );
/**
 * Contents page controller
 */
ezineControllers.controller( 'contentsController', [ '$scope', '$routeParams',
    function( $scope, $routeParams ) {
        angular.element( '#sjTileArea' ).addClass( 'animated bounceInRight' );
        var ezineId = $routeParams.ezineId;
        
    }
] );