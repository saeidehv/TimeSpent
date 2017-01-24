namespace TimeSpent.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class entitytablechanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeEntry", "Duration", c => c.Single(nullable: false));
            CreateIndex("dbo.TimeEntry", "ProjectId");
            CreateIndex("dbo.TimeEntry", "CategoryId");
            AddForeignKey("dbo.TimeEntry", "CategoryId", "dbo.Category", "CategoryId", cascadeDelete: true);
            AddForeignKey("dbo.TimeEntry", "ProjectId", "dbo.Project", "ProjectId", cascadeDelete: true);
            DropColumn("dbo.TimeEntry", "Hours");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TimeEntry", "Hours", c => c.Single(nullable: false));
            DropForeignKey("dbo.TimeEntry", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.TimeEntry", "CategoryId", "dbo.Category");
            DropIndex("dbo.TimeEntry", new[] { "CategoryId" });
            DropIndex("dbo.TimeEntry", new[] { "ProjectId" });
            DropColumn("dbo.TimeEntry", "Duration");
        }
    }
}
