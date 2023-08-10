namespace TelefonRehberi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kisiler", "Foto", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Kisiler", "Foto");
        }
    }
}
