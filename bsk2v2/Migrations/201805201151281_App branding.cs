namespace bsk2v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Appbranding : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Documents", newName: "Recipes");
            DropForeignKey("dbo.Reports", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.Reports", "ControlLevelId", "dbo.ControlLevels");
            DropForeignKey("dbo.ReportDocuments", "Report_Id", "dbo.Reports");
            DropForeignKey("dbo.ReportDocuments", "Document_Id", "dbo.Documents");
            DropIndex("dbo.Reports", new[] { "AuthorId" });
            DropIndex("dbo.Reports", new[] { "ControlLevelId" });
            DropIndex("dbo.ReportDocuments", new[] { "Report_Id" });
            DropIndex("dbo.ReportDocuments", new[] { "Document_Id" });
            DropTable("dbo.Reports");
            DropTable("dbo.ReportDocuments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ReportDocuments",
                c => new
                    {
                        Report_Id = c.Int(nullable: false),
                        Document_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Report_Id, t.Document_Id });
            
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Desription = c.String(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        ControlLevelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.ReportDocuments", "Document_Id");
            CreateIndex("dbo.ReportDocuments", "Report_Id");
            CreateIndex("dbo.Reports", "ControlLevelId");
            CreateIndex("dbo.Reports", "AuthorId");
            AddForeignKey("dbo.ReportDocuments", "Document_Id", "dbo.Documents", "Id");
            AddForeignKey("dbo.ReportDocuments", "Report_Id", "dbo.Reports", "Id");
            AddForeignKey("dbo.Reports", "ControlLevelId", "dbo.ControlLevels", "Id");
            AddForeignKey("dbo.Reports", "AuthorId", "dbo.Users", "Id");
            RenameTable(name: "dbo.Recipes", newName: "Documents");
        }
    }
}
