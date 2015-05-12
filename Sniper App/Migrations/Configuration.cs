using Sniper_App.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Sniper_App.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Sniper_App.Models.SniperDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SniperDbContext context)
        {
            var databases = new List<Sniper_App.Models.Database>
                {
                    new Sniper_App.Models.Database{ Name="HarmonyAccounts", Server=@"IREL8LZCKV1\SQL2008", Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                    new Sniper_App.Models.Database{ Name="HE_V12_D_Harmony", Server=@"chav-adk11-1", Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                    new Sniper_App.Models.Database{ Name="HE_V13_D_Harmony", Server=@"chav-adk11-1", Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                    new Sniper_App.Models.Database{ Name="V12_L_Harmony", Server=@"chav-adk11-1", Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
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
                    new Sniper_App.Models.AppEnvironment{ Name="HarmonyDevV12", ApplicationID=0, Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                    new Sniper_App.Models.AppEnvironment{ Name="HarmonyDevV13", ApplicationID=0, Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                    new Sniper_App.Models.AppEnvironment{ Name="V12_L_Harmony", ApplicationID=0, Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                    new Sniper_App.Models.AppEnvironment{ Name="Development 1", ApplicationID=1, Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                    new Sniper_App.Models.AppEnvironment{ Name="Development 2", ApplicationID=1, Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                    new Sniper_App.Models.AppEnvironment{ Name="Itest 1", ApplicationID=1, Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                    new Sniper_App.Models.AppEnvironment{ Name="Itest 2", ApplicationID=1, Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                    new Sniper_App.Models.AppEnvironment{ Name="User Acceptance", ApplicationID=1, Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                    new Sniper_App.Models.AppEnvironment{ Name="Production", ApplicationID=1, Active=true, TimeStamp=DateTime.Parse("2005-09-01"), AppEnvironmentDatabases=new List<AppEnvironmentDatabase>() },
                };
            appEnvironments.ForEach(s => context.AppEnvironments.AddOrUpdate(s));
            applications[0].AppEnvironments.Add(appEnvironments[0]);
            applications[0].AppEnvironments.Add(appEnvironments[1]);
            applications[1].AppEnvironments.Add(appEnvironments[2]);
            applications[1].AppEnvironments.Add(appEnvironments[3]);
            applications[1].AppEnvironments.Add(appEnvironments[4]);
            applications[1].AppEnvironments.Add(appEnvironments[5]);
            applications[1].AppEnvironments.Add(appEnvironments[6]);
            applications[1].AppEnvironments.Add(appEnvironments[7]);
            context.SaveChanges();

            /****/
            var appEnvironmentDatabases = new List<AppEnvironmentDatabase>
                {
                    new AppEnvironmentDatabase { AppEnvironmentID=0, DatabaseID=0 },
                    new AppEnvironmentDatabase { AppEnvironmentID=0, DatabaseID=1 },
                    new AppEnvironmentDatabase { AppEnvironmentID=1, DatabaseID=0 },
                    new AppEnvironmentDatabase { AppEnvironmentID=1, DatabaseID=2 },
                    new AppEnvironmentDatabase { AppEnvironmentID=2, DatabaseID=3 },
                    new AppEnvironmentDatabase { AppEnvironmentID=3, DatabaseID=4 },
                    new AppEnvironmentDatabase { AppEnvironmentID=4, DatabaseID=5 },
                    new AppEnvironmentDatabase { AppEnvironmentID=5, DatabaseID=6 },
                    new AppEnvironmentDatabase { AppEnvironmentID=6, DatabaseID=7 },
                    new AppEnvironmentDatabase { AppEnvironmentID=7, DatabaseID=8 },
                };
            appEnvironmentDatabases.ForEach(s => context.AppEnvironmentDatabases.AddOrUpdate(s));
            databases[0].AppEnvironmentDatabases.Add(appEnvironmentDatabases[0]);
            databases[0].AppEnvironmentDatabases.Add(appEnvironmentDatabases[2]);
            databases[1].AppEnvironmentDatabases.Add(appEnvironmentDatabases[1]);
            databases[1].AppEnvironmentDatabases.Add(appEnvironmentDatabases[3]);
            appEnvironments[0].AppEnvironmentDatabases.Add(appEnvironmentDatabases[0]);
            appEnvironments[0].AppEnvironmentDatabases.Add(appEnvironmentDatabases[1]);
            appEnvironments[1].AppEnvironmentDatabases.Add(appEnvironmentDatabases[2]);
            appEnvironments[1].AppEnvironmentDatabases.Add(appEnvironmentDatabases[3]);
            context.SaveChanges();

            var remoteStoredProcedures = new List<RemoteStoredProcedure>
                {
                    new RemoteStoredProcedure{ DatabaseID=3, Name="usp_UDP_UpdateBCN", Active=true, TimeStamp=DateTime.Parse("2005-09-01") },
                    new RemoteStoredProcedure{ DatabaseID=3, Name="usp_UDP_SelectAccountInformation", Active=true, TimeStamp=DateTime.Parse("2005-09-01") }
                };
            remoteStoredProcedures.ForEach(s => context.RemoteStoredProcedures.AddOrUpdate(s));
            context.SaveChanges();

            var columns = new List<Column>
                {
                    new Column{ RemoteStoredProcedureID=0, ColumnName="AccountName", ColumnDisplayName="Account Name", DataType=2, Active=true, TimeStamp=DateTime.Parse("2005-09-01") },
                    new Column{ RemoteStoredProcedureID=1, ColumnName="HarmonyAccountNumber", ColumnDisplayName="Harmony Account Number", DataType=1, Active=true, TimeStamp=DateTime.Parse("2005-09-01") },
                    new Column{ RemoteStoredProcedureID=1, ColumnName="AccountName", ColumnDisplayName="Account Name", DataType=2, Active=true, TimeStamp=DateTime.Parse("2005-09-01") },
                    new Column{ RemoteStoredProcedureID=1, ColumnName="HarmonyAccountNumber", ColumnDisplayName="Harmony Account Number", DataType=1, Active=true, TimeStamp=DateTime.Parse("2005-09-01") }
                };
            columns.ForEach(s => context.Columns.AddOrUpdate(s));
            context.SaveChanges();

            var fixes = new List<Fix>
                {
                    new Fix{ AppEnvironmentID=0, Name="Change the account name", Description="Select an account to edit. Then click the column to edit the value.", SelectProcedureID=1, UpdateProcedureID=0, Active=false, TimeStamp=DateTime.Parse("2005-09-01"), HtmlCode="<span>This fix has not yet Implemented </span>"},
                    new Fix{ AppEnvironmentID=0, Name="Add BCN to Account", Description="Select an account to expand the BCN edit option. Then click the column to edit the value.", SelectProcedureID=1, UpdateProcedureID=0, Active=true, TimeStamp=DateTime.Parse("2005-09-01"), HtmlCode="<div class='input-group col-xs-4'><span class='input-group-addon' style='width:30%'>Account #:</span> <input type='text' class='form-control accountNumber' aria-label='...' disabled ></div><!-- /input-group --><br></div><div class='input-group col-xs-4'><span class='input-group-addon' style='width:30%'>Account Name:</span> <input type='text' class='form-control accountName' aria-label='...' disabled ></div><!-- /input-group --><br><div class='input-group col-xs-4'><span class='input-group-addon' style='width:30%'>BCN</span> <input type='text' class='form-control BCN' aria-label='...'></div><!-- /input-group --></div>"},
                    new Fix{ AppEnvironmentID=0, Name="Deactivate BCN from Account", Description="Select an account to expand the Deactivate BCN from Account edit option. Then click the column to edit the value.", SelectProcedureID=1, UpdateProcedureID=0, Active=true, TimeStamp=DateTime.Parse("2005-09-01"), HtmlCode= "<div class='input-group col-xs-4'><span class='input-group-addon' style='width:30%'>Account #:</span> <input type='text' class='form-control accountNumber' aria-label='...' disabled ></div><!-- /input-group --><br><div class='input-group col-xs-4'><span class='input-group-addon' style='width:30%'>Account Name:</span> <input type='text' class='form-control accountName' aria-label='...' disabled ></div><!-- /input-group --><br><div class='input-group col-xs-4'><span class='input-group-addon' style='width:30%'>Account Name:</span><button id='bcnBtn' class='btn 'style='margin:0px' dropdown-toggle>BCN</button><button class='btn dropdown-toggle' style='margin:0px'><span class='caret'></span></button><ul id='bcnDropdown' class='dropdown-menu'><li><a href='#'>123456</a></li><li><a href='#'>987654</a></li></ul></div>"},
                    new Fix{ AppEnvironmentID=0, Name="Deactivate Account", Description="Select an account to expand the Deactivate Account edit option. Then click the column to edit the value.", SelectProcedureID=1, UpdateProcedureID=0, Active=false, TimeStamp=DateTime.Parse("2005-09-01"), HtmlCode="<span>This fix has not yet Implemented </span>" },
                    new Fix{ AppEnvironmentID=0, Name="Group New Hire Waiting Period", Description="Select an account to expand the Group New Hire Waiting Period edit option. Then click the column to edit the value.", SelectProcedureID=1, UpdateProcedureID=0, Active=false, TimeStamp=DateTime.Parse("2005-09-01"), HtmlCode="<span>This fix has not yet Implemented </span>"  },
                    new Fix{ AppEnvironmentID=0, Name="Group Policy Effective Date", Description="Select an account to expand the Group Policy Effective Date edit option. Then click the column to edit the value.", SelectProcedureID=1, UpdateProcedureID=0, Active=true, TimeStamp=DateTime.Parse("2005-09-01"), HtmlCode="<span>This fix has not yet Implemented </span>" },
                    new Fix{ AppEnvironmentID=0, Name="Group Min Hours Worked", Description="Select an account to expand the Group Min Hours Worked edit option. Then click the column to edit the value.", SelectProcedureID=1, UpdateProcedureID=0, Active=true, TimeStamp=DateTime.Parse("2005-09-01"), HtmlCode="<span>This fix has not yet Implemented </span>" }
                };
            //public ICollection<Fix> RelatedFixes { get; set; }
            fixes.ForEach(s => context.Fixes.AddOrUpdate(s));
            context.SaveChanges();

            //________________________________________________________________________________________________________
            var audit = new List<Audit>
            {
                //new Audit{ UserId="BCF13", System="Harmony", Enviroment="Dev V11", Event="qqq" ,KeyValue="13",FixId_Name="Tom",ValueStart="%F%" ,ValueEnd="", TimeStamp="24/03/2015 07:56:32.000"} ,
                //new Audit{ UserId="BCF14", System="Harmony", Enviroment="Dev V11", Event="Search" ,KeyValue="14",FixId_Name="Jerry",ValueStart="%G%" ,ValueEnd="", TimeStamp=DateTime.Parse("25/03/2015 08:56:32.000")} , 
                //new Audit{ UserId="BCF15", System="Harmony", Enviroment="Dev V11", Event="Search" ,KeyValue="15",FixId_Name="test",ValueStart="%H%" ,ValueEnd="", TimeStamp=DateTime.Parse("26/03/2015 09:56:32.000")} 
            };
            audit.ForEach(s => context.Audits.AddOrUpdate(s));
            context.SaveChanges();
            //________________________________________________________________________________________________________

            //var harmonyAccounts = new List<HarmonyAccount>
            //    {
            //        new HarmonyAccount{ AccountName="0052A Don", HarmonyAccountNumber="F5120001", AccountId="f985a089-6004-44d4-b7ea-be6595fe3db0" },
            //        new HarmonyAccount{ AccountName="10.0.8.1 Workflow Demo 1", HarmonyAccountNumber="F3023777", AccountId="f985a089-6004-44d4-b7ea-be6595fe3db1" },
            //        new HarmonyAccount{ AccountName="10.0.8.1 Workflow Demo 2", HarmonyAccountNumber="E3023793", AccountId="f985a089-6004-44d4-b7ea-be6595fe3db2" },
            //        new HarmonyAccount{ AccountName="Automation Test Account 1", HarmonyAccountNumber="X1505338", AccountId="f985a089-6004-44d4-b7ea-be6595fe3db3" },
            //        new HarmonyAccount{ AccountName="Automation Test Account 2", HarmonyAccountNumber="X5140801", AccountId="f985a089-6004-44d4-b7ea-be6595fe3db4" },
            //        new HarmonyAccount{ AccountName="Ri Testing", HarmonyAccountNumber="F8855447", AccountId="f985a089-6004-44d4-b7ea-be6595fe3db5" },
            //        new HarmonyAccount{ AccountName="0052A DonA", HarmonyAccountNumber="F5120002", AccountId="f985a089-6004-44d4-b7ea-be6595fe3db2" },
            //        new HarmonyAccount{ AccountName="10.0.8.1 Workflow Demo 3", HarmonyAccountNumber="F3023778", AccountId="f985a089-6004-44d4-b7ea-be6595fe3db3" },
            //        new HarmonyAccount{ AccountName="10.0.8.1 Workflow Demo 4", HarmonyAccountNumber="E3023794", AccountId="f985a089-6004-44d4-b7ea-be6595fe3db4" },
            //        new HarmonyAccount{ AccountName="Automation Test Account 3", HarmonyAccountNumber="X1505339", AccountId="f985a089-6004-44d4-b7ea-be6595fe3db5" },
            //        new HarmonyAccount{ AccountName="Automation Test Account 4", HarmonyAccountNumber="X5140802", AccountId="f985a089-6004-44d4-b7ea-be6595fe3db6" },
            //        new HarmonyAccount{ AccountName="Ri TestingA", HarmonyAccountNumber="F8855448", AccountId="f985a089-6004-44d4-b7ea-be6595fe3db7" },
            //        new HarmonyAccount{ AccountName="0052A DonB", HarmonyAccountNumber="F5120003", AccountId="f985a089-6004-44d4-b7ea-be6595fe3db3" },
            //        new HarmonyAccount{ AccountName="10.0.8.1 Workflow Demo 5", HarmonyAccountNumber="F3023779", AccountId="f985a089-6004-44d4-b7ea-be6595fe3db4" },
            //        new HarmonyAccount{ AccountName="10.0.8.1 Workflow Demo 6", HarmonyAccountNumber="E3023795", AccountId="f985a089-6004-44d4-b7ea-be6595fe3db5" },
            //        new HarmonyAccount{ AccountName="Automation Test Account 5", HarmonyAccountNumber="X1505341", AccountId="f985a089-6004-44d4-b7ea-be6595fe3db6" },
            //        new HarmonyAccount{ AccountName="Automation Test Account 6", HarmonyAccountNumber="X5140803", AccountId="f985a089-6004-44d4-b7ea-be6595fe3db7" },
            //        new HarmonyAccount{ AccountName="Ri TestingB", HarmonyAccountNumber="F8855449", AccountId="f985a089-6004-44d4-b7ea-be6595fe3db8" }
            //    };
            //harmonyAccounts.ForEach(s => context.HarmonyAccounts.AddOrUpdate(s));
            //context.SaveChanges();

            var users = new List<User>
                {
                    new User{ LanID="afc08", FirstName="Eamonn", LastName="Lawler", UserType=1, Active=true, },// TimeStamp=DateTime.Parse("2005-09-01") },
                    new User{ LanID="ADK11", FirstName="Patrick", LastName="Killalea", UserType=1, Active=true, },// TimeStamp=DateTime.Parse("2005-09-01") },
                    new User{ LanID="ADI13", FirstName="Rebecca", LastName="Curran", UserType=1, Active=true, },// TimeStamp=DateTime.Parse("2005-09-01") },
                    new User{ LanID="BCF13", FirstName="Seamus", LastName="Cunningham", UserType=1, Active=true, },// TimeStamp=DateTime.Parse("2005-09-01") }
                    new User{ LanID="AHP15", FirstName="Con", LastName="Kennedy", UserType=1, Active=true, },// TimeStamp=DateTime.Parse("2005-09-01") }
                    new User{ LanID="AHU15", FirstName="Matthew", LastName="Gerig", UserType=1, Active=true, },// TimeStamp=DateTime.Parse("2005-09-01") }
                    new User{ LanID="AHV15", FirstName="Thomas", LastName="Woodhouse", UserType=1, Active=true, },// TimeStamp=DateTime.Parse("2005-09-01") }
                };
            //public ICollection<Permission> Permissions { get; set; }
            users.ForEach(s => context.Users.AddOrUpdate(s));
            context.SaveChanges();

            var groups = new List<Group>
                {
                    new Group{ GroupName="Harmony Group", Active=true, TimeStamp=DateTime.Parse("2005-09-01") },
                    new Group{ GroupName="Enrollment Group", Active=true, TimeStamp=DateTime.Parse("2005-09-01") },
                    new Group{ GroupName="Census", Active=true, TimeStamp=DateTime.Parse("2005-09-01") },
                    new Group{ GroupName="All Permissions", Active=true, TimeStamp=DateTime.Parse("2005-09-01") }
                };
            //public ICollection<Permission> Permissions { get; set; }
            groups.ForEach(s => context.Groups.AddOrUpdate(s));
            context.SaveChanges();

            var userGroups = new List<UserGroup>
                {
                    new UserGroup { LanID="ADK11", GroupID=0 },
                    new UserGroup { LanID="ADI13", GroupID=0 },
                    new UserGroup { LanID="BCF13", GroupID=0 },
                    new UserGroup { LanID="AHP15", GroupID=0 },
                    new UserGroup { LanID="AHU15", GroupID=0 },
                    new UserGroup { LanID="AHV15", GroupID=0 },
                };
            userGroups.ForEach(s => context.UserGroups.AddOrUpdate(s));
            //users[0].UserGroups.Add(userGroups[0]);
            //groups[0].UserGroups.Add(userGroups[0]);
            context.SaveChanges();

            var permissions = new List<Permission>
                {
                    new Permission{ LanID="afc08", AppEnvironmentID=2 , Active=true, TimeStamp=DateTime.Parse("2005-09-01") },
                    new Permission{ LanID="afc08", FixID=0 , Active=true, TimeStamp=DateTime.Parse("2005-09-01") },

                    new Permission{ GroupID=0, AppEnvironmentID=2 , Active=true, TimeStamp=DateTime.Parse("2005-09-01") },
                    new Permission{ GroupID=0, FixID=0 , Active=true, TimeStamp=DateTime.Parse("2005-09-01") },
                };
            permissions.ForEach(s => context.Permissions.AddOrUpdate(s));
            context.SaveChanges();
        }
    }
}