namespace HumanResourcesManagementBackend.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modify : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AbsenceApplies", newName: "R_AbsenceApply");
            RenameTable(name: "dbo.BusinessTripApplies", newName: "R_BusinessTripApply");
            RenameTable(name: "dbo.CompensatoryApplies", newName: "R_CompensatoryApply");
            RenameTable(name: "dbo.FieldWorkApplies", newName: "R_FieldWorkApply");
            RenameTable(name: "dbo.PermissionRoleRefs", newName: "R_PermissionRoleRef");
            RenameTable(name: "dbo.Permissions", newName: "R_Permission");
            RenameTable(name: "dbo.Roles", newName: "R_Role");
            RenameTable(name: "dbo.UserRoleRefs", newName: "R_UserRoleRef");
            RenameTable(name: "dbo.Users", newName: "R_User");
            RenameTable(name: "dbo.VacationApplies", newName: "R_VacationApply");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.R_VacationApply", newName: "VacationApplies");
            RenameTable(name: "dbo.R_User", newName: "Users");
            RenameTable(name: "dbo.R_UserRoleRef", newName: "UserRoleRefs");
            RenameTable(name: "dbo.R_Role", newName: "Roles");
            RenameTable(name: "dbo.R_Permission", newName: "Permissions");
            RenameTable(name: "dbo.R_PermissionRoleRef", newName: "PermissionRoleRefs");
            RenameTable(name: "dbo.R_FieldWorkApply", newName: "FieldWorkApplies");
            RenameTable(name: "dbo.R_CompensatoryApply", newName: "CompensatoryApplies");
            RenameTable(name: "dbo.R_BusinessTripApply", newName: "BusinessTripApplies");
            RenameTable(name: "dbo.R_AbsenceApply", newName: "AbsenceApplies");
        }
    }
}
