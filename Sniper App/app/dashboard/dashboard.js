(function () {
    'use strict';
    var controllerId = 'dashboard';
    angular.module('app').controller(controllerId, ['common', 'datacontext', '$scope', '$location', dashboard]);

    function dashboard(common, datacontext, $scope, $location) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;       
        vm.modules = [];

        $scope.$watch(datacontext.userID, activate());
            
         //   activate();

        function activate() {
            var promises = [getModules()];
            common.activateController(promises, controllerId)
                .then(function () { log('Sniper Dashboard Loaded'); });
        }

        function getModules() {
            return datacontext.getModules().then(function (data) {
                return vm.modules = data
            });
        }

        $scope.changeView = function (system) {
            if (system != "Admin") { $location.path('/pageView'); }
            else { $location.path('/addFix'); }

            $("#system").text(system);
            $('#system').show();
        };
    }
    
})();


