namespace TimeSpent.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Project", "Code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Project", "Code", c => c.String());
        }
    }
}
