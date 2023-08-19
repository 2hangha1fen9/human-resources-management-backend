namespace HumanResourcesManagementBackend.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Roles", "User_Id", "dbo.Users");
            DropIndex("dbo.Roles", new[] { "User_Id" });
            DropPrimaryKey("dbo.Roles");
            DropPrimaryKey("dbo.Users");
            CreateTable(
                "dbo.AbsenceApplies",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EmployeeId = c.Long(nullable: false),
                        AbsenceDateTime = c.DateTime(nullable: false),
                        CheckInType = c.Int(nullable: false),
                        Reason = c.String(),
                        Prover = c.String(),
                        AuditStatus = c.Int(nullable: false),
                        AuditType = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BusinessTripApplies",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EmployeeId = c.Long(nullable: false),
                        Address = c.String(),
                        Reason = c.String(),
                        Result = c.String(),
                        Support = c.String(),
                        BeginDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        AuditStatus = c.Int(nullable: false),
                        AuditType = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CompensatoryApplies",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EmployeeId = c.Long(nullable: false),
                        WorkDate = c.DateTime(nullable: false),
                        RestDate = c.DateTime(nullable: false),
                        WorkPlan = c.String(),
                        AuditStatus = c.Int(nullable: false),
                        AuditType = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FieldWorkApplies",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EmployeeId = c.Long(nullable: false),
                        BeginDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Address = c.String(),
                        Reason = c.String(),
                        AuditStatus = c.Int(nullable: false),
                        AuditType = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PermissionRoleRefs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PermissionId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                        Status = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.Int(nullable: false),
                        IsPublic = c.Int(nullable: false),
                        Resource = c.String(),
                        Status = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoleRefs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                        Status = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VacationApplies",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EmployeeId = c.Long(nullable: false),
                        BeginDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Reason = c.String(),
                        VacationType = c.Int(nullable: false),
                        AuditStatus = c.Int(nullable: false),
                        AuditType = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Roles", "CreateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Roles", "UpdateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "EmployeeId", c => c.Long(nullable: false));
            AddColumn("dbo.Users", "CreateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "UpdateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Roles", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Users", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.Roles", "Id");
            AddPrimaryKey("dbo.Users", "Id");
            DropColumn("dbo.Roles", "User_Id");
            DropColumn("dbo.Users", "RoleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "RoleId", c => c.Int(nullable: false));
            AddColumn("dbo.Roles", "User_Id", c => c.Int());
            DropPrimaryKey("dbo.Users");
            DropPrimaryKey("dbo.Roles");
            AlterColumn("dbo.Users", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Roles", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Users", "UpdateTime");
            DropColumn("dbo.Users", "CreateTime");
            DropColumn("dbo.Users", "EmployeeId");
            DropColumn("dbo.Roles", "UpdateTime");
            DropColumn("dbo.Roles", "CreateTime");
            DropTable("dbo.VacationApplies");
            DropTable("dbo.UserRoleRefs");
            DropTable("dbo.Permissions");
            DropTable("dbo.PermissionRoleRefs");
            DropTable("dbo.FieldWorkApplies");
            DropTable("dbo.CompensatoryApplies");
            DropTable("dbo.BusinessTripApplies");
            DropTable("dbo.AbsenceApplies");
            AddPrimaryKey("dbo.Users", "Id");
            AddPrimaryKey("dbo.Roles", "Id");
            CreateIndex("dbo.Roles", "User_Id");
            AddForeignKey("dbo.Roles", "User_Id", "dbo.Users", "Id");
        }
    }
}
