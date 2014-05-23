namespace chat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sql003 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Ava", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Ava", c => c.Binary());
        }
    }
}
