namespace WebDataExtraction.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RestaurentDatas",
                c => new
                    {
                        RestaurentId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Address = c.String(),
                        Zip = c.String(),
                        SearchDataId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RestaurentId)
                .ForeignKey("dbo.SearchDatas", t => t.SearchDataId, cascadeDelete: true)
                .Index(t => t.SearchDataId);
            
            CreateTable(
                "dbo.SearchDatas",
                c => new
                    {
                        SearchDataId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.SearchDataId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RestaurentDatas", "SearchDataId", "dbo.SearchDatas");
            DropIndex("dbo.RestaurentDatas", new[] { "SearchDataId" });
            DropTable("dbo.SearchDatas");
            DropTable("dbo.RestaurentDatas");
        }
    }
}
