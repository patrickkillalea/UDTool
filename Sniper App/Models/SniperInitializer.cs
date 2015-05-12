using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;
using System.IO;

namespace Sniper_App.Models
{
    public class SniperInitializer : System.Data.Entity.DropCreateDatabaseAlways<SniperDbContext>
   // public class SniperInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<SniperDbContext>
    {
        protected override void Seed(SniperDbContext context)
        {

            //audit procs
            #region auditProcedures
            string auditProcs  = "CREATE PROCEDURE [dbo].[usp_UDT_SelectAuditEvents] As SELECT  Event, count(*) as 'TimesUsed'  From   Audit   Group by Event";
            context.Database.ExecuteSqlCommand(auditProcs);
            auditProcs = "CREATE PROCEDURE [dbo].[usp_UDT_SelectAuditEventsSniper] As SELECT  Event,  count(*) as 'TimesUsed'  From  Audit where FixID is null Group by Event ";
            context.Database.ExecuteSqlCommand(auditProcs);
            auditProcs = "CREATE PROCEDURE [dbo].[usp_UDT_SelectDifferentAuditsUsers] As Begin Select LanID,  count(*) as 'TimesUsed'  From   Audit	where FixID is null Group by LanID End ";
            context.Database.ExecuteSqlCommand(auditProcs);
            auditProcs = "CREATE PROCEDURE [dbo].[usp_UDT_SelectDifferentAuditsUsersFix] As Begin   Select LanID, count(*) as 'TimesUsed'   From Audit where [FixId] is not null  Group by LanID End";
            context.Database.ExecuteSqlCommand(auditProcs);
            auditProcs = "CREATE PROCEDURE [dbo].[usp_UDT_SelectAuditGroupDate] As Begin select CAST(TimeStamp as Date) as 'Date',	count(*) as 'count'	from audit 	group by   CAST(TimeStamp as Date)  end";
            context.Database.ExecuteSqlCommand(auditProcs);
            #endregion auditProcedures

            #region userProcedures
            auditProcs = "CREATE PROCEDURE [dbo].[usp_UDT_SelectUsers] As Begin Select * From [User] End";
            context.Database.ExecuteSqlCommand(auditProcs);
            auditProcs = "CREATE PROCEDURE [dbo].[usp_UDT_AddUser] ( @LanID NVARCHAR(MAX),@FirstName NVARCHAR(MAX),@LastName NVARCHAR(MAX),@UserType BIT,@Active BIT ) AS SET NOCOUNT ON; INSERT INTO [User] (LanID,FirstName,LastName,UserType,Active,[TimeStamp]) VALUES ( @LanID,@FirstName,@LastName,@UserType,@Active,getdate() )";
            context.Database.ExecuteSqlCommand(auditProcs);
            auditProcs = "CREATE PROCEDURE [dbo].[usp_UDT_UpdateUser] @LanID [nvarchar](128),@FirstName [nvarchar](max),@LastName [nvarchar](max),@UserType [int],@Active [bit] AS BEGIN UPDATE [dbo].[User] SET [FirstName] = @FirstName, [LastName] = @LastName, [UserType] = @UserType, [Active] = @Active, [TimeStamp] = GETDATE() WHERE ([LanID] = @LanID) END";
            context.Database.ExecuteSqlCommand(auditProcs);
            #endregion
            var databases = new List<Sniper_App.Models.Database>
                {
                    new Sniper_App.Models.Database{ Name="HarmonyAccounts", Server=@"IREL8LZCKV1\SQL2008", Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                    new Sniper_App.Models.Database{ Name="HE_V12_D_Harmony", Server=@"chav-adk11-1", Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                    new Sniper_App.Models.Database{ Name="HE_V13_D_Harmony", Server=@"chav-adk11-1", Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                    new Sniper_App.Models.Database{ Name="V12_L_Harmony", Server=@"chav-adk11-1", Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                    new Sniper_App.Models.Database{ Name="HE_V13_P_Harmony", Server=@"chav-adk11-1", Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                    new Sniper_App.Models.Database{ Name="DIRPD1", Server=@"CHADSQLNNH100\HAS2K8N", Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                    new Sniper_App.Models.Database{ Name="DIRPD2", Server=@"CHADSQLNNH100\HAS2K8N", Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                    new Sniper_App.Models.Database{ Name="DIRPI1", Server=@"CHADSQLNNH100\HAS2K8N", Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                    new Sniper_App.Models.Database{ Name="DIRPI2", Server=@"CHADSQLNNH100\HAS2K8N", Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                    new Sniper_App.Models.Database{ Name="DIRPA", Server=@"HA2813\S2K8N", Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                    new Sniper_App.Models.Database{ Name="DIRPP", Server=@"HA2832\S2K8N", Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                };
            databases.ForEach(s => context.Databases.AddOrUpdate(s));
            context.SaveChanges();

            var applications = new List<Application>
                {
                    new Application { Name="Harmony", Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironments=new List<AppEnvironment>() },
                    new Application { Name="IRP", Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironments=new List<AppEnvironment>() },
                    new Application { Name="Enrollment", Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironments=new List<AppEnvironment>() },
                };
            applications.ForEach(s => context.Applications.AddOrUpdate(s));
            context.SaveChanges();

            var appEnvironments = new List<Sniper_App.Models.AppEnvironment>
                {
                    new Sniper_App.Models.AppEnvironment{ Name="HarmonyDevV12", ApplicationID=1, Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                    new Sniper_App.Models.AppEnvironment{ Name="HarmonyDevV13", ApplicationID=1, Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                    new Sniper_App.Models.AppEnvironment{ Name="V12_L_Harmony", ApplicationID=1, Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                    new Sniper_App.Models.AppEnvironment{ Name="HarmonyProdV13", ApplicationID=1, Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                    new Sniper_App.Models.AppEnvironment{ Name="Development 1", ApplicationID=2, Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                    new Sniper_App.Models.AppEnvironment{ Name="Development 2", ApplicationID=2, Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                    new Sniper_App.Models.AppEnvironment{ Name="Itest 1", ApplicationID=2, Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                    new Sniper_App.Models.AppEnvironment{ Name="Itest 2", ApplicationID=2, Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                    new Sniper_App.Models.AppEnvironment{ Name="User Acceptance", ApplicationID=2, Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                    new Sniper_App.Models.AppEnvironment{ Name="Production", ApplicationID=2, Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                };
            appEnvironments.ForEach(s => context.AppEnvironments.AddOrUpdate(s));
            context.SaveChanges();

            /****/
            var appEnvironmentDatabases = new List<AppEnvironmentDatabase>
                {
                    new AppEnvironmentDatabase { AppEnvironmentID=1, DatabaseID=3 },
                    new AppEnvironmentDatabase { AppEnvironmentID=1, DatabaseID=2 },

                    new AppEnvironmentDatabase { AppEnvironmentID=2, DatabaseID=1 },
                    new AppEnvironmentDatabase { AppEnvironmentID=2, DatabaseID=2 },

                    new AppEnvironmentDatabase { AppEnvironmentID=3, DatabaseID=4 },
                    new AppEnvironmentDatabase { AppEnvironmentID=3, DatabaseID=2 },

                    new AppEnvironmentDatabase { AppEnvironmentID=4, DatabaseID=5 },
                    new AppEnvironmentDatabase { AppEnvironmentID=4, DatabaseID=2 },

                    new AppEnvironmentDatabase { AppEnvironmentID=5, DatabaseID=7 },

                    new AppEnvironmentDatabase { AppEnvironmentID=6, DatabaseID=6 },

                    new AppEnvironmentDatabase { AppEnvironmentID=7, DatabaseID=8 },

                    new AppEnvironmentDatabase { AppEnvironmentID=8, DatabaseID=9 },

                    new AppEnvironmentDatabase { AppEnvironmentID=9, DatabaseID=10 },

                    new AppEnvironmentDatabase { AppEnvironmentID=10, DatabaseID=11 },
                };
            appEnvironmentDatabases.ForEach(s => context.AppEnvironmentDatabases.AddOrUpdate(s));
            context.SaveChanges();

            var remoteStoredProcedures = new List<RemoteStoredProcedure>
                {
                    new RemoteStoredProcedure{ DatabaseID=4, Name="usp_UDP_UpdateAccountName", Active=true, TimeStamp=DateTime.Parse("2005-09-01") },
                    new RemoteStoredProcedure{ DatabaseID=4, Name="usp_UDP_UpdateAddBCN", Active=true, TimeStamp=DateTime.Parse("2005-09-01") },
                    new RemoteStoredProcedure{ DatabaseID=4, Name="usp_UDP_UpdateDeactivateBCN", Active=true, TimeStamp=DateTime.Parse("2005-09-01") },
                    new RemoteStoredProcedure{ DatabaseID=4, Name="usp_UDT_GetHarmonyAccounts", Active=true, TimeStamp=DateTime.Parse("2005-09-01") },
                };
            remoteStoredProcedures.ForEach(s => context.RemoteStoredProcedures.AddOrUpdate(s));
            context.SaveChanges();

            var columns = new List<Column>
                {
                    new Column{ RemoteStoredProcedureID=4, ColumnName="Account_Name", ColumnDisplayName="Account Name", DataType=1, Active=true, TimeStamp=DateTime.Parse("2005-09-01") },
                    new Column{ RemoteStoredProcedureID=4, ColumnName="Colonial_Employer_Number", ColumnDisplayName="Harmony Account Number", DataType=1, Active=true, TimeStamp=DateTime.Parse("2005-09-01") },
                    new Column{ RemoteStoredProcedureID=4, ColumnName="Account_ID", ColumnDisplayName="Account Id", DataType=1, Active=true, TimeStamp=DateTime.Parse("2005-09-01") },
                    new Column{ RemoteStoredProcedureID=1, ColumnName="BCN", ColumnDisplayName="BCN", DataType=1, Active=true, TimeStamp=DateTime.Parse("2005-09-01") },
                    new Column{ RemoteStoredProcedureID=2, ColumnName="AccountName", ColumnDisplayName="Account Name", DataType=1, Active=true, TimeStamp=DateTime.Parse("2005-09-01") },
                    new Column{ RemoteStoredProcedureID=3, ColumnName="BCN", ColumnDisplayName="BCN", DataType=1, Active=true, TimeStamp=DateTime.Parse("2005-09-01") },
                };
            columns.ForEach(s => context.Columns.AddOrUpdate(s));
            context.SaveChanges();

            var fixes = new List<Fix>
                {
                    new Fix{ AppEnvironmentID=3, Name="Change the account name", Description="Select an account to edit. Then click the column to edit the value.", SelectProcedureID=2, UpdateProcedureID=1, Active=false, TimeStamp=DateTime.Parse("2005-09-01"), HtmlCode="<span>This fix has not yet Implemented </span>"},
                    new Fix{ AppEnvironmentID=3, Name="Add BCN to Account", Description="Select an account to expand the BCN edit option. Then click the column to edit the value.", SelectProcedureID=2, UpdateProcedureID=1, Active=true, TimeStamp=DateTime.Parse("2005-09-01"), HtmlCode="<div class='input-group col-xs-4'><span class='input-group-addon' style='width:30%'>Account #:</span> <input type='text' class='form-control accountNumber' aria-label='...' disabled ></div><!-- /input-group --><br></div><div class='input-group col-xs-4'><span class='input-group-addon' style='width:30%'>Account Name:</span> <input type='text' class='form-control accountName' aria-label='...' disabled ></div><!-- /input-group --><br><div class='input-group col-xs-4'><span class='input-group-addon' style='width:30%'>BCN</span> <input type='text' class='form-control BCN' aria-label='...'></div><!-- /input-group --></div>"},
                    new Fix{ AppEnvironmentID=3, Name="Deactivate BCN from Account", Description="Select an account to expand the Deactivate BCN from Account edit option. Then click the column to edit the value.", SelectProcedureID=2, UpdateProcedureID=1, Active=true, TimeStamp=DateTime.Parse("2005-09-01"), HtmlCode= "<div class='input-group col-xs-4'><span class='input-group-addon' style='width:30%'>Account #:</span> <input type='text' class='form-control accountNumber' aria-label='...' disabled ></div><!-- /input-group --><br><div class='input-group col-xs-4'><span class='input-group-addon' style='width:30%'>Account Name:</span> <input type='text' class='form-control accountName' aria-label='...' disabled ></div><!-- /input-group --><br><div class='input-group col-xs-4'><span class='input-group-addon' style='width:30%'>Account Name:</span><button id='bcnBtn' class='btn 'style='margin:0px' dropdown-toggle>BCN</button><button class='btn dropdown-toggle' style='margin:0px'><span class='caret'></span></button><ul id='bcnDropdown' class='dropdown-menu'><li><a href='#'>123456</a></li><li><a href='#'>987654</a></li></ul></div>"},
                    new Fix{ AppEnvironmentID=3, Name="Deactivate Account", Description="Select an account to expand the Deactivate Account edit option. Then click the column to edit the value.", SelectProcedureID=2, UpdateProcedureID=1, Active=false, TimeStamp=DateTime.Parse("2005-09-01"), HtmlCode="<span>This fix has not yet Implemented </span>" },
                    new Fix{ AppEnvironmentID=3, Name="Group New Hire Waiting Period", Description="Select an account to expand the Group New Hire Waiting Period edit option. Then click the column to edit the value.", SelectProcedureID=2, UpdateProcedureID=1, Active=false, TimeStamp=DateTime.Parse("2005-09-01"), HtmlCode="<span>This fix has not yet Implemented </span>"  },
                    new Fix{ AppEnvironmentID=3, Name="Group Policy Effective Date", Description="Select an account to expand the Group Policy Effective Date edit option. Then click the column to edit the value.", SelectProcedureID=2, UpdateProcedureID=1, Active=true, TimeStamp=DateTime.Parse("2005-09-01"), HtmlCode="<span>This fix has not yet Implemented </span>" },
                    new Fix{ AppEnvironmentID=3, Name="Group Min Hours Worked", Description="Select an account to expand the Group Min Hours Worked edit option. Then click the column to edit the value.", SelectProcedureID=2, UpdateProcedureID=1, Active=true, TimeStamp=DateTime.Parse("2005-09-01"), HtmlCode="<span>This fix has not yet Implemented </span>" }
                };
            fixes.ForEach(s => context.Fixes.AddOrUpdate(s));
            context.SaveChanges();

            var users = new List<User>
                {
                    new User{ LanID="AFC08", FirstName="Eamonn", LastName="Lawler", UserType=1, Active=true, TimeStamp=DateTime.Now },// TimeStamp=DateTime.Parse("2005-09-01") },
                    new User{ LanID="ADK11", FirstName="Patrick", LastName="Killalea", UserType=1, Active=true, TimeStamp=DateTime.Now },// TimeStamp=DateTime.Parse("2005-09-01") },
                    new User{ LanID="ADI13", FirstName="Rebecca", LastName="Curran", UserType=1, Active=true, TimeStamp=DateTime.Now },// TimeStamp=DateTime.Parse("2005-09-01") },
                    new User{ LanID="BCF13", FirstName="Seamus", LastName="Cunningham", UserType=1, Active=true, TimeStamp=DateTime.Now },// TimeStamp=DateTime.Parse("2005-09-01") }
                    new User{ LanID="AHP15", FirstName="Con", LastName="Kennedy", UserType=1, Active=true, TimeStamp=DateTime.Now },// TimeStamp=DateTime.Parse("2005-09-01") }
                    new User{ LanID="AHU15", FirstName="Matthew", LastName="Gerig", UserType=1, Active=true, TimeStamp=DateTime.Now },// TimeStamp=DateTime.Parse("2005-09-01") }
                    new User{ LanID="AHV15", FirstName="Thomas", LastName="Woodhouse", UserType=1, Active=true, TimeStamp=DateTime.Now },// TimeStamp=DateTime.Parse("2005-09-01") }
                };
            users.ForEach(s => context.Users.AddOrUpdate(s));
            context.SaveChanges();

            var audit = new List<Audit>
            {
                new Audit{ LanID="AHU15", ApplicationID=1, AppEnvironmentID=3, Event="Search", KeyValue="14", FixID=null, ValueStart="%G%" ,ValueEnd="", TimeStamp=DateTime.Parse("25/03/2015 08:56:32.000") }, 
                new Audit{ LanID="AHU15", ApplicationID=1, AppEnvironmentID=1, Event="Search", KeyValue="15", FixID=null, ValueStart="%H%" ,ValueEnd="", TimeStamp=DateTime.Parse("26/03/2015 09:56:32.000") }, 
                new Audit{ LanID="AHV15", ApplicationID=1, AppEnvironmentID=3, Event="Add BCN", KeyValue="14", FixID=2, ValueStart="%G%" ,ValueEnd="421452245", TimeStamp=DateTime.Parse("25/03/2015 08:56:32.000") }, 
                new Audit{ LanID="AHV15", ApplicationID=1, AppEnvironmentID=3, Event="Remove BCN", KeyValue="14", FixID=3, ValueStart="421452245" ,ValueEnd="", TimeStamp=DateTime.Parse("25/03/2015 08:56:32.000") }, 
                new Audit{ LanID="AHV15", ApplicationID=1, AppEnvironmentID=1, Event="Edit Account", KeyValue="AccountID", FixID=null, ValueStart="FHAS216423748" ,ValueEnd="", TimeStamp=DateTime.Parse("02/05/2015 08:56:32.000") }, 
                new Audit{ LanID="AHP15", ApplicationID=1, AppEnvironmentID=3, Event="Search", KeyValue="14", FixID=null, ValueStart="%G%" ,ValueEnd="", TimeStamp=DateTime.Parse("03/05/2015 08:56:32.000") }, 
                new Audit{ LanID="AHP15", ApplicationID=1, AppEnvironmentID=2, Event="Add BCN", KeyValue="15", FixID=2,ValueStart="%G%" ,ValueEnd="421452245", TimeStamp=DateTime.Parse("04/05/2015 09:56:32.000") }, 
                new Audit{ LanID="AHP15", ApplicationID=1, AppEnvironmentID=2, Event="Add BCN", KeyValue="14", FixID=2, ValueStart="%G%" ,ValueEnd="421452245", TimeStamp=DateTime.Now }, 
                new Audit{ LanID="AHU15", ApplicationID=1, AppEnvironmentID=3, Event="Remove BCN", KeyValue="14", FixID=3, ValueStart="421452245" ,ValueEnd="", TimeStamp=DateTime.Now }, 
                new Audit{ LanID="AHP15", ApplicationID=1, AppEnvironmentID=1, Event="Add BCN", KeyValue="AccountID", FixID=2, ValueStart="%G%" ,ValueEnd="421452245", TimeStamp=DateTime.Now }, 

            };
            audit.ForEach(s => context.Audits.AddOrUpdate(s));
            context.SaveChanges();

            var groups = new List<Group>
                {
                    new Group{ GroupName="Harmony Group", Active=true, TimeStamp=DateTime.Parse("2005-09-01") },
                    new Group{ GroupName="Enrollment Group", Active=true, TimeStamp=DateTime.Parse("2005-09-01") },
                    new Group{ GroupName="Census", Active=true, TimeStamp=DateTime.Parse("2005-09-01") },
                    new Group{ GroupName="All Permissions", Active=true, TimeStamp=DateTime.Parse("2005-09-01") },
                };
            groups.ForEach(s => context.Groups.AddOrUpdate(s));
            context.SaveChanges();

            var userGroups = new List<UserGroup>
                {
                    new UserGroup { LanID="ADK11", GroupID=2 },
                    new UserGroup { LanID="ADI13", GroupID=2 },
                    new UserGroup { LanID="BCF13", GroupID=2 },
                    new UserGroup { LanID="AHP15", GroupID=2 },
                    new UserGroup { LanID="AHU15", GroupID=2 },
                    new UserGroup { LanID="AHV15", GroupID=2 },
                };
            userGroups.ForEach(s => context.UserGroups.AddOrUpdate(s));
            context.SaveChanges();

            var permissions = new List<Permission>
                {
                    new Permission{ LanID="AFC08", AppEnvironmentID=3 , Active=true, TimeStamp=DateTime.Parse("2005-09-01") },
                    new Permission{ LanID="AFC08", FixID=2 , Active=true, TimeStamp=DateTime.Parse("2005-09-01") },
                    new Permission{ LanID="AFC08", FixID=3 , Active=true, TimeStamp=DateTime.Parse("2005-09-01") },

                    new Permission{ GroupID=2, AppEnvironmentID=3 , Active=true, TimeStamp=DateTime.Parse("2005-09-01") },
                    new Permission{ GroupID=2, FixID=2 , Active=true, TimeStamp=DateTime.Parse("2005-09-01") },
                    new Permission{ GroupID=2, FixID=3 , Active=true, TimeStamp=DateTime.Parse("2005-09-01") },
                };
            permissions.ForEach(s => context.Permissions.AddOrUpdate(s));
            context.SaveChanges();

            var baseDir = AppDomain.CurrentDomain.BaseDirectory + "\\Migrations";
            // Comment out this line if compiling outside of the Unum network - otherwise it'll throw an error pulling LAN Ids.
            //context.Database.ExecuteSqlCommand(File.ReadAllText(baseDir + "\\CreateUserRoles.sql"));
        }
    }
}