namespace Street_Vendors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageTypeadding : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ImageType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ImageType");
        }
    }
}
