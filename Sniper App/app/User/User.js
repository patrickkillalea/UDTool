(function () {
    'use strict';
    var controllerId = 'User';
    angular.module('app').controller(controllerId, ['common', 'datacontext', '$scope', '$window', User]);

    function User(common, datacontext, $scope, $window) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        var isSave = true;
        vm.groups = [];
        vm.users = [];
        var message = "";
        vm.userType = [{ ID: 1, TypeName: "General" }, { ID: 2, TypeName: "Admin" }];
        vm.userActive = [{ Text:"Active" ,Active: true }, {Text:"Inactive" , Active: false }];

        $scope.applicationPristine = true;
        $scope.fixPristine = true;
        $scope.cancel = cancel;
        $scope.enable = enable;
        $scope.addUser = addUser;
        $scope.updateUser = updateUser;
        $scope.isCollapsed;
        $scope.convertUNIX = convertUNIX;


        function convertUNIX(timestamp) {
            var date = new Date(timestamp);
            var dateUNIX = date.getTime();
            return dateUNIX;
        };


        $scope.filterDates = function (row) {
            var fromDate = new Date($scope.dateFrom);
            fromDate = fromDate.getTime(); //start of input range
            var endDate = new Date($scope.endDate);
            endDate = endDate.getTime() + 86340000; //+24 Hours
            if (fromDate < convertUNIX(row.TimeStamp) && convertUNIX(row.TimeStamp) < endDate) { // if the start date and end date are present compare with timestamp show between
                return true;
            }
            else if (endDate > convertUNIX(row.TimeStamp) && (typeof $scope.dateFrom == 'undefined' || $scope.dateFrom == '')) { // if there is a end date but no start date show all less than end Date
                return true;
            }
            else if (fromDate < convertUNIX(row.TimeStamp) && (typeof $scope.endDate == 'undefined' || $scope.endDate == '')) { //if there is a start date but no end date show all greater than startDate
                return true;
            }
            else if ((typeof $scope.endDate == 'undefined' || $scope.endDate == '') && (typeof $scope.dateFrom == 'undefined' || $scope.dateFrom == ''))// no start or end date show all 
            {
                return true
            }
            return false;


            //converting to unix timestamp and comparing with hidden column

        };

        $scope.typeChange = function (text,id) {
          
            $('#typeBtn').text(text);
            $('#activeType').val(id);
            $('#activeType').trigger('input');
        };
        $scope.activeChange = function (text,active) {
            $('#activeBtn').text(text);
            $('#activeStatus').val(active);
            $('#activeStatus').trigger('input');

        };

        $scope.searchUser = function (row) {
            if (angular.lowercase(row.LanID).indexOf(angular.lowercase($scope.searchUserPerson)) != -1 || angular.lowercase(row.LastName).indexOf(angular.lowercase($scope.searchUserPerson)) != -1 || angular.lowercase(row.FirstName).indexOf(angular.lowercase($scope.searchUserPerson)) != -1) {
                return true;
            }
            return false;
        };


        $scope.collapse = function () {
            if ($('#filterBtn').text() == 'Clear Filters') {
                $('#filterBtn').text('Filters');
                $scope.isCollapsed = true;
                $('#activeBtn').text('Active/Inactive');
                $('#groupBtn').text('Group');
                $('#typeBtn').text('User Type');
               
                $('.filterModels').val(''); 
                $('#startDate').val('');
                $('#endDate').val('');
                $('#startDate').trigger('input');
                $('#endDate').trigger('input');
                $('.filterModels').trigger('input');
            }
            else { //equal false
                //opening filters
                $('.filterModels').trigger('input');
                $('#filterBtn').text('Clear Filters');
                $('#startDate').trigger('input');
                $('#endDate').trigger('input');
                $scope.isCollapsed = false;
            }
            return $scope.isCollapsed;
        };
        $scope.submitForm = function (isValid) {
            // check to make sure the form is completely valid
            if (isValid) {
                alert('our form is amazing');
                if (isSave) {
                    $scope.user.Permissions = permissionSelection;
                    $scope.user.TimeStamp = moment().format('L LT');
                    datacontext.addUser($scope.user).then(function () {
                        $window.history.back();
                    })
                    .catch(function (data) {
                        alert('saveFailed');
                    });
                }
            }
        };

        $scope.enableEdit = function () {
            var flag = true;
            var i = 0;

            if ($("#displayHolder option:selected").text() != "-- Choose LAN ID To Edit --") {
                $("#editButton").prop("disabled", false);


                $("#editFName").prop("disabled", false);
                $("#editLName").prop("disabled", false);
                $("#editType").prop("disabled", false);
                $("#editGroup").prop("disabled", false);
                $("#editSave").prop("disabled", false);
                $("#editCancel").prop("disabled", false);
                $("#editActive").prop("disabled", false);

                while (flag == true) {
                    if ($("#displayHolder option:selected").text() == vm.users[i].LanID) {
                        $("#editFName").val(vm.users[i].FirstName);
                        $("#editLName").val(vm.users[i].LastName);
                        $("#editType").val(vm.users[i].UserType);
                        $("#editGroup option:selected").text(vm.users[i].Group_GroupID);

                        if (vm.users[i].Active == true) {
                            $("#editActive option:selected").text("Activate");
                            $("#editActive option:selected").val(1);
                        }
                        else{
                            $("#editActive option:selected").text("Deactivate");
                            $("#editActive option:selected").val(0);
                        }
                        
                        flag = false;
                    }
                    else {
                        i++
                    }
                }//while

                $("#editFName").prop("disabled", true);
                $("#editLName").prop("disabled", true);
                $("#editType").prop("disabled", true);
                $("#editGroup").prop("disabled", true);
                $("#editSave").prop("disabled", true);
                $("#editCancel").prop("disabled", true);
                $("#editActive").prop("disabled", true);

                log('Ability to edit User Info enabled');
            }
            else {
                $("#editButton").prop("disabled", true);
                log('Ability to edit User Info disabled');
            }


        };

        function enable() {

            if ($("#editFName").prop("disabled") == true) {

                $("#editFName").prop("disabled", false);
                $("#editLName").prop("disabled", false);
                $("#editType").prop("disabled", false);
                $("#editGroup").prop("disabled", false);
                $("#editSave").prop("disabled", false);
                $("#editCancel").prop("disabled", false);
                $("#editActive").prop("disabled", false);

                log('Features enabled');
            }

            else {
                $("#editFName").prop("disabled", true);
                $("#editLName").prop("disabled", true);
                $("#editType").prop("disabled", true);
                $("#editGroup").prop("disabled", true);
                $("#editSave").prop("disabled", true);
                $("#editCancel").prop("disabled", true);
                $("#editActive").prop("disabled", true);
                log('Features disable');

            }

        }// enable

        activate();

        function activate() {
            var promises = [getGroups(), getUsers()];
            common.activateController(promises, controllerId)
                .then(function () { log('User Functions Loaded'); });
            setTimeout(function () {
               
                $('.filterModels').trigger('input');
                
            }, 100);
            $scope.isCollapsed = true;
            $('.input-daterange').datepicker({
                clearBtn: true,
                autoclose: true,
                todayBtn: "linked",
                orientation: "top auto"
            });
            $('#startDate .datepicker').datepicker({
                format: "dd/mm/yyyy"
            });
            $('#endDate .datepicker').datepicker({
                format: "dd/mm/yyyy"
            });

            $('.datepicker').datepicker();
        }

        function cancel() {
            isSave = false;
            $window.history.back();
        }

        function getGroups() {
            return datacontext.getGroup().then(function (data) {
                vm.groups = data.results;
                return vm.groups.results;
            });
        }

        function getUsers() {
            return datacontext.getUser().then(function (data) {
                vm.users = data.results;
                return vm.users;
            });
        }

        $scope.cBtnChange = function (text) {
            $('#cBtn').text(text);
        };
        
        $scope.trBcnClick = function (trBcnid, bcnNum) {

            bcnRowID = trBcnid;
            bcnNumber = bcnNum;

            if ($("#bcnTable tr").length > 2 && $scope.bcnChecked.value == true) {
                $('#bcnTable tr').hide(); // hide all rows 
                $('#bcnTable #' + trBcnid).show();
                $('#bcnTable thead tr').show();

            }

            else {
                $('#bcnTable tr').show();
            }
        };

        function addUser() {

            var LanID = $('#addLan').val();
            var FirstName = $('#addFName').val();
            var LastName = $('#addLName').val();
            var UserType = $('#addType').val();
           // var Group_GroupID = $('#addGroup').val();
            
            return datacontext.addUser(LanID, FirstName, LastName, UserType).then(function (data) {
                log('LAN ID: ' + LanID + " added to database ");//Group_GroupID removed
                location.reload();
                message = data;
                return message;
            });
            
        }

        function updateUser() {

            var LanID = $("#displayHolder option:selected").text();
            var FirstName = $('#editFName').val();
            var LastName = $('#editLName').val();
            var UserType = $('#editType').val();
            //var Group_GroupID = $('#editGroup option:selected').val();
            var Active = $('#editActive option:selected').val();


            return datacontext.updateUser(LanID, FirstName, LastName, UserType /*,Group_GroupID*/, Active).then(function (data) {
                log('LAN ID: ' + LanID + " Has Been Updated ");
                location.reload();
                message = data;
                return message;
            });
            
        }

    }//User

}
)();
