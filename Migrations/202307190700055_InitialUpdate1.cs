namespace TelefonRehberi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialUpdate1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Kisis", "Numara", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Kisis", "Numara", c => c.Long(nullable: false));
        }
    }
}
