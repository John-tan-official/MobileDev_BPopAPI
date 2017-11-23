namespace BPopAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatingUsers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QandAs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        ChoiceA = c.String(),
                        ChoiceB = c.String(),
                        ChoiceC = c.String(),
                        ChoiceD = c.String(),
                        Answer = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.String(),
                        Name = c.String(),
                        Team = c.String(),
                        Points = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.QandAs");
        }
    }
}
