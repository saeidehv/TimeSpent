namespace TimeSpent.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class entitymilescolumn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TimeEntry", "Miles", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TimeEntry", "Miles", c => c.Single());
        }
    }
}
