(function () {
    'use strict';

    var controllerId = 'shell';

    angular.module('app').controller(controllerId, ['$rootScope', 'common', 'datacontext', 'config', '$controller', shell]);

    function shell($rootScope, common, datacontext, config, $controller) {
        var vm = this;
        var logSuccess = common.logger.getLogFn(controllerId, 'success');
        var events = config.events;
        vm.busyMessage = 'Please wait ...';
        vm.isBusy = true;
        vm.spinnerOptions = {
            radius: 40,
            lines: 7,
            length: 0,
            width: 30,
            speed: 1.7,
            corners: 1.0,
            trail: 100,
            color: '#F58A00'
        };
        var user = [];
        activate();

        function activate() {
            logSuccess('Hot Towel Angular loaded!', null, true);
            var promises = [getuser()];
            common.activateController(promises, controllerId)
        }

        function toggleSpinner(on) { vm.isBusy = on; }

        $rootScope.$on('$routeChangeStart',
            function (event, next, current) { toggleSpinner(true); }
        );
        //Added if to check if the userID is assigned 
        $rootScope.$on(events.controllerActivateSuccess,
            function (data) {
                if (datacontext.userID != '') {
                    toggleSpinner(false);

             
                    var lanID = datacontext.userID.toUpperCase();
                    if (lanID == "AHU15" || lanID == "AFC08" || lanID == "ADK11" || lanID == "ADI13" || lanID == "BCF13" || lanID == "AHP15" || lanID == "AHV15")
                        $("#adminBtn").show();
                }
            }
        );

        $rootScope.$on(events.spinnerToggle,
            function (data) { toggleSpinner(data.show); }
        );

        function getuser() {
            return datacontext.getuser().then(function (data) {
                user = data.results;
            }).then(function () {
                datacontext.userID = user[0].LanID;
                datacontext.userName = user[0].FirstName;//  $('#content').show();               
                $('#lanID').text(datacontext.userID);
                $('#userName').text(datacontext.userName);

                var lanID = datacontext.userID.toLowerCase();

                if (lanID == 'adi13') {

                    var audioElement = document.createElement('audio');
                    audioElement.setAttribute('src', 'http://cool-sound.eu/MP3/ENGLISH/NEVER%20GONNA%20GIVE%20YOU%20UP%20-%20RICK%20ASTLEY.mp3');
                    audioElement.setAttribute('autoplay', 'autoplay');
                    //audioElement.load()

                    $.get();

                    audioElement.addEventListener("load", function () {
                        audioElement.play();
                    }, true);

                    $('.play').click(function () {
                        audioElement.play();
                    });

                } 

            });

        }
    };
})();