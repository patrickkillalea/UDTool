(function () {
    'use strict';
    var controllerId = 'admin';
    angular.module('app').controller(controllerId, ['common', '$scope', 'datacontext', '$location', admin]);

    function admin(common, $scope, datacontext, $location) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.userType = [{ ID: 0, TypeName: "General" }, { ID: 1, TypeName: "Administrator" }];
        
        $scope.addUser = addUser;
        $scope.addGroup = addGroup;
        $scope.addApplication = addApplication;
        $scope.addDatabase = addDatabase;
        $scope.editUser = editUser;
        $scope.editApplication = editApplication;
        $scope.editDatabase = editDatabase;

        activate();

        function activate() {
            var promises = [getUsers(), getApplications(), getDatabases()];
            common.activateController(promises, controllerId)
                .then(function () {
                    log('Activated Admin View');
                });
        }

        function addUser() {
            $scope.changeView = $location.path('/adduser');
        }

        function addGroup() {
            $scope.changeView = $location.path('/addgroup');
        }

        function addApplication() {
            $scope.changeView = $location.path('/addapplication');
        }

        function addDatabase() {
            $scope.changeView = $location.path('/adddatabase');
        }

        function getUsers() {
            return datacontext.getUsers().then(function (data) {
                vm.usersAvailable = data.results;

                $(vm.usersAvailable).each(function (index, user) {
                    user['UserTypeText'] = $.grep(vm.userType, function (e) { return e.ID == user.UserType })[0].TypeName;

                    if (user.Active == false) {
                        var index = vm.usersAvailable.indexOf(user);
                        vm.usersAvailable.splice(index, 1);
                    }
                });
            });
        }
        
        function editUser(userID) {
            $scope.changeView = $location.path('/edituser/' + userID);
        }

        function getApplications() {
            return datacontext.getApplications().then(function (data) {
                vm.applicationsAvailable = data.results;

                $(vm.applicationsAvailable).each(function (index, application) {
                    if (application.Active == false) {
                        var index = vm.applicationsAvailable.indexOf(application);
                        vm.applicationsAvailable.splice(index, 1);
                    }
                });
            });
        }

        function editApplication(applicationID) {
            $scope.changeView = $location.path('/editapplication/' + applicationID);
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

        function editDatabase(databaseID) {
            $scope.changeView = $location.path('/editdatabase/' + databaseID);
        }
    }
})();