namespace Street_Vendors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class spellincheck : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ImagePath", c => c.String());
            DropColumn("dbo.AspNetUsers", "ImageType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "ImageType", c => c.String());
            DropColumn("dbo.AspNetUsers", "ImagePath");
        }
    }
}
