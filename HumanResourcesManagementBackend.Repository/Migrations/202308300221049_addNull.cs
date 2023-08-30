namespace HumanResourcesManagementBackend.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.R_Employee", "BirthDay", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.R_Employee", "BirthDay", c => c.DateTime(nullable: false));
        }
    }
}
