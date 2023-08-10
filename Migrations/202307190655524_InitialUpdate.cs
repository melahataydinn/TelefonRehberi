namespace TelefonRehberi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Kisis", "Numara", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Kisis", "Numara", c => c.Int(nullable: false));
        }
    }
}
