namespace HumanResourcesManagementBackend.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.R_VacationApply", "AuditNodeJson", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.R_VacationApply", "AuditNodeJson");
        }
    }
}
