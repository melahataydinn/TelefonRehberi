namespace TelefonRehberi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kisiler", "FotoYolu", c => c.String());
            DropColumn("dbo.Kisiler", "Foto");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Kisiler", "Foto", c => c.Binary());
            DropColumn("dbo.Kisiler", "FotoYolu");
        }
    }
}
