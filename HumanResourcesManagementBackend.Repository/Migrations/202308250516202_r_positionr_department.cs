namespace HumanResourcesManagementBackend.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class r_positionr_department : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.R_Department",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PositionName = c.String(),
                        Status = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.R_Position",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PositionName = c.String(),
                        Status = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.R_Position");
            DropTable("dbo.R_Department");
        }
    }
}
