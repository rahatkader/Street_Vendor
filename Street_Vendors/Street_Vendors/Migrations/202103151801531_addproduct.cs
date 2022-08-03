namespace Street_Vendors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addproduct : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Items", newName: "pros");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.pros", newName: "Items");
        }
    }
}
