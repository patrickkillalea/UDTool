(function () {
    'use strict';
    var controllerId = 'topnav';
    angular.module('app').controller(controllerId, ['common', 'datacontext', '$scope', '$location', 'routes', topnav]);

    function topnav(common, datacontext, $scope, $location, routes) {

        var vm = this;
        vm.navRoutes = [];

        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);
        var user = [];

        $(document).ready(function () {
            $("#adminBtn").hide();

            $('.slideout-menu-toggle').on('click', function (event) {
                event.preventDefault();
                // create menu variables
                var slideoutMenu = $('.slideout-menu');
                var slideoutMenuWidth = $('.slideout-menu').width();

                // toggle open class
                slideoutMenu.toggleClass("open");

                //checks if open already or not
                if (slideoutMenu.hasClass("open")) {
                    slideoutMenu.animate({
                        right: "0px"
                    });
                    $("#adminBtn").prop("disabled", true);
                }
                else {
                    slideoutMenu.animate({
                        right: -slideoutMenuWidth
                    }, 250);
                    $("#adminBtn").prop("disabled", false);
                }

                $('#content').css({ "opacity": "0.2" });
            });
        });

        $(document).mouseup(function (e) {
            var container = $("#slideOutMenu");

            if (!container.is(e.target) // if the target of the click isn't the container...
                && container.has(e.target).length == 0) // ... nor a descendant of the container
            {
                $scope.closeMenu();
            }
        });

        //used for closing the admin menu, will only work if the menu is already open
        $scope.closeMenu = function () {
            var slideoutMenu = $('.slideout-menu');
            var slideoutMenuWidth = $('.slideout-menu').width();

            if (slideoutMenu.hasClass("open")) {
                slideoutMenu.toggleClass("open");

                $("#adminBtn").prop("disabled", false);

                slideoutMenu.animate({
                    right: -slideoutMenuWidth
                }, 250);
            }

            $('#content').css({ "opacity": "1" });
        };

        $scope.home = function () {
            $(".nav li").removeClass("active");
            $location.path('/dashboard');
            $('#system').hide();
        };
        //________________________________________________________________
        $scope.reloadPage = function () {

            $('#resultsSection').hide();
            $('#search').val('');
            $('#cBtn').text('| Criteria |');
            $('#teBtn').text('Target Environment');
            log('Harmony Loaded');
        };
        //________________________________________________________________ 

        $scope.selectedOption = function (page) {
            $(".nav li").removeClass("active");
            $("#" + page).addClass("active");

        };
        
        activate();

        setTimeout(function () {
            var lanID = "ADK11";
                if (lanID == "AHU15" || lanID == "AFC08" || lanID == "ADK11" || lanID == "ADI13" || lanID == "BCF13" || lanID == "AHP15" || lanID == "AHV15") {
                    $("#adminBtn").show();
                    log('Admin Rights Given');
                }
            }, 5000) 

        
        function activate() {

            var promises = [getNavRoutes()];
            common.activateController(promises, controllerId)
                .then(function () {
                    //log('Sniper Dashboard Loaded');
                });
        }

        function getuser() {
            return datacontext.getuser().then(function (data) {
                user = data.results;
            }).then(function () {
                var lanID = "ADK11";
                $('#lanID').text(datacontext.userID);
                $('#userName').text(datacontext.userName);//  $('#content').show();                 

                //checking if admin user
                if (lanID == "AHU15" || lanID == "AFC08" || lanID == "ADK11" || lanID == "ADI13" || lanID == "BCF13" || lanID == "AHP15" || lanID == "AHV15")
                    $("#adminBtn").show();
            });
        }

        function getNavRoutes() {
            vm.navRoutes = routes.filter(function (r) {
                return r.config.settings && r.config.settings.nav;
            }).sort(function (r1, r2) {
                return r1.config.settings.nav - r2.config.settings.nav;
            });

            $(vm.navRoutes).each(function (index, route) {
                if (route.config.settings.nav == 2) {
                    var index = vm.navRoutes.indexOf(route);
                    vm.navRoutes.splice(index, 1);
                }
            });

        }

    
        $('.btn-toggle').click(function () {
            $(this).find('.btn').toggleClass('active');

            if ($(this).find('.btn-primary').size() > 0) {
                $(this).find('.btn').toggleClass('btn-primary');
            }
            if ($(this).find('.btn-danger').size() > 0) {
                $(this).find('.btn').toggleClass('btn-danger');
            }
            if ($(this).find('.btn-success').size() > 0) {
                $(this).find('.btn').toggleClass('btn-success');
            }
            if ($(this).find('.btn-info').size() > 0) {
                $(this).find('.btn').toggleClass('btn-info');
            }

            $(this).find('.btn').toggleClass('btn-default');

        });

        $scope.setConnection = function (value) {
            return datacontext.setConnection(value).then(function (data) {
                log(data.results);
            });
        }
    }

})();


