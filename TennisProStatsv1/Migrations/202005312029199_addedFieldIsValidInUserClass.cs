namespace TennisProStatsv1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedFieldIsValidInUserClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsValid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsValid");
        }
    }
}
