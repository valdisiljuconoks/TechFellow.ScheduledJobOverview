using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using TechFellow.ScheduledJobOverview.Migrations;

namespace TechFellow.ScheduledJobOverview
{
    public class ScheduledJobsStatisticsContext : DbContext
    {
        public ScheduledJobsStatisticsContext() : base("EPiServerDB")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ScheduledJobsStatisticsContext, Configuration>());
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            Database.Initialize(false);
        }

        public DbSet<ScheduledJobsStatisticsEntry> ScheduledJobsStatistics { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ScheduledJobsStatisticsEntry>().ToTable("ScheduledJobsStatistics");
            modelBuilder.Entity<ScheduledJobsStatisticsEntry>()
                        .Property(c => c.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }

    public class ScheduledJobsStatisticsEntry
    {
        [Key]
        public int Id { get; set; }

        public Guid JobId { get; set; }

        public string Name { get; set; }

        public long DurationInMilliseconds { get; set; }

        public DateTime ExecutedAt { get; set; }
    }
}
