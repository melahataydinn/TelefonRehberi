namespace TelefonRehberi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Kisiler", "FotoYolu");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Kisiler", "FotoYolu", c => c.String());
        }
    }
}
