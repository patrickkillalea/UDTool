(function () {
    'use strict';
    var controllerId = 'audit';
    angular.module('app').controller(controllerId, ['common', 'datacontext', '$scope', audit]);

    function audit(common, datacontext, $scope) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        
        vm.allAudits = [];
        vm.searchAudits = [];
        vm.chartAudits = [];
        vm.fixes = [];
        vm.applications = [];
        vm.enviroments = []; 
        var activeSearch;
        $scope.isCollapsed;
        //for ordering the table by its headers 
        $scope.orderByField = 'LastName';
        $scope.reverseSort = false;
        $scope.search = {};
        $scope.FIX_ONLY = 0;
        $scope.applyFixOnly = applyFixOnly; 
        $scope.convertUNIX = convertUNIX;
        

        function convertUNIX(timestamp) {
           var  date = new Date(timestamp);
            var dateUNIX = date.getTime(); 
            return dateUNIX; 
        };
        //Show only valid fixes 
        function applyFixOnly() {
            if ($scope.FIX_ONLY == 1 ) { //true 
                $('#fixOnlyBtn').css({ background: '#82b04c' });
                $('#fixOnlyBtn').text("Show All");
                $('td:contains("false")').parent().hide(); //each row has either true or false attached whether it is a valid fix or not
                //toggle fix only on
            }
            else { // false
                $('#fixOnlyBtn').css({ background: '#8f9498' });
                $('#fixOnlyBtn').text("Fixes Only");
                $('td:contains("false")').parent().show(); //toggle fix only off
            }
            
            return $scope.FIX_ONLY;
        }; 
        //collapsing filters
        $scope.collapse = function () {
            if ($('#filterBtn').text() == 'Clear Filters' )
            {
                $('#filterBtn').text('Filters');
                $scope.isCollapsed = true;
                $('#systemBtn').text('System');
                $('#enviromentBtn').text('Environment');
                $('.filterModelsAudits').val('');
                $('#startDate').datepicker('setDate', null);
                $('#endDate').datepicker('setDate', null);
                $('#startDate').val('');
                $('#endDate').val('');
                $('#startDate').trigger('input');
                $('#endDate').trigger('input');
                $('.filterModelsAudits').trigger('input');
                $scope.FIX_ONLY = 0; 
                applyFixOnly();
               
            }
            else { //equal false
                //opening filters
                $('.filterModelsAudits').trigger('input');
                $('#filterBtn').text('Clear Filters');
                $('#startDate').trigger('input');
                $('#endDate').trigger('input');
                $scope.isCollapsed = false; 
            }
            return $scope.isCollapsed;
        };
        //searching by firstname surname or lanID
        $scope.searchUser = function (row) {
            //converting text input to lowercase comparing both the current row and the text input
            if (angular.lowercase(row.User.LanID).indexOf(angular.lowercase($scope.searchUserPerson)) != -1 || angular.lowercase(row.User.LastName).indexOf(angular.lowercase($scope.searchUserPerson)) != -1 || angular.lowercase(row.User.FirstName).indexOf(angular.lowercase($scope.searchUserPerson)) != -1) {
                return true;
            }
            return false;
        };
        $scope.filterDates = function (row) {
            var fromDate = new Date($scope.dateFrom);
             fromDate = fromDate.getTime(); //start of input range
             var endDate = new Date($scope.endDate);
             endDate = endDate.getTime() + 86340000; //+24 Hours
             if (fromDate < convertUNIX(row.TimeStamp) && convertUNIX(row.TimeStamp) < endDate  ) { // if the start date and end date are present compare with timestamp show between
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
        google.setOnLoadCallback(drawChartPieChart);
        google.setOnLoadCallback(drawBarChart);
        function drawBarChart() {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Event');
            data.addColumn('number', 'Count');
            var index = 0;
            while (index !== vm.chartAudits.length) {
                if (vm.chartAudits[index].eventName === "Search" || vm.chartAudits[index].eventName === "View Audit" || vm.chartAudits[index].eventName === "Edit Account" )
                {
                    index++; 
                }
                else
                {
                data.addRow([vm.chartAudits[index].eventName, parseInt(vm.chartAudits[index].count,10)]);
                index++;
            }

            }


            var options = {
                title: 'Fixes Applied',
                animation: {"startup": true, "duration" : 500}, //allows animation to chart
                is3D: true,
                titleTextStyle: { color: 'white' },
                backgroundColor: '#232B38',
                hAxis: {
                    textStyle: { color: 'white' }
                },
                vAxis: {
                    textStyle: { color: 'white' }
                },
                legend: {
                    textStyle: { color: 'white' }
                },
                width: 500,
                height: 300
        

            };

            var chart = new google.visualization.BarChart(document.getElementById('eventsBarChart'));

            chart.draw(data, options);
        }
        function drawBarChartSniper() {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Event');
            data.addColumn('number', 'Count');
            var index = 0;
            while (index !== vm.chartAudits.length) {
                
                //if (vm.chartAudits[index].eventName)
                    data.addRow([vm.chartAudits[index].eventName, parseInt(vm.chartAudits[index].count,10)]);
                    index++;
            }



            var options = {
                title: 'Sniper Events',
                animation: { "startup": true, "duration": 500 }, //allows animation to chart
                is3D: true,
                titleTextStyle: { color: 'white' },
                backgroundColor: '#232B38',
                hAxis: {
                    textStyle: { color: 'white' }
                },
                vAxis: {
                    textStyle: { color: 'white' }
                },
                legend: {
                    textStyle: { color: 'white' }
                },
                width: 500,
                height: 300


            };

            var chart = new google.visualization.BarChart(document.getElementById('usersBarChartSniper'));

            chart.draw(data, options);
        }
        function drawChartPieChart() {
           
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'LanID');
            data.addColumn('number', 'Count');
          
            var index = 0 ;
            while(index !== vm.chartAudits.length)
            {
                data.addRow([vm.chartAudits[index].LanID, parseInt(vm.chartAudits[index].count)]);
                index++; 
            }
           
           

            var options = {
                title: 'Fixes by User',
                animation: { "startup": true, "duration": 500 },

                is3D: true,
                titleTextStyle: { color: 'white' },
                backgroundColor: '#232B38',
                hAxis: {
                    textStyle: { color: 'white' }
                },
                vAxis: {
                    textStyle: { color: 'white' }
                },
                legend: {
                    textStyle: { color: 'white' }
                },
                width: 500,
                height: 300

               
            };

            var chart = new google.visualization.PieChart(document.getElementById('usersPieChart'));

           chart.draw(data, options);
        }
        function drawChartPieChartSniper() {
    
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'LanID');
            data.addColumn('number', 'Count');

            var index = 0;
            while (index !== vm.chartAudits.length) {
                data.addRow([vm.chartAudits[index].LanID, parseInt(vm.chartAudits[index].count, 10)]);
                index++;
            }



            var options = {
                title: 'Sniper Events by User',
                animation: { "startup": true, "duration": 500 },

                is3D: true,
                titleTextStyle: { color: 'white' },
                backgroundColor: '#232B38',
                hAxis: {
                    textStyle: { color: 'white' }
                },
                vAxis: {
                    textStyle: { color: 'white' }
                },
                legend: {
                    textStyle: { color: 'white' }
                },
                width: 500,
                height: 300


            };

            var chart = new google.visualization.PieChart(document.getElementById('usersPieCharSniper'));

            chart.draw(data, options);
        }
        $scope.checkFix = function (audit) {
            try {
                audit.validFix = true; 
                return audit.Fix.Name;
            }
            catch (err) {
                audit.validFix = false;
                return audit.Event;

            }

        };
       
        $(document).ready(function ()
        {
            setTimeout(function ()
            { $('#LanIDInput').trigger('input'); },100
            );      
        });
        activate();
        $scope.systemChange = function (app) {

            $('#systemBtn').text(app.Name);
            $scope.selectedApp = app;
            $('#enviromentBtn').text("Environment");
            $('#activeEnviroment').val("");
            $('#activeSystem').val(app.Name);
            $('#activeSystem').trigger('input');
            $('#activeEnviroment').trigger('input');
            $scope.FIX_ONLY = 0;
            applyFixOnly(); 
            $scope.search; 
        };
        $scope.enviromentChange = function (text) {
            $('#enviromentBtn').text(text);
            $('#activeEnviroment').val(text);
           
            $scope.FIX_ONLY = 0;
            applyFixOnly(); 
            $('#activeEnviroment').trigger('input');
           
            
        } ; 
        function getFixes() {
            return datacontext.getFixes().then(function (data) {
                return vm.fixes = data.results;
            });
        }
        //button to update charts 
        $scope.updateCharts = function () {


            datacontext.chartsIntervalID = setInterval(function () { checkCondition(); }, 10000);
            UpdatePieChartUsers();
            UpdateBarChartEvents();
            UpdatePieChartUsersFix();
            UpdateBarChartEventsSniper();
            //UpdateLineChart(); 


        };
        function activate() {
            //getFixes(),
            var promises = [getAudits() ,getFixes(), getApplications()];
            common.activateController(promises, controllerId)
                .then(function () {
               
                    //needed here to prevent blank system text
                    //executes 100 milliseconds after page load
                    setTimeout(function () {
                        $('#system').text('Audits');
                        $('#system').show();
                        $('.filterModelsAudits').trigger('input');
                        log('Audit Info Loaded');
                    }, 100);
                });
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
        function checkCondition()
        {
            if ($("#auditAnalysis").length) { //check if exists on the page 
                UpdatePieChartUsers();
                UpdateBarChartEvents();
                UpdatePieChartUsersFix();
                UpdateBarChartEventsSniper(); 
            }
            else {
                clearInterval(datacontext.chartsIntervalID); //clear the interval
            }
        }
        //draws pie chart for the fixes of sniper applied 
        function UpdatePieChartUsersFix() {
            vm.chartAudits = [];
            return datacontext.getDifferentAuditDetailsFix().then(function (data) {
                vm.chartAudits = data.results;
                drawChartPieChart();
                return vm.chartAudits;
            });
        }
        //draws pie chart for sniper by users
        function UpdatePieChartUsers() {
            vm.chartAudits = [];
            return datacontext.getDifferentAuditDetails().then(function (data) {
                vm.chartAudits = data.results;
                drawChartPieChartSniper();
                return vm.chartAudits;
            });
        }
        //draws bar chart for events
        function UpdateBarChartEvents() {
            vm.chartAudits = [];
            return datacontext.getDifferentAuditEvents().then(function (data) {
                vm.chartAudits = data.results;
                drawBarChart();
                return vm.chartAudits;
            });
        }
        //draw bar charts on non sniper events ie. Edit View Search
        function UpdateBarChartEventsSniper() {
            vm.chartAudits = [];
            return datacontext.getDifferentAuditEventsSniper().then(function (data) {
                vm.chartAudits = data.results;
                drawBarChartSniper();
                return vm.chartAudits;
            });
        }
        function getAudits() {

            return datacontext.getAudits().then(function (data) {
                vm.allAudits = data.results;
            });
        }
        function getUsers() {
            return datacontext.getUsers().then(function (data) {
                vm.users = data.results;
                return vm.users;
            });
        }
        //Harmony etc..
        function getApplications() {
            return datacontext.getApplications().then(function (data) {
                vm.applications = data.results;
                return vm.applications;
            });
        }
    }
})();
