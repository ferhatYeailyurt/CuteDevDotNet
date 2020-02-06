namespace CuteDev.Log.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        id = c.Decimal(nullable: false, precision: 18, scale: 0, identity: true),
                        guid = c.String(nullable: false, maxLength: 50),
                        createDate = c.DateTime(nullable: false),
                        creatorId = c.Decimal(nullable: false, precision: 18, scale: 0),
                        creatorIP = c.String(maxLength: 50),
                        logLevel = c.String(maxLength: 50),
                        message = c.String(nullable: false),
                        appName = c.String(maxLength: 50),
                        logData = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Logs");
        }
    }
}
