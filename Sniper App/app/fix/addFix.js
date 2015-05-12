(function () {
    'use strict';
    var controllerId = 'addFix';
    angular.module('app').controller(controllerId, ['common', 'datacontext', '$scope', addFix]);

    function addFix(common, datacontext, $scope) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;

        vm.applicationsAvailable = [];
        vm.databasesAvailable = [];
        vm.applicationDatabasesAvailable = [];
        vm.fixesAvailable = []; 

        $scope.selectApplication = selectApplication;
        $scope.selectDatabase = selectDatabase;

        $scope.storedProcFound = true;

        $scope.submitForm = function (isValid) {
            // check to make sure the form is completely valid
            if (isValid) {
                //alert('our form is amazing');
                if (isSave) {
                    datacontext.updateEntity().then(function () {
                        $window.history.back();
                    })
                    .catch(function (data) {
                        alert('saveFailed');
                    });
                }
            }
        };

        activate();

        //when stored procedure name entering is clicked off
        //check if stored procedure is part of selected database
        $(document).ready(function () {
            $("#storedUpdateProcFound").hide();
            $("#storedUpdateProcNotFound").hide();

            $("#storedUpdateProcName").focusout(function () { 

                var notFound = "Could not";
                var applicationName = $("#applicationSelection option:selected").text();
                var storedProcName = $("#storedUpdateProcName").val();

                return datacontext.checkForSProc(applicationName, storedProcName).then(function (data) {
                    var error = data.results; 

                    if (String(error).indexOf(notFound) > -1) {
                        $("#storedUpdateProcNotFound").show();
                        $("#storedUpdateProcFound").hide();
                    }
                    else {
                        $("#storedUpdateProcFound").show();
                        $("#storedUpdateProcNotFound").hide();
                    }
                });

            });

            $("#storedViewProcFound").hide();
            $("#storedViewProcNotFound").hide();
            $("#storedViewProcName").focusout(function () {
                //put in check here.
                $("#storedViewProcFound").show();
            });

            //Click icon to close table
            $("#toggleIcon").click(function () {
                $("#toggleTable").slideToggle('slow');
                $("#toggleIcon").toggleClass("fa fa-minus-square fa fa-plus-square");
            });
        });

        function activate() {
            var promises = [getApplications(), getDatabases(), getFixes() ];
            common.activateController(promises, controllerId)
                .then(function () {
                    log('Fixes Loaded');
                });
        }
        
        function getApplications() {
            return datacontext.getApplications().then(function (data) {
                vm.applicationsAvailable = data.results;

                $(vm.applicationsAvailable).each(function (index, application) {
                    if (application.Active == false) {
                        var index = vm.applicationsAvailable.indexOf(vm);
                        application.applicationsAvailable.splice(index, 1);
                    }
                });
            });
        }

        function getDatabases() {
            return datacontext.getDatabases().then(function (data) {
                vm.databasesAvailable = data.results; 

                $(vm.databasesAvailable).each(function (index, database) {
                    if (database.Active == false) {
                        var index = vm.databasesAvailable.indexOf(database);
                        vm.databasesAvailable.splice(index, 1); 
                    }
                });
            });
        }

        function getFixes() {
            return datacontext.getFixes().then(function (data) {
                vm.fixesAvailable = data.results;

                $(vm.fixesAvailable).each(function (index, fix) {
                    if (fix.Active == false) {
                        var index = vm.fixesAvailable.indexOf(fix);
                        vm.fixesAvailable.splice(index, 1);
                    }
                });
            });
        }

        function selectApplication() {
            var appDatabases = $.grep($scope.applicationSelection.ApplicationDatabases, function (e) { return e.ApplicationID == $scope.applicationSelection.ApplicationID });
            vm.applicationDatabasesAvailable = $.grep(vm.databasesAvailable, function (db) {
                return $.grep(appDatabases, function (appD) { return appD.DatabaseID == db.DatabaseID }).length > 0;
            });
        }

        function selectDatabase() {
            var test = $scope.databaseSelection;

            return datacontext.getDatabaseTables($scope.databaseSelection.DatabaseID).then(function (data) {
                vm.tablesAvailable = data.results;
            });
        }

        $scope.trDblClick = function (id) {

            if ($("tr").length > 2) {
                $('tr').hide(); // hide all rows
                $('#' + id).show();
                $('thead tr').show();
            }
        };
    }    
})();
