(function () {
    'use strict';
    var controllerId = 'admin';
    angular.module('app').controller(controllerId, ['common', '$scope', 'datacontext', '$modal', admin]);

    function admin(common, $scope, datacontext, $modal) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'Admin';

        $scope.accounts = [];

        activate();

        function activate() {
            var promises = [getHarmonyAccounts()];
            common.activateController(promises, controllerId)
                .then(function () { log('Activated Admin View'); });
        }

        function getHarmonyAccounts() {
            return datacontext.getHarmonyAccounts().then(function (data) {
                vm.accounts = $.extend(true, [], data.results);
            });
        }
    }
})();