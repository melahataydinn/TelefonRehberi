namespace TelefonRehberi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kisiler", "Website", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Kisiler", "Website");
        }
    }
}
