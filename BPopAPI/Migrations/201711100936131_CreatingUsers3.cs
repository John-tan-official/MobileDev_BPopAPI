namespace BPopAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatingUsers3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameStarters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        begin = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GameStarters");
        }
    }
}
