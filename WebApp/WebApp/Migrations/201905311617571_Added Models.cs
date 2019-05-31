namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Drivelines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        DrivingPlan_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DrivingPlans", t => t.DrivingPlan_Id)
                .Index(t => t.DrivingPlan_Id);
            
            CreateTable(
                "dbo.Stations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        CoordinatesId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coordinates", t => t.CoordinatesId, cascadeDelete: true)
                .Index(t => t.CoordinatesId);
            
            CreateTable(
                "dbo.Coordinates",
                c => new
                    {
                        CoordinatesId = c.Int(nullable: false, identity: true),
                        CoordX = c.Single(nullable: false),
                        CoordY = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.CoordinatesId);
            
            CreateTable(
                "dbo.DrivingPlans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PricelistItems",
                c => new
                    {
                        PricelistItemId = c.Int(nullable: false, identity: true),
                        TicketType = c.Int(nullable: false),
                        PassengerTypeCoefficientId = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        PricelistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PricelistItemId)
                .ForeignKey("dbo.Pricelists", t => t.PricelistId, cascadeDelete: true)
                .ForeignKey("dbo.PassengerTypeCoefficients", t => t.PassengerTypeCoefficientId, cascadeDelete: true)
                .Index(t => t.PassengerTypeCoefficientId)
                .Index(t => t.PricelistId);
            
            CreateTable(
                "dbo.Pricelists",
                c => new
                    {
                        PricelistId = c.Int(nullable: false, identity: true),
                        ValidFrom = c.DateTime(nullable: false),
                        ValidUntil = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PricelistId);
            
            CreateTable(
                "dbo.PassengerTypeCoefficients",
                c => new
                    {
                        PassengerTypeCoefficientId = c.Int(nullable: false, identity: true),
                        PassengerType = c.Int(nullable: false),
                        Coefficient = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.PassengerTypeCoefficientId);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        TicketId = c.Int(nullable: false, identity: true),
                        TimeOfPurchase = c.DateTime(nullable: false),
                        TicketInfoId = c.Int(nullable: false),
                        PassengerId = c.String(maxLength: 128),
                        ControllerId = c.String(maxLength: 128),
                        IsCanceled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TicketId)
                .ForeignKey("dbo.AspNetUsers", t => t.ControllerId)
                .ForeignKey("dbo.AspNetUsers", t => t.PassengerId)
                .ForeignKey("dbo.PricelistItems", t => t.TicketInfoId, cascadeDelete: true)
                .Index(t => t.TicketInfoId)
                .Index(t => t.PassengerId)
                .Index(t => t.ControllerId);
            
            CreateTable(
                "dbo.StationDrivelines",
                c => new
                    {
                        Station_Id = c.Int(nullable: false),
                        Driveline_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Station_Id, t.Driveline_Id })
                .ForeignKey("dbo.Stations", t => t.Station_Id, cascadeDelete: true)
                .ForeignKey("dbo.Drivelines", t => t.Driveline_Id, cascadeDelete: true)
                .Index(t => t.Station_Id)
                .Index(t => t.Driveline_Id);
            
            AddColumn("dbo.AspNetUsers", "PassengerType", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "IsVerified", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "TicketInfoId", "dbo.PricelistItems");
            DropForeignKey("dbo.Tickets", "PassengerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tickets", "ControllerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PricelistItems", "PassengerTypeCoefficientId", "dbo.PassengerTypeCoefficients");
            DropForeignKey("dbo.PricelistItems", "PricelistId", "dbo.Pricelists");
            DropForeignKey("dbo.Drivelines", "DrivingPlan_Id", "dbo.DrivingPlans");
            DropForeignKey("dbo.StationDrivelines", "Driveline_Id", "dbo.Drivelines");
            DropForeignKey("dbo.StationDrivelines", "Station_Id", "dbo.Stations");
            DropForeignKey("dbo.Stations", "CoordinatesId", "dbo.Coordinates");
            DropIndex("dbo.StationDrivelines", new[] { "Driveline_Id" });
            DropIndex("dbo.StationDrivelines", new[] { "Station_Id" });
            DropIndex("dbo.Tickets", new[] { "ControllerId" });
            DropIndex("dbo.Tickets", new[] { "PassengerId" });
            DropIndex("dbo.Tickets", new[] { "TicketInfoId" });
            DropIndex("dbo.PricelistItems", new[] { "PricelistId" });
            DropIndex("dbo.PricelistItems", new[] { "PassengerTypeCoefficientId" });
            DropIndex("dbo.Stations", new[] { "CoordinatesId" });
            DropIndex("dbo.Drivelines", new[] { "DrivingPlan_Id" });
            DropColumn("dbo.AspNetUsers", "IsVerified");
            DropColumn("dbo.AspNetUsers", "PassengerType");
            DropTable("dbo.StationDrivelines");
            DropTable("dbo.Tickets");
            DropTable("dbo.PassengerTypeCoefficients");
            DropTable("dbo.Pricelists");
            DropTable("dbo.PricelistItems");
            DropTable("dbo.DrivingPlans");
            DropTable("dbo.Coordinates");
            DropTable("dbo.Stations");
            DropTable("dbo.Drivelines");
        }
    }
}
