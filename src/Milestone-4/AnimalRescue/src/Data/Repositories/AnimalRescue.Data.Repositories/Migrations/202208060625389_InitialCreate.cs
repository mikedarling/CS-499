namespace AnimalRescue.Data.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Animals",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AnimalId = c.String(maxLength: 20),
                        IsMixedBreed = c.Boolean(nullable: false),
                        DateOfBirth = c.DateTime(),
                        DateOfOutcome = c.DateTime(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Name = c.String(maxLength: 100),
                        AnimalType_Id = c.Long(),
                        OutcomeSubtype_Id = c.Long(),
                        OutcomeType_Id = c.Long(),
                        Sex_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnimalTypes", t => t.AnimalType_Id)
                .ForeignKey("dbo.OutcomeSubtypes", t => t.OutcomeSubtype_Id)
                .ForeignKey("dbo.OutcomeTypes", t => t.OutcomeType_Id)
                .ForeignKey("dbo.Sexes", t => t.Sex_Id)
                .Index(t => t.AnimalType_Id)
                .Index(t => t.OutcomeSubtype_Id)
                .Index(t => t.OutcomeType_Id)
                .Index(t => t.Sex_Id);
            
            CreateTable(
                "dbo.AnimalTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Breeds",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        AnimalType_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnimalTypes", t => t.AnimalType_Id, cascadeDelete: true)
                .Index(t => t.AnimalType_Id);
            
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OutcomeSubtypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        OutcomeType_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OutcomeTypes", t => t.OutcomeType_Id, cascadeDelete: true)
                .Index(t => t.OutcomeType_Id);
            
            CreateTable(
                "dbo.OutcomeTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sexes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Animal_to_Breed",
                c => new
                    {
                        Animal_Id = c.Long(nullable: false),
                        Breed_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Animal_Id, t.Breed_Id })
                .ForeignKey("dbo.Animals", t => t.Animal_Id, cascadeDelete: true)
                .ForeignKey("dbo.Breeds", t => t.Breed_Id, cascadeDelete: true)
                .Index(t => t.Animal_Id)
                .Index(t => t.Breed_Id);
            
            CreateTable(
                "dbo.Animal_to_Color",
                c => new
                    {
                        Animal_Id = c.Long(nullable: false),
                        Color_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Animal_Id, t.Color_Id })
                .ForeignKey("dbo.Animals", t => t.Animal_Id, cascadeDelete: true)
                .ForeignKey("dbo.Colors", t => t.Color_Id, cascadeDelete: true)
                .Index(t => t.Animal_Id)
                .Index(t => t.Color_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.Animals", "Sex_Id", "dbo.Sexes");
            DropForeignKey("dbo.Animals", "OutcomeType_Id", "dbo.OutcomeTypes");
            DropForeignKey("dbo.Animals", "OutcomeSubtype_Id", "dbo.OutcomeSubtypes");
            DropForeignKey("dbo.OutcomeSubtypes", "OutcomeType_Id", "dbo.OutcomeTypes");
            DropForeignKey("dbo.Animal_to_Color", "Color_Id", "dbo.Colors");
            DropForeignKey("dbo.Animal_to_Color", "Animal_Id", "dbo.Animals");
            DropForeignKey("dbo.Animal_to_Breed", "Breed_Id", "dbo.Breeds");
            DropForeignKey("dbo.Animal_to_Breed", "Animal_Id", "dbo.Animals");
            DropForeignKey("dbo.Animals", "AnimalType_Id", "dbo.AnimalTypes");
            DropForeignKey("dbo.Breeds", "AnimalType_Id", "dbo.AnimalTypes");
            DropIndex("dbo.Animal_to_Color", new[] { "Color_Id" });
            DropIndex("dbo.Animal_to_Color", new[] { "Animal_Id" });
            DropIndex("dbo.Animal_to_Breed", new[] { "Breed_Id" });
            DropIndex("dbo.Animal_to_Breed", new[] { "Animal_Id" });
            DropIndex("dbo.IdentityUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.OutcomeSubtypes", new[] { "OutcomeType_Id" });
            DropIndex("dbo.Breeds", new[] { "AnimalType_Id" });
            DropIndex("dbo.Animals", new[] { "Sex_Id" });
            DropIndex("dbo.Animals", new[] { "OutcomeType_Id" });
            DropIndex("dbo.Animals", new[] { "OutcomeSubtype_Id" });
            DropIndex("dbo.Animals", new[] { "AnimalType_Id" });
            DropTable("dbo.Animal_to_Color");
            DropTable("dbo.Animal_to_Breed");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.Sexes");
            DropTable("dbo.OutcomeTypes");
            DropTable("dbo.OutcomeSubtypes");
            DropTable("dbo.Colors");
            DropTable("dbo.Breeds");
            DropTable("dbo.AnimalTypes");
            DropTable("dbo.Animals");
        }
    }
}
