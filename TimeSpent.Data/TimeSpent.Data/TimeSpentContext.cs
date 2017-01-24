using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Runtime.Serialization;
using TimeSpent.Business.Entities;
using TimeSpent.Core.Contracts;
using System.ComponentModel;

namespace TimeSpent.Data
{
    public class TimeSpentContext : DbContext

    {
        public TimeSpentContext() : base("name=TimeSpent")
        {
            Database.SetInitializer<TimeSpentContext>(null);
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<TimeEntry> TimeEntrySet { get; set; }

        public DbSet<Category> CategorySet { get; set; }
        
        public DbSet <Project> ProjectSet { get; set; }

        public DbSet<Account> AccountSet { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Disable pluralization of the tables name
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToOneConstraintIntroductionConvention>();
            
            modelBuilder.Ignore<PropertyChangedEventHandler>();

            // ignore the Extensiondata and IIdentifiableEntity in ObjectBase
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();

            // set the primary key for every table
            modelBuilder.Entity<TimeEntry>().HasKey<int>(e => e.TimeEntryId).Ignore(e=>e.EntityId);
            modelBuilder.Entity<Project>().HasKey<int>(e => e.ProjectId).Ignore(e=>e.EntityId);
            modelBuilder.Entity<Category>().HasKey<int>(e => e.CategoryId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Account>().HasKey<int>(e => e.AccountId).Ignore(e => e.EntityId);
        }
    }
}
