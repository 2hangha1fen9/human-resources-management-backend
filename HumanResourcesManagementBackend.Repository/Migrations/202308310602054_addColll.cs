namespace HumanResourcesManagementBackend.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColll : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.R_AbsenceApply", "AuditNodeJson", c => c.String());
            AddColumn("dbo.R_BusinessTripApply", "AuditNodeJson", c => c.String());
            AddColumn("dbo.R_CompensatoryApply", "AuditNodeJson", c => c.String());
            AddColumn("dbo.R_FieldWorkApply", "AuditNodeJson", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.R_FieldWorkApply", "AuditNodeJson");
            DropColumn("dbo.R_CompensatoryApply", "AuditNodeJson");
            DropColumn("dbo.R_BusinessTripApply", "AuditNodeJson");
            DropColumn("dbo.R_AbsenceApply", "AuditNodeJson");
        }
    }
}
