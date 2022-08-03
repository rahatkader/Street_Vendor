namespace Street_Vendors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialingMap : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Maps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Lat = c.Double(nullable: false),
                        Lng = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Maps");
        }
    }
}
