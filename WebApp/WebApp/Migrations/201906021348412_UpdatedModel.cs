namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PricelistItems", "PassengerTypeCoefficientId", "dbo.PassengerTypeCoefficients");
            DropForeignKey("dbo.Drivelines", "DrivingPlan_Id", "dbo.DrivingPlans");
            DropIndex("dbo.Drivelines", new[] { "DrivingPlan_Id" });
            DropIndex("dbo.PricelistItems", new[] { "PassengerTypeCoefficientId" });
            RenameColumn(table: "dbo.Drivelines", name: "DrivingPlan_Id", newName: "DrivingPlanId");
            AddColumn("dbo.PricelistItems", "PassengerType", c => c.Int(nullable: false));
            AddColumn("dbo.Tickets", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "VerificationStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.Drivelines", "DrivingPlanId", c => c.Int(nullable: false));
            AlterColumn("dbo.PricelistItems", "Price", c => c.Single(nullable: false));
            CreateIndex("dbo.Drivelines", "DrivingPlanId");
            CreateIndex("dbo.Tickets", "ApplicationUser_Id");
            AddForeignKey("dbo.Tickets", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Drivelines", "DrivingPlanId", "dbo.DrivingPlans", "Id", cascadeDelete: true);
            DropColumn("dbo.PricelistItems", "PassengerTypeCoefficientId");
            DropColumn("dbo.AspNetUsers", "IsVerified");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "IsVerified", c => c.Boolean(nullable: false));
            AddColumn("dbo.PricelistItems", "PassengerTypeCoefficientId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Drivelines", "DrivingPlanId", "dbo.DrivingPlans");
            DropForeignKey("dbo.Tickets", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Tickets", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Drivelines", new[] { "DrivingPlanId" });
            AlterColumn("dbo.PricelistItems", "Price", c => c.Int(nullable: false));
            AlterColumn("dbo.Drivelines", "DrivingPlanId", c => c.Int());
            DropColumn("dbo.AspNetUsers", "VerificationStatus");
            DropColumn("dbo.Tickets", "ApplicationUser_Id");
            DropColumn("dbo.PricelistItems", "PassengerType");
            RenameColumn(table: "dbo.Drivelines", name: "DrivingPlanId", newName: "DrivingPlan_Id");
            CreateIndex("dbo.PricelistItems", "PassengerTypeCoefficientId");
            CreateIndex("dbo.Drivelines", "DrivingPlan_Id");
            AddForeignKey("dbo.Drivelines", "DrivingPlan_Id", "dbo.DrivingPlans", "Id");
            AddForeignKey("dbo.PricelistItems", "PassengerTypeCoefficientId", "dbo.PassengerTypeCoefficients", "PassengerTypeCoefficientId", cascadeDelete: true);
        }
    }
}
