namespace bsk2v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesconnectedtoRecipeslistview : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Recipes", name: "UserId", newName: "AuthorId");
            RenameIndex(table: "dbo.Recipes", name: "IX_UserId", newName: "IX_AuthorId");
            AddColumn("dbo.Recipes", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recipes", "Name");
            RenameIndex(table: "dbo.Recipes", name: "IX_AuthorId", newName: "IX_UserId");
            RenameColumn(table: "dbo.Recipes", name: "AuthorId", newName: "UserId");
        }
    }
}
