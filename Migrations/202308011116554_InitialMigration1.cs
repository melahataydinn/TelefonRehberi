namespace TelefonRehberi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kisiler", "FotoYolu", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Kisiler", "FotoYolu");
        }
    }
}
