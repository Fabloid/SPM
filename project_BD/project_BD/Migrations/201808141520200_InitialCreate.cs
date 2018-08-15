namespace project_BD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        _id = c.Int(nullable: false, identity: true),
                        _name = c.String(),
                        _type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t._id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        _id = c.Int(nullable: false, identity: true),
                        _family = c.String(),
                        _name = c.String(),
                        _patronymic = c.String(),
                        _email = c.String(),
                        _skill = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t._id);
            
            CreateTable(
                "dbo.Employees_in_Project",
                c => new
                    {
                        _id = c.Int(nullable: false, identity: true),
                        _id_project = c.Int(nullable: false),
                        _id_employee = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t._id);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        _id = c.Int(nullable: false, identity: true),
                        _name = c.String(),
                        _id_lead = c.Int(nullable: false),
                        _id_company_custoner = c.Int(nullable: false),
                        _id_company_performer = c.Int(nullable: false),
                        _date_begin = c.DateTime(nullable: false),
                        _date_end = c.DateTime(nullable: false),
                        _priority = c.Int(nullable: false),
                        _comment = c.String(),
                    })
                .PrimaryKey(t => t._id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Projects");
            DropTable("dbo.Employees_in_Project");
            DropTable("dbo.Employees");
            DropTable("dbo.Companies");
        }
    }
}
