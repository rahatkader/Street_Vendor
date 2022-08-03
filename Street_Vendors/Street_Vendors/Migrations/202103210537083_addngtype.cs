namespace Street_Vendors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addngtype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Type");
        }
    }
}
