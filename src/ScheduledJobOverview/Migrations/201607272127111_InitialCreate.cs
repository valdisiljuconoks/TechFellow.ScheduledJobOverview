namespace TechFellow.ScheduledJobOverview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ScheduledJobsStatistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobId = c.Guid(nullable: false),
                        Name = c.String(),
                        DurationInMilliseconds = c.Long(nullable: false),
                        ExecutedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ScheduledJobsStatistics");
        }
    }
}
