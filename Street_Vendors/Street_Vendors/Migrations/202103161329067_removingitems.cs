namespace Street_Vendors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removingitems : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.pros", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.pros", new[] { "UserId" });
            DropTable("dbo.pros");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.pros",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                        ItemDescription = c.String(),
                        ItemPrice = c.Int(nullable: false),
                        ItemImage = c.String(),
                        Rating = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.pros", "UserId");
            AddForeignKey("dbo.pros", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
