namespace bsk2v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Recipecontrollerimplementation : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Users", name: "ControlLevelId", newName: "CleranceLevelId");
            RenameColumn(table: "dbo.Recipes", name: "ControlLevelId", newName: "ClassificationLevelId");
            RenameIndex(table: "dbo.Recipes", name: "IX_ControlLevelId", newName: "IX_ClassificationLevelId");
            RenameIndex(table: "dbo.Users", name: "IX_ControlLevelId", newName: "IX_CleranceLevelId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Users", name: "IX_CleranceLevelId", newName: "IX_ControlLevelId");
            RenameIndex(table: "dbo.Recipes", name: "IX_ClassificationLevelId", newName: "IX_ControlLevelId");
            RenameColumn(table: "dbo.Recipes", name: "ClassificationLevelId", newName: "ControlLevelId");
            RenameColumn(table: "dbo.Users", name: "CleranceLevelId", newName: "ControlLevelId");
        }
    }
}
