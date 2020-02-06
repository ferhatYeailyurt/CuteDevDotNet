namespace CuteDev.Users.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        guid = c.String(nullable: false, maxLength: 50),
                        createIp = c.String(nullable: false, maxLength: 50),
                        createDate = c.DateTime(nullable: false),
                        creatorId = c.Int(nullable: false),
                        updateIp = c.String(maxLength: 50),
                        updateDate = c.DateTime(),
                        updaterId = c.Int(),
                        deleted = c.Boolean(nullable: false),
                        code = c.String(nullable: false, maxLength: 50),
                        title = c.String(nullable: false, maxLength: 100),
                        description = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        guid = c.String(nullable: false, maxLength: 50),
                        createIp = c.String(nullable: false, maxLength: 50),
                        createDate = c.DateTime(nullable: false),
                        creatorId = c.Int(nullable: false),
                        updateIp = c.String(maxLength: 50),
                        updateDate = c.DateTime(),
                        updaterId = c.Int(),
                        deleted = c.Boolean(nullable: false),
                        title = c.String(nullable: false, maxLength: 100),
                        description = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.RolesPermissions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        guid = c.String(nullable: false, maxLength: 50),
                        createIp = c.String(nullable: false, maxLength: 50),
                        createDate = c.DateTime(nullable: false),
                        creatorId = c.Int(nullable: false),
                        updateIp = c.String(maxLength: 50),
                        updateDate = c.DateTime(),
                        updaterId = c.Int(),
                        deleted = c.Boolean(nullable: false),
                        rol_Id = c.Int(nullable: false),
                        permission_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        guid = c.String(nullable: false, maxLength: 50),
                        createIp = c.String(nullable: false, maxLength: 50),
                        createDate = c.DateTime(nullable: false),
                        creatorId = c.Int(nullable: false),
                        updateIp = c.String(maxLength: 50),
                        updateDate = c.DateTime(),
                        updaterId = c.Int(),
                        deleted = c.Boolean(nullable: false),
                        email = c.String(nullable: false, maxLength: 250),
                        password = c.String(nullable: false, maxLength: 250),
                        fullname = c.String(nullable: false, maxLength: 200),
                        role = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.UsersMeta",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        guid = c.String(nullable: false, maxLength: 50),
                        createIp = c.String(nullable: false, maxLength: 50),
                        createDate = c.DateTime(nullable: false),
                        creatorId = c.Int(nullable: false),
                        updateIp = c.String(maxLength: 50),
                        updateDate = c.DateTime(),
                        updaterId = c.Int(),
                        deleted = c.Boolean(nullable: false),
                        user_Id = c.Int(nullable: false),
                        metaKey = c.String(nullable: false, maxLength: 50),
                        metaValue = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.UsersRoles",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        guid = c.String(nullable: false, maxLength: 50),
                        createIp = c.String(nullable: false, maxLength: 50),
                        createDate = c.DateTime(nullable: false),
                        creatorId = c.Int(nullable: false),
                        updateIp = c.String(maxLength: 50),
                        updateDate = c.DateTime(),
                        updaterId = c.Int(),
                        deleted = c.Boolean(nullable: false),
                        user_Id = c.Int(nullable: false),
                        role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UsersRoles");
            DropTable("dbo.UsersMeta");
            DropTable("dbo.Users");
            DropTable("dbo.RolesPermissions");
            DropTable("dbo.Roles");
            DropTable("dbo.Permissions");
        }
    }
}
