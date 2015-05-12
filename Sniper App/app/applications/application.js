(function () {
    'use strict';
    var controllerId = 'application';
    angular.module('app').controller(controllerId, ['common', 'datacontext', '$scope', '$modal', application]);

    function application(common, datacontext, $scope, $modal) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.applicationsAvailable;
        vm.databasesAvailable;
        $scope.serversAvailable = [];

        var validName;
        var validStoredProc;
        var applicationActive;

        $scope.selectApplication = selectApplication;
        $scope.selectDatabase = selectDatabase;

        $scope.databasesForEnvironment = [];
        $scope.applicationEnvironments = [];

        activate();

        $(document).ready(function () {
            $("#storedProcFound").hide();
            $("#storedProcNotFound").hide();

            $("#storedProcName").focusout(function () {

                var notFoundCheck = "Could not";
                var notFoundCheckTwo = "not been initialized";
                var storedProcName = $("#storedProcName").val();

                return datacontext.checkForSProc("Sniper", storedProcName).then(function (data) {
                    var error = data.results;

                    if (String(error).indexOf(notFoundCheck) > -1 || String(error).indexOf(notFoundCheckTwo) > -1) {
                        $("#storedProcNotFound").show();
                        $("#storedProcFound").hide();
                    }
                    else {
                        validStoredProc = true;
                        $("#storedProcFound").show();
                        $("#storedProcNotFound").hide();
                    }
                });
            });

            $("#applicationName").focusout(function () {
                var applicationName = $("#applicationName").val().toLowerCase();
                var arrayLength = vm.applicationsAvailable.length;
                for (var i = 0; i < arrayLength; i++) {
                    if (vm.applicationsAvailable[i].Name.toLowerCase() == applicationName || applicationName == "") {
                        log("Application with that name already exists or the name is invalid");
                        validName = false;
                        break;
                    }
                    else {
                        validName = true;
                    }
                }
            });
        });

        $scope.showEnvironments = function () {
            if (validName) { 
                $("#addEnvironmentsForm").fadeIn();
                $("#applicationName").prop("disabled", true);
                $("#applicationActive").prop("disabled", true);
                $("#storedProcName").prop("disabled", true);
            }
            else {
                log("You need to enter a valid Application name");
            }
        };

        $scope.assignDatabase = function (dbName, takingArray, receivingArray) {
            var arrayLength = takingArray.length;
            for (var i = 0; i < arrayLength; i++) {
                if (takingArray[i].Name == dbName) {
                    receivingArray.push(takingArray.splice(i, 1)[0]);
                    break;
                }
            }
        };

        $scope.addEnvironment = function () {
            var name = $("#environmentName").val();
            var active = $("#environmentActive").is(":checked");
            var databases = $scope.databasesForEnvironment;

            if (name == "" || databases.length <= 0) {
                log("Name invalid or no databases selected");
            }
            else {
                $scope.applicationEnvironments.push({ 'Name': name, 'ApplicationID': '', 'Databases': databases, 'Active': active });
                $("#addEnvironmentsForm").fadeOut();
                var name = $("#environmentName").val('');
                $scope.databasesForEnvironment = [];
                getDatabases();
                $("#addAppBtn").fadeIn();
                $("#applicationName").prop("disabled", false);
                $("#applicationActive").prop("disabled", false);
                $("#storedProcName").prop("disabled", false);
            }
        };

        $scope.addApplication = function () {
            if (validName && $scope.applicationEnvironments.length > 0 && validStoredProc) {
                var application = { 'Name': '', 'DefaultViewStoredProcedure': '', 'Active': false, 'TimeStamp': null };
                application.Name = $("#applicationName").val();
                application.DefaultViewStoredProcedure = $("#storedProcName").val();
                application.Active = $("#applicationActive").is(":checked");
                application.TimeStamp = moment().format('L LT');

                datacontext.addApplication(application).then(function (data) {

                    datacontext.addAppEnvironments($scope.applicationEnvironments, data.ApplicationID)

                    //reseting data and form fields
                    $("#applicationName").prop("disabled", false);
                    $("#applicationActive").prop("disabled", false);
                    $("#storedProcName").prop("disabled", false);

                    getApplications();
                    $scope.applicationEnvironments = [];
                    $("#applicationName").val('');
                    $("#storedProcName").val('');
                    $("#addAppBtn").fadeOut();
                    validName = false;
                    validStoredProc = false;
                });
            }

            else {
                log("Error, invalid info");
            }
        };

        $scope.open = function (size) {
            $scope.modalInstance = $modal.open({
                size: 'lg',
                animation: true,
                templateUrl: 'databaseModal.html',
                scope: $scope,
                backdropClass: 'modal-backdrop h-full',
            });
        };

        $scope.addDatabase = function () {
            var validAdd = true;
            var database = { 'Name': '', 'Server': '', 'Active': false, 'TimeStamp': null };
            database.Name = $("#databaseName").val();
            database.Server = $("#serverName option:selected").text();
            database.Active = $("#activeSwitch").is(":checked");
            database.TimeStamp = moment().format('L LT'); 

            //a validity check for the adding of databases
            var arrayLength = vm.databasesAvailable.length;
            for (var i = 0; i < arrayLength; i++) {
                if (vm.databasesAvailable[i].Name == database.Name && vm.databasesAvailable[i].Server == $scope.database.Server) {
                    validAdd = false;
                    log("The database exists on the specified server!");
                    break;
                }
            }

            if (validAdd) {
                datacontext.addDatabase(database).then(function () { 
                    log("Added DB " + database.Name + " to the server " + database.Server);
                    $scope.databasesForEnvironment.push(database);
                })
                    .catch(function (data) {
                        alert('saveFailed');
                    });
                $scope.modalInstance.close();
            }
        };

        $scope.cancelAddEnvironment = function () {
            $("#addEnvironmentsForm").fadeOut();
            $("#environmentName").val('');
            $scope.databasesForEnvironment = [];
            getDatabases();
            $("#addAppBtn").fadeIn();
            $("#applicationName").prop("disabled", false);
            $("#applicationActive").prop("disabled", false);
            $("#storedProcName").prop("disabled", false);
        };

        $scope.cancelModal = function () {
            $scope.modalInstance.close();
        }

        $scope.search = function (row) {
            if (angular.lowercase(row.Name).indexOf($scope.filterName) != -1 || angular.lowercase(row.Server).indexOf($scope.filterName) != -1) {
                return true;
            }
            return false;
        };

        //for ordering the table by its headers (databases)
        $scope.orderByField = 'Name';
        $scope.reverseSort = false;
        $scope.filterName = '';

        $('[data-toggle="tooltip"]').tooltip({
            placement: 'top'
        });

        function activate() {
            var promises = [getApplications(), getDatabases()];
            common.activateController(promises, controllerId)
                .then(function () {
                    log("Applications Loaded");
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
                $scope.serversAvailable = [];
                vm.databasesAvailable = data.results;

                //making sure there are only unique items for the servers
                var arrayLength = vm.databasesAvailable.length;
                for (var i = 0; i < arrayLength; i++) {
                    if ($scope.serversAvailable.indexOf(vm.databasesAvailable[i].Server) < 0) {
                        $scope.serversAvailable.push(vm.databasesAvailable[i].Server);
                    }
                }
                /*
                $(vm.databasesAvailable).each(function (index, database) {
                    if (database.Active == false) {
                        var index = vm.databasesAvailable.indexOf(database);
                        vm.databasesAvailable.splice(index, 1);
                    }
                }); */
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
    }
})();
