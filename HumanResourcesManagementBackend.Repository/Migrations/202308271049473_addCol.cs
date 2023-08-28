namespace HumanResourcesManagementBackend.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.R_Role", "IsDefault", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.R_Role", "IsDefault");
        }
    }
}
