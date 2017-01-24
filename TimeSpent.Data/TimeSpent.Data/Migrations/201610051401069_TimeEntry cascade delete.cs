namespace TimeSpent.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimeEntrycascadedelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TimeEntry", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.TimeEntry", "ProjectId", "dbo.Project");
            AddForeignKey("dbo.TimeEntry", "CategoryId", "dbo.Category", "CategoryId");
            AddForeignKey("dbo.TimeEntry", "ProjectId", "dbo.Project", "ProjectId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeEntry", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.TimeEntry", "CategoryId", "dbo.Category");
            AddForeignKey("dbo.TimeEntry", "ProjectId", "dbo.Project", "ProjectId", cascadeDelete: true);
            AddForeignKey("dbo.TimeEntry", "CategoryId", "dbo.Category", "CategoryId", cascadeDelete: true);
        }
    }
}
