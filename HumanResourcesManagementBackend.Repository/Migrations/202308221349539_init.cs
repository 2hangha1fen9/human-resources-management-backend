namespace HumanResourcesManagementBackend.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.R_AbsenceApply",
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
                "dbo.R_BusinessTripApply",
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
                "dbo.R_CompensatoryApply",
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
                "dbo.R_Employee",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        WorkNum = c.String(),
                        WorkStatus = c.Int(nullable: false),
                        Name = c.String(),
                        Gender = c.Int(nullable: false),
                        MaritalStatus = c.Int(nullable: false),
                        BirthDay = c.DateTime(nullable: false),
                        IdCard = c.String(),
                        Native = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        AcademicDegree = c.Int(nullable: false),
                        HireDate = c.DateTime(nullable: false),
                        PositionId = c.Long(nullable: false),
                        DepartmentId = c.Long(nullable: false),
                        PositionLevel = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.R_FieldWorkApply",
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
                "dbo.R_PermissionRoleRef",
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
                "dbo.R_Permission",
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
                "dbo.R_Role",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Status = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.R_UserRoleRef",
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
                "dbo.R_User",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LoginName = c.String(),
                        Password = c.String(),
                        Question = c.String(),
                        Answer = c.String(),
                        EmployeeId = c.Long(nullable: false),
                        Status = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.R_VacationApply",
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.R_VacationApply");
            DropTable("dbo.R_User");
            DropTable("dbo.R_UserRoleRef");
            DropTable("dbo.R_Role");
            DropTable("dbo.R_Permission");
            DropTable("dbo.R_PermissionRoleRef");
            DropTable("dbo.R_FieldWorkApply");
            DropTable("dbo.R_Employee");
            DropTable("dbo.R_CompensatoryApply");
            DropTable("dbo.R_BusinessTripApply");
            DropTable("dbo.R_AbsenceApply");
        }
    }
}
