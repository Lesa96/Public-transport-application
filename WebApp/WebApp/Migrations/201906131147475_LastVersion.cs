namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LastVersion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Coordinates", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Drivelines", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.DrivingPlans", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Stations", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.PassengerTypeCoefficients", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.PricelistItems", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Pricelists", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Tickets", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "RowVersion");
            DropColumn("dbo.Pricelists", "RowVersion");
            DropColumn("dbo.PricelistItems", "RowVersion");
            DropColumn("dbo.PassengerTypeCoefficients", "RowVersion");
            DropColumn("dbo.Stations", "RowVersion");
            DropColumn("dbo.DrivingPlans", "RowVersion");
            DropColumn("dbo.Drivelines", "RowVersion");
            DropColumn("dbo.Coordinates", "RowVersion");
        }
    }
}
