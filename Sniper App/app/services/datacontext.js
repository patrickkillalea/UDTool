(function () {
    'use strict';

    var serviceId = 'datacontext';
    angular.module('app').factory(serviceId, ['common', 'breeze', datacontext]);

    function datacontext(common, breeze) {
        var $q = common.$q;
        var serviceName = 'breeze/Breeze';
        var manager = new breeze.EntityManager(serviceName);
        var userID = '';
        var userName = '';
        var chartsIntervalID;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            //used for audits google charts

        var service = {
            getHarmonyAccounts: getHarmonyAccounts,
            getModules: getModules,
            getFixes: getFixes,
            getAppEnvironmentFixes: getAppEnvironmentFixes,
            getApplications: getApplications,
            getDatabases: getDatabases,
            getuser: getuser,
            userID: userID,
            getBcn: getBcn,
            getGroup: getGroup,
            getUser: getUser ,
            getUsers: getUsers,
            userName: userName,
            addBCNToAccount: addBCNToAccount,
            addUser: addUser,
            updateUser: updateUser,
            removeBCNToAccount: removeBCNToAccount,
            recordAuditSearch: recordAuditSearch,
            getAuditsCriteria: getAuditsCriteria,
            checkForSProc: checkForSProc,
            updateAccountNameByKey: updateAccountNameByKey,
            getAudits: getAudits,
            getDifferentAuditEvents: getDifferentAuditEvents,
            getAuditsTimesOfDay: getAuditsTimesOfDay,
            getDifferentAuditDetails: getDifferentAuditDetails,
            getDifferentAuditDetailsFix: getDifferentAuditDetailsFix,
            addApplication: addApplication,
            addAppEnvironments: addAppEnvironments,
            addDatabase: addDatabase,
            editDatabase: editDatabase,
            removeDatabase: removeDatabase,
            chartsIntervalID: chartsIntervalID,
            setConnection: setConnection,
            GetAppEnvironments: GetAppEnvironments,
            getDifferentAuditEventsSniper: getDifferentAuditEventsSniper
        };

        return service;

        function getHarmonyAccounts(option, search) {
            var query = breeze.EntityQuery
                .from('getHarmonyAccounts')
                .withParameters({ option: option, criteria: search });

            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                return $q.reject(error); // so downstream promise users know it failed
            }
        }


        function getBcn(account) {

            var query = breeze.EntityQuery
                .from("getBcnNumbers")
                .withParameters({ account: account });
            // .where('fNumber', '==', account);


            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                return $q.reject(error); // so downstream promise users know it failed
            }
        }

        function getGroup() {
            var query = breeze.EntityQuery.from("getGroups");

            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                return $q.reject(error); // so downstream promise users know it failed
            }
        }

        function getUser() {
            var query = breeze.EntityQuery.from("getUserInfo");

            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                return $q.reject(error); // so downstream promise users know it failed
            }
        }


        function getModules() {
            var modules = [
                { Text: 'Harmony', image: 'http://localhost:56302/Content/images/greencross.gif' }
                /*
                { Text: 'iServices', image: 'http://localhost:56302/Content/images/greencross.gif' },
                { Text: 'CCP', image: 'http://localhost:56302/Content/images/greencross.gif' },
                { Text: 'Admin', image: 'http://localhost:56302/Content/images/adminGreen_white.png' }
                */
            ];
            return $q.when(modules);
        }

        function getFixes() {
            var query = breeze.EntityQuery
                .from("GetFixes");


            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                return $q.reject(error); // so downstream promise users know it failed
            }
        } 

        function getAppEnvironmentFixes(appEnvironmentId) {
            var query = breeze.EntityQuery
                .from("GetAppEnvironmentFixes")
                .withParameters({ appEnvironmentID: appEnvironmentId });

            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                return $q.reject(error); // so downstream promise users know it failed
            }
        }

        function getApplications() {
            var query = breeze.EntityQuery
                .from("GetApplications")
                .orderBy("Name");

            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                return $q.reject(error); // so downstream promise users know it failed
            }
        }

        function getDatabases() {
            var query = breeze.EntityQuery
                .from("GetDatabases")
                .orderBy("Name");

            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                return $q.reject(error); // so downstream promise users know it failed
            }
        }

        function getUsers() {
            var query = breeze.EntityQuery.from("getUserInfo");

            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                return $q.reject(error); // so downstream promise users know it failed
            }
        }

        function getuser() {
            var query = breeze.EntityQuery.from("getUserDetails");
            var promise = manager.executeQuery(query).catch(queryFailed);

            return promise;

            function queryFailed(error) {
                return $q.reject(error); // so downstream promise users know it failed
            }
        }

        function addBCNToAccount(account, BCN) {
            var query = breeze.EntityQuery
                .from("AddBCNtoAccount")
                  .withParameters({ accountID: account, BCN: BCN });

            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                return $q.reject(error); // so downstream promise users know it failed
            }
        }

        function setConnection(local) {
            var query = breeze.EntityQuery
                .from("setConnectionString")
                  .withParameters({ local: local });

            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                return $q.reject(error); // so downstream promise users know it failed
            }
        }

        function updateUser(LanID, FirstName, LastName, UserType/*,Group_GroupID*/, Active) {
            var query = breeze.EntityQuery
                .from("updateUser")
                .withParameters({ LanID: LanID, FirstName: FirstName, LastName: LastName, UserType: UserType /*,Group_GroupID: Group_GroupID*/,Active: Active });

            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                return $q.reject(error); // so downstream promise users know it failed
            }
        }

        function addUser(LanID, FirstName, LastName, UserType, Group_GroupID) {
            var query = breeze.EntityQuery
                .from("addUser")
                .withParameters({ LanID: LanID, FirstName: FirstName, LastName: LastName, UserType: UserType});
//,Group_GroupID: Group_GroupID gone from above
            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                return $q.reject(error); // so downstream promise users know it failed
            }
        }

        //function addUser(entity) {
        //    var database = manager.createEntity('User', entity);
        //    return manager.saveChanges()
        //        .then(function (data) {
        //            //alert('saveSucceeded');
        //            return $q.when(database);
        //        })
        //        .catch(function (error) {
        //            $q.reject(error);
        //        })
        //}



        function removeBCNToAccount(BcnId) {
            var query = breeze.EntityQuery
                .from("removeBCNtoAccount")
                  .withParameters({ BcnId: BcnId });

            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                return $q.reject(error); // so downstream promise users know it failed
            }
        }



        //           public string RecordSearchAudits(string userID, string system, string enviroment,string eventType, string keyval, int FixId, string valstart , string valend)

        function updateAccountNameByKey(accountName, accountKey) {
            var query = breeze.EntityQuery
                .from("UpdateAccountNameByKey")
                  .withParameters({ accountName: accountName, accountKey: accountKey });

            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                return $q.reject(error); // so downstream promise users know it failed
            }
        }

        function recordAuditSearch(userID, system, enviroment, eventType, keyval, fixId, valstart, valend) {
            var query = breeze.EntityQuery
                .from("RecordSearchAudits")
                  .withParameters({
                      userID: userID,
                      system: system,
                      enviroment: enviroment,
                      eventType: eventType,
                      keyval: keyval,
                      FixId: fixId,
                      valstart: valstart,
                      valend: valend

                  });

            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                return $q.reject(error); // so downstream promise users know it failed
            }
        }

        function checkForSProc(database, storedProc) {
            var query = breeze.EntityQuery
                .from("CheckStoredProcedures")
                  .withParameters({ database: database, storedProc: storedProc });

            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                return $q.reject(error); // so downstream promise users know it failed
            }
        }
        function getAudits() {
            var query = breeze.EntityQuery
               .from("GetAudits")
               .orderBy("AuditID");

            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                return $q.reject(error); // so downstream promise users know it failed
            }
        }
        function getAuditsCriteria(procedure, searchDate, endDate, lanId, eventType, fixtype) {
            var query = breeze.EntityQuery
               .from("GetAuditsCriteria").withParameters({
                   procedure: procedure,
                   searchDate: searchDate,
                   endDate: endDate,
                   lanId: lanId,
                   eventType: eventType,
                   fixtype: fixtype
               });


            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                return $q.reject(error); // so downstream promise users know it failed
            }
        }
        function getDifferentAuditDetails() {
            var query = breeze.EntityQuery
              .from("GetAuditsUsers");
            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                return $q.reject(error);
            }
        }

        function addDatabase(entity) {
            var database = manager.createEntity('Database', entity);
            return manager.saveChanges()
                .then(function (data) {
                    //alert('saveSucceeded');
                    return $q.when(database);
                })
                .catch(function (data) {
                    alert('saveFailed');
                })
        }

        function addApplication(entity) {
            var application = manager.createEntity('Application', entity);
            return manager.saveChanges()
                .then(function (data) {
                    //alert('saveSucceeded');
                    return $q.when(application);
                })
                .catch(function (data) {
                    alert('saveFailed');
                })
        }

        function addAppEnvironments(entityArray, appID) {
            manager.clear();
            $(entityArray).each(function (index, value) {
                value.ApplicationID = appID;
                addAppEnvironment(value);
            });

            return manager.saveChanges()
                .then(function (data) {
                    //alert('saveSucceeded'); 
                })
                .catch(function (data) { 
                })
        }

        function addAppEnvironment(entity) {
            var appEnvironment = manager.createEntity('AppEnvironment', entity);
            addAppEnvironmentDatabases(entity.Databases, appEnvironment.AppEnvironmentID);
        }
        function getDifferentAuditDetailsFix() {
            var query = breeze.EntityQuery
              .from("GetAuditsUsersFixes");
            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                return $q.reject(error);
            }
        }

        function addAppEnvironmentDatabases(entityArray, appEnvID) {
            $(entityArray).each(function (index, value) {
                var appEnvDBLink = { 'AppEnvironmentID': '', 'DatabaseID': '' };
                appEnvDBLink.AppEnvironmentID = appEnvID;
                appEnvDBLink.DatabaseID = value.DatabaseID;
                addAppEnviornmentDatabase(appEnvDBLink);
            });
        }

        function addAppEnviornmentDatabase(entity) {
            var appEnvironmentDatabase = manager.createEntity('AppEnvironmentDatabase', entity);
        }

        /*
        function addDatabase(name, server, active) {
            var query = breeze.EntityQuery.from("AddDatabase")
            .withParameters({
                name: name,
                server: server,
                active: active
            });

            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                return $q.reject(error);
            }
        }*/

        function editDatabase(dbID, name, server, active) {
            var query = breeze.EntityQuery.from("EditDatabase")
            .withParameters({
                dbId: dbID,
                name: name,
                server: server,
                active: active
            });

            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                return $q.reject(error);
            }
        }

        function removeDatabase(dbID) {
            var query = breeze.EntityQuery.from("RemoveDatabase")
            .withParameters({
                dbId: dbID
            });

            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                return $q.reject(error);
            }
        }

        function getDifferentAuditEvents() {
            var query = breeze.EntityQuery
              .from("GetAuditsEvents");
            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                return $q.reject(error); // so downstream promise users know it failed
            }
        }
        function getDifferentAuditEventsSniper() {
            var query = breeze.EntityQuery
              .from("GetAuditsEventsSniper");
            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                return $q.reject(error); // so downstream promise users know it failed
            }
        }
        function getAuditsTimesOfDay() {
            var query = breeze.EntityQuery
              .from("GetAuditEventDates");
            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                return $q.reject(error); // so downstream promise users know it failed
            }
        }
        function GetAppEnvironments() {
            var query = breeze.EntityQuery
              .from("GetAppEnvironments");
            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                return $q.reject(error); // so downstream promise users know it failed
            }
        }
    }
})();