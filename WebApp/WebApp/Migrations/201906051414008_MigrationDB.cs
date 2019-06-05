namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationDB : DbMigration
    {
        public override void Up()
        {
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
                "dbo.Drivelines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DrivingPlans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DrivelineId = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Departures = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Drivelines", t => t.DrivelineId, cascadeDelete: true)
                .Index(t => t.DrivelineId);
            
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
                "dbo.PassengerTypeCoefficients",
                c => new
                    {
                        PassengerTypeCoefficientId = c.Int(nullable: false, identity: true),
                        PassengerType = c.Int(nullable: false),
                        Coefficient = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.PassengerTypeCoefficientId);
            
            CreateTable(
                "dbo.PricelistItems",
                c => new
                    {
                        PricelistItemId = c.Int(nullable: false, identity: true),
                        TicketType = c.Int(nullable: false),
                        PassengerType = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        PricelistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PricelistItemId)
                .ForeignKey("dbo.Pricelists", t => t.PricelistId, cascadeDelete: true)
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
                "dbo.Tickets",
                c => new
                    {
                        TicketId = c.Int(nullable: false, identity: true),
                        TimeOfPurchase = c.DateTime(nullable: false),
                        TicketInfoId = c.Int(nullable: false),
                        PassengerId = c.String(maxLength: 128),
                        ControllerId = c.String(maxLength: 128),
                        IsCanceled = c.Boolean(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TicketId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ControllerId)
                .ForeignKey("dbo.AspNetUsers", t => t.PassengerId)
                .ForeignKey("dbo.PricelistItems", t => t.TicketInfoId, cascadeDelete: true)
                .Index(t => t.TicketInfoId)
                .Index(t => t.PassengerId)
                .Index(t => t.ControllerId)
                .Index(t => t.ApplicationUser_Id);
            
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
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "BirthDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            AddColumn("dbo.AspNetUsers", "VerificationStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "TicketInfoId", "dbo.PricelistItems");
            DropForeignKey("dbo.Tickets", "PassengerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tickets", "ControllerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tickets", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PricelistItems", "PricelistId", "dbo.Pricelists");
            DropForeignKey("dbo.StationDrivelines", "Driveline_Id", "dbo.Drivelines");
            DropForeignKey("dbo.StationDrivelines", "Station_Id", "dbo.Stations");
            DropForeignKey("dbo.Stations", "CoordinatesId", "dbo.Coordinates");
            DropForeignKey("dbo.DrivingPlans", "DrivelineId", "dbo.Drivelines");
            DropIndex("dbo.StationDrivelines", new[] { "Driveline_Id" });
            DropIndex("dbo.StationDrivelines", new[] { "Station_Id" });
            DropIndex("dbo.Tickets", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Tickets", new[] { "ControllerId" });
            DropIndex("dbo.Tickets", new[] { "PassengerId" });
            DropIndex("dbo.Tickets", new[] { "TicketInfoId" });
            DropIndex("dbo.PricelistItems", new[] { "PricelistId" });
            DropIndex("dbo.Stations", new[] { "CoordinatesId" });
            DropIndex("dbo.DrivingPlans", new[] { "DrivelineId" });
            DropColumn("dbo.AspNetUsers", "VerificationStatus");
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "BirthDate");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropColumn("dbo.AspNetUsers", "PassengerType");
            DropTable("dbo.StationDrivelines");
            DropTable("dbo.Tickets");
            DropTable("dbo.Pricelists");
            DropTable("dbo.PricelistItems");
            DropTable("dbo.PassengerTypeCoefficients");
            DropTable("dbo.Stations");
            DropTable("dbo.DrivingPlans");
            DropTable("dbo.Drivelines");
            DropTable("dbo.Coordinates");
        }
    }
}
