///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// SJ Ezine JavaScript Library v 0.0.1
// jQuery v 2.1.1
// jQuery UI v 1.11.0
// jQuery mousewheel v 3.1.12
// Metro UI v 2.0.31
// Animate.css
//
// Copyright 2014 J.Soon Personal
// Released under the MIT license
//
// Date: 2014-07-18 18:03 pm
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

if ( window.SJ === undefined ) {
    window.SJ = {};
}

SJ.ezine = {
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // 初始化
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    init: function() {
        // this.freshDisabled();
        this.scrollHoriz();
        this.ajaxLoad( '#sjTileArea' );
    },
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // ajax 加载页面
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // 禁止 F5 和 Ctrl+R 刷新
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // 生成随机数
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    randomNum: function( scope ) {
        var scope = scope || 100000; // 默认随机数范围 0 到 100,000
        return Math.floor( Math.random() * scope );
    },
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Determine ie browser version from script @ http://support.microsoft.com/kb/167820
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // 给鼠标滚轮绑定横向滑动事件
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// jquery ajax 全局配置
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
$.ajaxSetup( {
    global: true, // 全局配置是否生效
    aysnc: true, // 是否异步
    cache: false, // 是否缓存
    timeout: 10 // 请求超时时限
} );
$( document ).ajaxStart( function() {
    var src = '../img/loading.gif?' + SJ.ezine.randomNum();
    // add loading mask
    var $sjLoading = $( '<div id="sjLoading"><img src="' + src + '" alt="loading..." /></div>' );
    $( 'body' ).append( $sjLoading );
    var h = $( window ).height() / 2 - 256 / 2; // 这里要求事先确定图片高度
    $( '#sjLoading > img' ).css( 'margin-top', h );
    $( '#sjLoading' ).show();
    // console.log('ajaxStart');
} ).ajaxComplete( function( event, xhr, settings ) {
    // console.log('ajaxComplete');
} ).ajaxStop( function() {
    $( '#sjLoading' ).delay( 1000 ).fadeOut( function() {
        $( this ).remove();
    } );
    // console.log('ajaxStop');
} ).ajaxError( function( event, jqXHR, ajaxSettings, errorThrown ) {
    var textStatus = jqXHR.statusText;
    if ( textStatus == 'notmodified' ) {
        alert( 'notmodified' );
    } else if ( textStatus == 'error' ) {
        alert( 'error' );
    } else if ( textStatus == 'timeout' ) {
        alert( 'timeout' );
    } else if ( textStatus == 'abort' ) {
        alert( 'abort' );
    } else if ( textStatus == 'parsererror' ) {
        alert( 'parsererror' );
    }
} );