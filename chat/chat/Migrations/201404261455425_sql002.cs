namespace chat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sql002 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Password", c => c.String());
            AddColumn("dbo.Users", "Ava", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Ava");
            DropColumn("dbo.Users", "Password");
        }
    }
}
