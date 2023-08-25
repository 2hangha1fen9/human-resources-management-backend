namespace HumanResourcesManagementBackend.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class departmentname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.R_Department", "DepartmentName", c => c.String());
            DropColumn("dbo.R_Department", "PositionName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.R_Department", "PositionName", c => c.String());
            DropColumn("dbo.R_Department", "DepartmentName");
        }
    }
}
