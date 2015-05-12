(function () {
    'use strict';

    var app = angular.module('app');

    // Collect the routes
    app.constant('routes', getRoutes());

    // Configure the routes and route resolvers
    app.config(['$routeProvider', 'routes', routeConfigurator]);
    function routeConfigurator($routeProvider, routes) {

        routes.forEach(function (r) {
            $routeProvider.when(r.url, r.config);
        });
        $routeProvider.otherwise({ redirectTo: '/' });
    }

    // Define the routes 
    function getRoutes() {
        return [
            {
                url: '/viewFixes',
                config: {
                    templateUrl: 'app/fix/viewFixes.html',
                    title: 'viewFixes',
                    settings: {
                        nav: 1,
                        content: '<i class="glyphicon glyphicon-screenshot"></i> View Fixes'
                    }
                }
            },
            {
                url: '/',
                config: {
                    templateUrl: 'app/dashboard/dashboard.html',
                    title: 'dashboard',
                    settings: {
                        nav: 2,
                        content: '<i class="fa fa-home"></i> Home'
                    }
                }
            },
            {
                url: '/pageView',
                config: {
                    title: 'pageView',
                    templateUrl: 'app/PageView/pageView.html',
                    settings: {
                        nav: 2,
                        content: '<i class="fa fa-search"></i> Harmony'
                    }
                }
            },
            {
                url: '/application',
                config: {
                    title: 'application',
                    templateUrl: 'app/applications/application.html',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-folder"></i> Applications'
                    }
                }
            },
            {
                url: '/addFix',
                config: {
                    title: 'addFix',
                    templateUrl: 'app/fix/addFix.html',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-wrench"></i> Fixes'
                    }
                }
            }
            ,
            {
                url: '/audit',
                config: {
                    title: 'audit',
                    templateUrl: 'app/audit/audit.html',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-file-text-o"></i> Audit Info'
                    }
                }
            }

            ,
            {
                url: '/User',
                config: {
                    title: 'User',
                    templateUrl: 'app/User/User.html',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-user"></i> User Functions'
                    }
                }
            },
            {
                url: '/addEditDatabase',
                config: {
                    title: 'addEditDatabase',
                    templateUrl: 'app/database/addEditDatabase.html',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-database"></i> Databases'
                    }
                }
            }
        ];
    }
})();