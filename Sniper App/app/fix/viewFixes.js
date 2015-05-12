(function () {
    'use strict';
    var controllerId = 'viewFixes';
    angular.module('app').controller(controllerId, ['common', '$scope', 'datacontext', '$modal', viewFixes]);

    function viewFixes(common, $scope, datacontext, $modal) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        $scope.fixes = [];// [{ Name: "Fix1", Description: "testing desc alpha" }, { Name: "Fix2", Description: "testing desc beta" }, { Name: "Add BCN to Account", Description: "Deactivate BCN(s) from Account" }];
        $scope.accounts = [];
        $scope.selectFix = selectFix;
        $scope.selectedRowId = 3;
        $scope.confirmFix = confirmFix;
        $scope.fixSearch = "";
        //$scope.fixSearch.FixID = "";

        $scope.status = {
            isopen: false
        };

        activate();

        function activate() {
            var promises = [ getAppEnvironmentFixes()];
            common.activateController(promises, controllerId)
                .then(function () {
                    //log('Activated Fixes View');
                });
        }

        function getAppEnvironmentFixes() {
            return datacontext.getAppEnvironmentFixes(3).then(function (data) {
                $scope.fixes = data.results;
            });
        }

        function selectFix(fixID) {
            //$scope.changeView = $location.path('/personedit/' + personId);
            $scope.selectedRowId = fixID;

            if ($scope.fixSearch.FixID === fixID)
            {
                $scope.fixSearch.FixID = "";
            }
            else
            {
                $scope.fixSearch.FixID = fixID;
            }
            //alert("RowID: " + fixID + " Selected row: " + $scope.selectedRowId);

            //confirmFix();
        }

        function selectAccount() {
            //
        }

        function confirmFix() {
            var modalInstance = $modal.open({
                templateUrl: 'confirmfix.html',
                controller: 'ConfirmPersonCtrl',
                resolve: {
                    items: function () {
                        return [vm.person];
                    }
                }
            });
        }// ng-click="confirmFix()"
    }
})();

(function () {
    'use strict';

    var controllerId = 'ConfirmPersonCtrl';
    angular.module('app').controller(controllerId, ['$window', '$scope', '$modalInstance', 'datacontext', 'items', ConfirmPersonCtrl]);

    function ConfirmPersonCtrl($window, $scope, $modalInstance, datacontext, items) {
        //var getLogFn = common.logger.getLogFn;
        //var log = getLogFn(controllerId);

        $scope.submit = function () {
            //items[0].Archived = true;
            //datacontext.updateEntity();
            $modalInstance.close();
            //$window.history.back();
        };
        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }
})();