namespace TelefonRehberi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TabloIsimleriGunncelleme : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Kisis", newName: "Kisiler");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Kisiler", newName: "Kisis");
        }
    }
}
