namespace WebDataExtraction.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RestaurentDatas", "Zipcode", c => c.String());
            DropColumn("dbo.RestaurentDatas", "Zip");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RestaurentDatas", "Zip", c => c.String());
            DropColumn("dbo.RestaurentDatas", "Zipcode");
        }
    }
}
