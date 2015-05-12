(function () {
    'use strict';
    var controllerId = 'addEditDatabase';
    angular.module('app').controller(controllerId, ['common', 'datacontext', '$scope', addEditDatabase]);

    function addEditDatabase(common, datacontext, $scope) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.applicationsAvailable;
        vm.databasesAvailable;
        vm.serversAvailable = [];
        vm.databasesToEdit;

        var dbToBeEdited;
        var serverToBeEdited;
        var editActive;

        $scope.selectApplication = selectApplication;
        $scope.selectDatabase = selectDatabase; 

        activate();

        /*
        $scope.checkedBox = function () {
            console.log($("#activeSwitch").is(":checked"));
        };
        */

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
                    getDatabases();
                    log("Added DB " + database.Name + " to the server " + database.Server);
                    getDatabases(); 
                })
                    .catch(function (data) {
                        alert('saveFailed');
                    });
            }
        };

        /*$scope.addDatabase = function () {
            var validAdd = true;

            var dbName = $("#databaseName").val();
            var serverName = $("#serverName option:selected").text();
            var active = $("#activeSwitch").is(":checked");

            //a validity check for the adding of databases
            var arrayLength = vm.databasesAvailable.length;
            for (var i = 0; i < arrayLength; i++) {
                if (vm.databasesAvailable[i].Name == dbName && vm.databasesAvailable[i].Server == serverName) {
                    validAdd = false;
                    log("The database exists on the specified server!");
                    break;
                }
            }

            if (validAdd) {
                return datacontext.addDatabase(dbName, serverName, active).then(function (data) {
                    getDatabases();
                    log("Added DB " + dbName + " to the server " + serverName);
                    getDatabases();
                    return data;
                });
            }
        }; */

        $scope.removeDatabase = function () {
            var dbName = $("#editDatabase option:selected").text();
            //getting database ID for the selected database
            var dbToBeEdited = vm.databasesAvailable.filter(function (obj) {
                return obj.Name == dbName;
            });
            var dbID = dbToBeEdited[0].DatabaseID;

            return datacontext.removeDatabase(dbID).then(function (data) {
                log("Deleted DB " + dbName);
                $("#saveEditBtn").prop("disabled", true);
                $("#removeDB").prop("disabled", true);
                getDatabases();
                $scope.cancelEdit();
                return data;
            });
        }
        $scope.editDatabase = function () {
            var validEdit = true;

            var oldDBName = $("#editDatabase option:selected").text(); 
            var oldServer = $("#editServer option:selected").text(); 

            database.Name = $("#editDatabaseName").val();
            database.Server;
            database.Active = $("#editActiveSwitch").is(":checked");
            database.TimeStamp = moment().format('L LT');

            if ($("#editServerName option:selected").text() == "-- Choose Server --") { newServer = oldServer; }
            else { database.Server = $("#editServerName option:selected").text(); } 

            //getting database ID for the selected database
            var dbToBeEdited = vm.databasesAvailable.filter(function (obj) {
                return obj.Name == oldDBName;
            });
            var dbID = dbToBeEdited[0].DatabaseID;

            var arrayLength = vm.databasesAvailable.length;

            if (oldDBName == database.Name && oldServer == database.Server && editActive == database.Active) {
                validEdit = false;
                log("No changes were made to the database");
            }

            if (validEdit) {

                datacontext.addDatabase(database).then(function () {
                    log("Edited DB" + oldDBName + " on the server " + oldServer);
                    $("#saveEditBtn").prop("disabled", true);
                    $scope.cancelEdit();
                    getDatabases();
                })
                    .catch(function (data) {
                        alert('saveFailed');
                    });
            }
        };

        /*
        $scope.editDatabase = function () {
            var validEdit = true;

            var oldDBName = $("#editDatabase option:selected").text();
            var newDBName = $("#editDatabaseName").val();
            var oldServer = $("#editServer option:selected").text();
            var newServer;

            if ($("#editServerName option:selected").text() == "-- Choose Server --") { newServer = oldServer; }
            else { newServer = $("#editServerName option:selected").text(); }
            var active = $("#editActiveSwitch").is(":checked");

            //getting database ID for the selected database
            var dbToBeEdited = vm.databasesAvailable.filter(function (obj) {
                return obj.Name == oldDBName;
            });
            var dbID = dbToBeEdited[0].DatabaseID;

            var arrayLength = vm.databasesAvailable.length;

            if (oldDBName == newDBName && oldServer == newServer && editActive == active) {
                validEdit = false;
                log("No changes were made to the database");
            }

            if (validEdit) {

                return datacontext.editDatabase(dbID, newDBName, newServer, active).then(function (data) {
                    log("Edited DB" + oldDBName + " on the server " + oldServer);
                    $("#saveEditBtn").prop("disabled", true);
                    $scope.cancelEdit();
                    return data;
                    getDatabases();
                });
            }
        };*/

        //used to populate database to edit dropdown when the server is picked
        $scope.editDatabases = function () {
            vm.databasesToEdit = [];
            var serverName = $("#editServer option:selected").text();

            var arrayLength = vm.databasesAvailable.length;
            for (var i = 0; i < arrayLength; i++) {
                if (vm.databasesAvailable[i].Server == serverName) {
                    vm.databasesToEdit.push(vm.databasesAvailable[i].Name);
                }
            }
        }

        $scope.showEdit = function () {
            dbToBeEdited = $("#editDatabase option:selected").text();
            serverToBeEdited = $("#editServer option:selected").text();
            $("#editServer").prop("disabled", true);
            $("#editDatabase").prop("disabled", true);
            $("#saveEditBtn").prop("disabled", false);
            $("#removeDB").prop("disabled", false);

            var arrayLength = vm.databasesAvailable.length;
            for (var i = 0; i < arrayLength; i++) {
                if (vm.databasesAvailable[i].Name == dbToBeEdited && vm.databasesAvailable[i].Server == serverToBeEdited) {
                    editActive = vm.databasesAvailable[i].Active;
                }
            }

            $("#editDatabaseName").val(dbToBeEdited);
            $("#editServerName").value = (serverToBeEdited);
            $("#editActiveSwitch").prop('checked', editActive);

            $('#hiddenUntilEdit').fadeIn();
        }

        $scope.cancelEdit = function () {
            $("#editServer").prop("disabled", false);
            $("#editDatabase").prop("disabled", false);
            $("#saveEditBtn").prop("disabled", true);
            $('#hiddenUntilEdit').fadeOut();
            $("#removeDB").prop("disabled", true);
        }

        function activate() {
            var promises = [getApplications(), getDatabases()];
            common.activateController(promises, controllerId)
                .then(function () {
                    log('Databases Loaded');
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
                vm.serversAvailable = [];
                vm.databasesAvailable = data.results;

                //making sure there are only unique items for the servers
                var arrayLength = vm.databasesAvailable.length;
                for (var i = 0; i < arrayLength; i++) {
                    if (vm.serversAvailable.indexOf(vm.databasesAvailable[i].Server) < 0) {
                        vm.serversAvailable.push(vm.databasesAvailable[i].Server);
                    }
                }
                //removing inactive databases

                /*$(vm.databasesAvailable).each(function (index, database) {
                    if (database.Active == false) {
                        var index = vm.databasesAvailable.indexOf(database);
                        vm.databasesAvailable.splice(index, 1);
                    }
                });*/
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
