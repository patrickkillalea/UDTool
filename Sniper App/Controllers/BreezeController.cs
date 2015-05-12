using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Script.Services;
using System.Web.Services;
using Breeze.ContextProvider;
using Breeze.ContextProvider.EF6;
using Newtonsoft.Json.Linq;
using Sniper_App.Models;
using System.Data.SqlClient;

namespace Sniper_App.Controllers
{
    public class BreezeController : ApiController
    {
        private SniperDbContext db = new SniperDbContext();
        private const int TIMEOUT = 30;
        private HarmonyDAL harmonyDB = new HarmonyDAL();
        private SniperDAL sniperDB = new SniperDAL();

        readonly EFContextProvider<SniperDbContext> _contextProvider = new EFContextProvider<SniperDbContext>();

        ICollection<Sniper_App.Models.Database> databases = new List<Sniper_App.Models.Database>();

        [HttpGet]
        public string Metadata()
        {
            return _contextProvider.Metadata();
        }

        [HttpPost]
        public SaveResult SaveChanges(JObject saveBundle)
        {
            return _contextProvider.SaveChanges(saveBundle);
        }

        #region AppEnvironment

        [HttpGet]
        public IQueryable<Sniper_App.Models.AppEnvironment> GetAppEnvironments()
        {
            return _contextProvider.Context.AppEnvironments.Include(appEnv => appEnv.AppEnvironmentDatabases).ToList().AsQueryable();
        }

        #endregion AppEnvironment

        #region AppEnvironmentDatabase

        [HttpGet]
        public IQueryable<Sniper_App.Models.AppEnvironmentDatabase> GetAppEnvironmentDatabases()
        {
            return _contextProvider.Context.AppEnvironmentDatabases;
        }

        #endregion AppEnvironmentDatabase

        #region Application

        [HttpGet]
        public IQueryable<Application> GetApplications()
        {
            return _contextProvider.Context.Applications.Include(app => app.AppEnvironments).ToList().AsQueryable();
        }

        #endregion Application

        #region Audit

        [HttpGet]
        public IQueryable<Audit> GetAudits()
        {
            return _contextProvider.Context.Audits.Include(audit => audit.AppEnvironment)
                                                  .Include(audit => audit.Application)
                                                  .Include(audit => audit.Fix)
                                                  .Include(audit => audit.User)
                                                  .ToList().AsQueryable();
        }

        [HttpGet]
        public List<Audit> GetAuditsCriteria(int procedure, DateTime searchDate, DateTime endDate, string lanId, string eventType, string fixtype)
        {
            List<Audit> audits = new List<Audit>();
            audits = sniperDB.GetAuditsCriteria(procedure, searchDate, endDate, lanId, eventType, fixtype);

            return audits;
        }
        //Used with charts
        [HttpGet]
        public List<AuditUsers> GetAuditsUsers()
        {
            List<AuditUsers> result = new List<AuditUsers>();
            result = sniperDB.GetAuditsUsers();

            return result;
        }
        [HttpGet]
        public List<AuditEvents> GetAuditsEvents()
        {
            List<AuditEvents> result = new List<AuditEvents>();
            result = sniperDB.GetAuditsEvents();
            return result;
        }
        [HttpGet]
        public List<AuditEvents> GetAuditsEventsSniper()
        {
            List<AuditEvents> result = new List<AuditEvents>();
            result = sniperDB.GetAuditsEventsSniper();
            return result;
        }
        [HttpGet]
        public List<AuditUsers> GetAuditsUsersFixes()
        {
            List<AuditUsers> result = new List<AuditUsers>();
            result = sniperDB.GetAuditsUsersFixes();
            return result;
        }
        [HttpGet]
        public void RecordSearchAudits(string userID, string system, string enviroment, string eventType, string keyval, int FixId, string valstart, string valend)
        {
            sniperDB.RecordSearchAudits(userID, system, enviroment, eventType, keyval, FixId, valstart, valend);
        }

        [HttpGet]
        public string GetAuditUserLookup()//IQueryable<Sniper_App.Models.AppEnvironmentDatabase>
        {
            var users = _contextProvider.Context.Users;
            var audits = _contextProvider.Context.Audits;
            return null;
        }

        #endregion Audit

        #region Column

        [HttpGet]
        public IQueryable<Column> GetColumns()
        {
            return _contextProvider.Context.Columns.Include(column => column.RemoteStoredProcedure).ToList().AsQueryable();
        }

        #endregion Column

        #region Database

        [HttpGet]
        public IQueryable<Sniper_App.Models.Database> GetDatabases()
        {
            return _contextProvider.Context.Databases.Include(db => db.AppEnvironmentDatabases).ToList().AsQueryable();
        }

        [HttpGet]
        public void AddDatabase(string name, string server, bool active)
        {
            sniperDB.AddDatabase(name, server, active);
        }

        [HttpGet]
        public void EditDatabase(string dbID, string name, string server, bool active)
        {
            sniperDB.EditDatabase(dbID, name, server, active);
        }

        [HttpGet]
        public void RemoveDatabase(string dbID)
        {
            sniperDB.RemoveDatabase(dbID);
        }

        #endregion Database

        #region Fix

        [HttpGet]
        public IQueryable<Fix> GetFixes()
        {
            return _contextProvider.Context.Fixes.Include(fix => fix.AppEnvironment)
                                                 .Include(fix => fix.RelatedFixes)
                                                 .Include(fix => fix.SelectProcedure)
                                                 .Include(fix => fix.UpdateProcedure)
                                                 .ToList().AsQueryable();
        }

        [HttpGet]
        public IQueryable<Fix> GetAppEnvironmentFixes(int appEnvironmentID)
        {
            return _contextProvider.Context.Fixes.Where(fix => fix.AppEnvironmentID == appEnvironmentID)
                                                 .Include(fix => fix.AppEnvironment)
                                                 .Include(fix => fix.RelatedFixes)
                                                 .Include(fix => fix.SelectProcedure)
                                                 .Include(fix => fix.UpdateProcedure)
                                                 .ToList().AsQueryable();
        }

        #endregion Fix

        #region Group

        [HttpGet]
        public IQueryable<Group> GetGroups()
        {
            return _contextProvider.Context.Groups.Include(group => group.Permissions)
                                                  .Include(group => group.UserGroups)
                                                  .ToList().AsQueryable();
        }

        #endregion Group

        #region Permission

        [HttpGet]
        public IQueryable<Permission> GetPermissions()
        {
            return _contextProvider.Context.Permissions//.Include(permission => permission.AppEnvironment)
                                                       //.Include(permission => permission.Fix)
                                                       //.Include(permission => permission.Group)
                                                       //.Include(permission => permission.User)
                                                       .ToList().AsQueryable();
        }

        [HttpGet]
        public IQueryable<Permission> GetActivePermissions(int applicationID)
        {
            return _contextProvider.Context.Permissions.Where(permissions => permissions.Active == true);
        }

        #endregion Permission

        #region RemoteStoredProcedure

        [HttpGet]
        public IQueryable<RemoteStoredProcedure> GetRemoteStoredProcedures()
        {
            return _contextProvider.Context.RemoteStoredProcedures.Include(rsp => rsp.Columns)
                                                                  .Include(rsp => rsp.Database)
                                                                  .ToList().AsQueryable();
        }

        #endregion RemoteStoredProcedure

        #region User

        [HttpGet]
        public IQueryable<Sniper_App.Models.User> GetUsers()
        {
            return _contextProvider.Context.Users.Include(user => user.Permissions)
                                                 .Include(user => user.UserGroups)
                                                 .ToList().AsQueryable();
        }

        [HttpGet]
        public List<User> getUserInfo()
        {
            List<User> users = new List<User>();
            users = sniperDB.getUserInfo();

            return users;
        }

        [HttpGet]
        public void addUser(String LanID, String FirstName, String LastName, int UserType/*,int Group_GroupID*/)
        {
            sniperDB.addUser(LanID, FirstName, LastName, UserType /*,Group_GroupID*/);
        }

        [HttpGet]
        public void updateUser(String LanID, String FirstName, String LastName, int UserType /*,int Group_GroupID*/, bool Active)
        {
            sniperDB.updateUser(LanID, FirstName, LastName, UserType /*,Group_GroupID*/, Active);
        }

        //[HttpGet]
        //public User getUserDetails()
        //{
        //    string currentDomain = Domain.GetComputerDomain().Name;
        //    User currentUser = null;

        //    currentUser = new User();

        //    currentUser.LanID = HttpContext.Current.Request.LogonUserIdentity.Name;
        //    currentUser.FirstName = GetUserDisplayName(HttpContext.Current.Request.LogonUserIdentity.Name);
        //    currentUser.LanID = currentUser.LanID.Substring(3);

        //    return currentUser;
        //}

        //public string GetUserDisplayName(string lanId)
        //{
        //    string displayName = null;

        //    using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "UP"))
        //    {
        //        UserPrincipal user = System.DirectoryServices.AccountManagement.UserPrincipal.FindByIdentity(ctx, lanId);
        //        displayName = (user != null) ? user.DisplayName : string.Empty;
        //    }
        //    return displayName;
        //}

        #endregion User

        #region UserGroup

        [HttpGet]
        public IQueryable<Sniper_App.Models.UserGroup> GetUserGroups()
        {
            return _contextProvider.Context.UserGroups;
        }

        #endregion UserGroup



        [HttpPost]
        public SaveResult AddAppEnvironmentDatabase(JObject saveBundle)
        {
            return _contextProvider.SaveChanges(saveBundle);
        }

        #region Harmony

        [HttpGet]
        public List<HarmonyAccount> getHarmonyAccounts(string option, string criteria)
        {
            List<HarmonyAccount> accounts = new List<HarmonyAccount>();
            accounts = harmonyDB.getHarmonyAccounts(option, criteria);

            return accounts;
        }

        [HttpGet]
        public List<Bcn> getBcnNumbers(string account)
        {
            List<Bcn> BcnNumbers = new List<Bcn>();
            BcnNumbers = harmonyDB.getBcnNumbers(account);

            return BcnNumbers;
        }

        [HttpGet]
        public void AddBCNtoAccount(string accountID, string BCN)
        {
            harmonyDB.AddBCNtoAccount(accountID, BCN);
        }

        [HttpGet]
        public void removeBCNToAccount(string BcnId)
        {
            harmonyDB.removeBCNToAccount(BcnId);
        }

        #endregion

        #region Sniper

        [HttpGet]
        public string CheckStoredProcedures(string database, string storedProc)
        {
            return sniperDB.CheckStoredProcedures(database, storedProc);
        }

        #endregion
    }
}
