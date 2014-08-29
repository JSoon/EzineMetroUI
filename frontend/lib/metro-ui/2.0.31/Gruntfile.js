module.exports = function( grunt ) {

    // 项目配置
    grunt.initConfig( {
        pkg: grunt.file.readJSON( 'package.json' ),
        // 将要被合并的文件列表
        concat: {
            options: {
                stripBanners: true,
                banner: '/*! <%= pkg.name %> - v<%= pkg.version %> - ' +
                    '<%= grunt.template.today("yyyy-mm-dd") %> */\n'
            },
            dist: {
                src: [ 'js/*.js' ],
                dest: 'dist/metro-ui.js'
            }
        }
    } );

    // 加载提供"concat"任务的插件
    grunt.loadNpmTasks( 'grunt-contrib-concat' );
    // 默认任务
    grunt.registerTask( 'default', [ 'concat' ] );

}