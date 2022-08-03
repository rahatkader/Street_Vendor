namespace Street_Vendors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mapfore : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Maps", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Maps", "UserId");
            AddForeignKey("dbo.Maps", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Maps", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Maps", new[] { "UserId" });
            DropColumn("dbo.Maps", "UserId");
        }
    }
}
