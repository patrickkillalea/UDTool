using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Web.Http;

namespace Sniper_App.Models
{
    public class SniperDbContext : DbContext
    {
        public SniperDbContext()
            : base(nameOrConnectionString: "Sniper") { }

        static SniperDbContext()
        {
            System.Data.Entity.Database.SetInitializer<SniperDbContext>(null);    // Regular DB access
            //System.Data.Entity.Database.SetInitializer<SniperDbContext>(new SniperInitializer()); // This one drops and creates the DB
            //System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<SniperDbContext, Sniper_App.Migrations.Configuration>()); // This will perform automatic updates.
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Use singular table names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //Configuration.ProxyCreationEnabled = false;
            //Configuration.LazyLoadingEnabled = false;

            modelBuilder.Entity<AppEnvironment>().MapToStoredProcedures(s =>
            {
                s.Update(u => u.HasName("usp_UDT_UpdateAppEnvironment"));
                s.Delete(d => d.HasName("usp_UDT_DeleteAppEnvironment"));
                s.Insert(i => i.HasName("usp_UDT_InsertAppEnvironment"));
            });
            modelBuilder.Entity<AppEnvironmentDatabase>().MapToStoredProcedures(s =>
            {
                s.Update(u => u.HasName("usp_UDT_UpdateAppEnvironmentDatabase"));
                s.Delete(d => d.HasName("usp_UDT_DeleteAppEnvironmentDatabase"));
                s.Insert(i => i.HasName("usp_UDT_InsertAppEnvironmentDatabase"));
            });
            modelBuilder.Entity<Application>().MapToStoredProcedures(s =>
            {
                s.Update(u => u.HasName("usp_UDT_UpdateApplication"));
                s.Delete(d => d.HasName("usp_UDT_DeleteApplication"));
                s.Insert(i => i.HasName("usp_UDT_InsertApplication"));
            });
            modelBuilder.Entity<Audit>().MapToStoredProcedures(s =>
            {
                s.Update(u => u.HasName("usp_UDT_UpdateAudit"));
                s.Delete(d => d.HasName("usp_UDT_DeleteAudit"));
                s.Insert(i => i.HasName("usp_UDT_InsertAudit"));
            });
            modelBuilder.Entity<Column>().MapToStoredProcedures(s =>
            {
                s.Update(u => u.HasName("usp_UDT_UpdateColumn"));
                s.Delete(d => d.HasName("usp_UDT_DeleteColumn"));
                s.Insert(i => i.HasName("usp_UDT_InsertColumn"));
            });
            modelBuilder.Entity<Database>().MapToStoredProcedures(s =>
            {
                s.Update(u => u.HasName("usp_UDT_UpdateDatabase"));
                s.Delete(d => d.HasName("usp_UDT_DeleteDatabase"));
                s.Insert(i => i.HasName("usp_UDT_InsertDatabase"));
            });
            modelBuilder.Entity<Fix>().MapToStoredProcedures(s =>
            {
                s.Update(u => u.HasName("usp_UDT_UpdateFix"));
                s.Delete(d => d.HasName("usp_UDT_DeleteFix"));
                s.Insert(i => i.HasName("usp_UDT_InsertFix"));
            });
            modelBuilder.Entity<Group>().MapToStoredProcedures(s =>
            {
                s.Update(u => u.HasName("usp_UDT_UpdateGroup"));
                s.Delete(d => d.HasName("usp_UDT_DeleteGroup"));
                s.Insert(i => i.HasName("usp_UDT_InsertGroup"));
            });
            modelBuilder.Entity<Permission>().MapToStoredProcedures(s =>
            {
                s.Update(u => u.HasName("usp_UDT_UpdatePermission"));
                s.Delete(d => d.HasName("usp_UDT_DeletePermission"));
                s.Insert(i => i.HasName("usp_UDT_InsertPermission"));
            });
            modelBuilder.Entity<RemoteStoredProcedure>().MapToStoredProcedures(s =>
            {
                s.Update(u => u.HasName("usp_UDT_UpdateRemoteStoredProcedure"));
                s.Delete(d => d.HasName("usp_UDT_DeleteRemoteStoredProcedure"));
                s.Insert(i => i.HasName("usp_UDT_InsertRemoteStoredProcedure"));
            });
            modelBuilder.Entity<User>().MapToStoredProcedures(s =>
            {
               // s.Update(u => u.HasName("usp_UDT_UpdateUser"));
                s.Delete(d => d.HasName("usp_UDT_DeleteUser"));
                s.Insert(i => i.HasName("usp_UDT_InsertUser"));
            });
            modelBuilder.Entity<UserGroup>().MapToStoredProcedures(s =>
            {
                s.Update(u => u.HasName("usp_UDT_UpdateUserGroup"));
                s.Delete(d => d.HasName("usp_UDT_DeleteUserGroup"));
                s.Insert(i => i.HasName("usp_UDT_InsertUserGroup"));
            });
        }

        public DbSet<AppEnvironment> AppEnvironments { get; set; }
        public DbSet<AppEnvironmentDatabase> AppEnvironmentDatabases { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Audit> Audits { get; set; }
        public DbSet<Column> Columns { get; set; }
        public DbSet<Database> Databases { get; set; }
        public DbSet<Fix> Fixes { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RemoteStoredProcedure> RemoteStoredProcedures { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }

        public User User { get; set; }
    }
}