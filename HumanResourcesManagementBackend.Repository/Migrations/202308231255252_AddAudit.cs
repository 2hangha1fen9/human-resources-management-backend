namespace HumanResourcesManagementBackend.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAudit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.R_AbsenceApply", "AuditResult", c => c.String());
            AddColumn("dbo.R_BusinessTripApply", "AuditResult", c => c.String());
            AddColumn("dbo.R_CompensatoryApply", "AuditResult", c => c.String());
            AddColumn("dbo.R_FieldWorkApply", "AuditResult", c => c.String());
            AddColumn("dbo.R_VacationApply", "AuditResult", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.R_VacationApply", "AuditResult");
            DropColumn("dbo.R_FieldWorkApply", "AuditResult");
            DropColumn("dbo.R_CompensatoryApply", "AuditResult");
            DropColumn("dbo.R_BusinessTripApply", "AuditResult");
            DropColumn("dbo.R_AbsenceApply", "AuditResult");
        }
    }
}
