namespace TelefonRehberi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Kisiler", "Foto");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Kisiler", "Foto", c => c.Binary());
        }
    }
}
