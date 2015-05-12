(function () {
    'use strict';
    var controllerId = 'pageView';
    angular.module('app').controller(controllerId, ['common', 'datacontext', '$scope', pageView]);

    function pageView(common, datacontext, $scope) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.accounts = [];
        vm.fixes = [];
        vm.bcns =[];

        var message = "";
        var harmonyRowID;
        var bcnRowID;
        var AccountNumber;
        var AccountName;
        var AccountID;
        var bcnNumber; 
       

        activate();

        //for ordering the table by its headers
        $scope.orderByField = 'AccountName';
        $scope.reverseSort = false;

        function activate() {
            var promises = [];
            common.activateController(promises, controllerId)
                .then(function () {
                    setTimeout(function () {
                        $('#system').text('Harmony');
                        $('#system').show();
                        log('Harmony Loaded');
                    }, 100)
                    
                });
            $('#toggleSwitch').hide();
        }
        $scope.getHarmonyAccounts = function (searchCriteria) {

            $scope.checked.value = false;
            $scope.bcnChecked.value = false;

            //var option = $('#cBtn').text();
            //$("#fixArea").hide();
            var option = $scope.orderByField;
            
            recordSearchAudit();

            getFixes();

            vm.accounts = [];
            if (searchCriteria == 'undefined')
            {
                searchCriteria = ' '; 
            }
            return datacontext.getHarmonyAccounts(option, searchCriteria).then(function (data) {
                if (data.results.length === 0) {
                    $("#resultsSection").hide();
                    log('No results found for: ' + option + " containing " + searchCriteria);
                }
                else {
                    $('tr').show();
                    $("#resultsSection").show();

                    for (var i = 0; i < data.results.length; i++) {

                        var acc = { "AccountId": data.results[i].AccountId, "AccountNumber": data.results[i].HarmonyAccountNumber, "AccountName": data.results[i].AccountName };

                        vm.accounts.push(acc);
                }

                    return vm.accounts;
                }

            });

        };

        $scope.bcnChecked = {
            value: false
        };

        $scope.checked = {
            value: false
        };

        function recordSearchAudit() {
            //     AHV15, Harmony,Search, Account Name, -1, Dev11, Don*, ""
            // function recordAuditSearch(userID,system,enviroment,eventType, keyval,fixId,valstart,valend)
            if ($("#search").val() != "") {
                datacontext.recordAuditSearch($("#lanID").text(), "Harmony", $("#teBtn").text(), "Search", $('#cBtn').text(), -1, $("#search").val(), "%");

            }
            else {
                datacontext.recordAuditSearch($("#lanID").text(), "Harmony", $("#teBtn").text(), "Search", $('#cBtn').text(), -1, "%", "%");

            }
        }

        function getFixes() {
            return datacontext.getFixes().then(function (data) {
                return vm.fixes = data.results;
            });
        }

        $scope.teBtnChange = function (text) {
            $('#teBtn').text(text);

            if (text.indexOf("Production") > -1) {
                $('#prodAlert').show();
            }
            else {
                $('#prodAlert').hide();
            }
            if (text === 'Target Environment') {
                $('#toggleSwitch').hide();
                $('#searchArea').hide();
            }
            else {
                $('#toggleSwitch').show();
                $('#searchArea').show();
                
            }
        };

        $scope.cBtnChange = function (text) {
            $('#cBtn').text(text);
        };


        $scope.trDblClick = function (id) {

            if ($("#harmonyAcooutsTable tr").length > 2 && $scope.checked.value == true) {
                $('#harmonyAcooutsTable tr').hide(); // hide all rows 
                $('#harmonyAcooutsTable #' + id).show();
                //-----------------------------------------
                //used for changing  the hover css for when one table is selected
                $('tr').hover(function () {
                    $(this).css({ 'backgroundColor': '#535c70;' });
                }, function () {
                    $(this).css({ 'backgroundColor': '#535c70;' });
                });
                //-------------------------------------------
                $('#harmonyAcooutsTable thead tr').show();
                harmonyRowID = id;
            }

            else {
                $('#harmonyAcooutsTable tr').show();
                //------------------------------------
                //used for changing  back the hover
                $('tr').hover(function () {
                    $(this).css({ 'backgroundColor': '#F6FAFE;' });
                }, function () {
                    $(this).css({ 'backgroundColor': '#535c70;' });
                });
                //------------------------------------
                datacontext.recordAuditSearch($("#lanID").text(),"Harmony", $("#teBtn").text(), "Remove BCN", $('#cBtn').text(), 0, id, "%");
            }
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

        $scope.correctionItemClick = function (fix, account) {
            $scope.bcnChecked.value = false;
            if (fix.Name != "Deactivate BCN from Account") {
                $("#modaltitle").html(fix.Name);
                $("#modalbody").html(fix.HtmlCode);
                $("#modalbody").show();
                $("#modalHeader").show();
                $("#modalfooter").show();
                $("#fixArea").show();
                $(".accountName").val(account.AccountName);
                $(".accountNumber").val(account.AccountNumber);
                $("#deactivateBcn").hide(); 
            }
            else{
                $("#deactivateBcn").show();
                $("#Button3").show();
                $("#Button4").show();
                $("#buttonDiv").show();
                $("#Article2").show();
                $("#modalbody").hide();
                $("#modalfooter").hide();
                $("#modalHeader").hide();
            }


            AccountNumber = account.AccountNumber;
            AccountName = account.AccountName;
            AccountID = account.AccountId;
            datacontext.recordAuditSearch($("#lanID").text(), "Harmony", $("#teBtn").text(), "Edit Account", $('#cBtn').text(), -1, AccountID, "%");
            if (fix.Name == "Deactivate BCN from Account") {
                return datacontext.getBcn(AccountNumber).then(function (data) {
                    return vm.bcns = data.results;
                });
               //  bcns = datacontext.getBcn(AccountNumber);
            }
            
            
        };

        $scope.hideWindow = function (elementID) {
            $("#" + elementID).hide();
        };

        $scope.addBCNToAccount = function (searchCriteria) {

            var BCN = $('#BCN').val();
            datacontext.recordAuditSearch($("#lanID").text(),"Harmony", $("#teBtn").text(), "Add BCN", $('#cBtn').text(), 2, "%", BCN);
            return datacontext.addBCNToAccount(AccountID, BCN).then(function (data) {
                log('BCN: ' + BCN + " added to account " + AccountName);
                return message = data;
               
            });
        };

        $scope.removeBCNToAccount = function () {
            datacontext.recordAuditSearch($("#lanID").text(), "Harmony", $("#teBtn").text(), "Remove BCN", $('#cBtn').text(), 1, bcnNumber, "%");

            return datacontext.removeBCNToAccount(bcnRowID).then(function (data) {
                $("#deactivateBcn").hide();
                log('The BCN number ' + bcnNumber + ' BCN Id ' + bcnRowID + " Have Been Removed ");
                return message = data;
            });
        };




    }// page view

    }
)();
