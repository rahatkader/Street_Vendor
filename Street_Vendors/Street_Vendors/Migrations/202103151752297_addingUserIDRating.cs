namespace Street_Vendors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingUserIDRating : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Items", name: "ApplicationUser_Id", newName: "UserId");
            RenameIndex(table: "dbo.Items", name: "IX_ApplicationUser_Id", newName: "IX_UserId");
            AddColumn("dbo.Items", "Rating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "Rating");
            RenameIndex(table: "dbo.Items", name: "IX_UserId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Items", name: "UserId", newName: "ApplicationUser_Id");
        }
    }
}
