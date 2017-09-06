namespace Mvc5WithOracleDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "C##AML.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "C##AML.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("C##AML.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("C##AML.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "C##AML.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(nullable: false, maxLength: 128),
                        EmailConfirmed = c.Decimal(nullable: false, precision: 1, scale: 0),
                        PasswordHash = c.String(maxLength: 256),
                        SecurityStamp = c.String(maxLength: 256),
                        PhoneNumber = c.String(maxLength: 256),
                        PhoneNumberConfirmed = c.Decimal(nullable: false, precision: 1, scale: 0),
                        TwoFactorEnabled = c.Decimal(nullable: false, precision: 1, scale: 0),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Decimal(nullable: false, precision: 1, scale: 0),
                        AccessFailedCount = c.Decimal(nullable: false, precision: 10, scale: 0),
                        UserName = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "C##AML.UserClaims",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(maxLength: 256),
                        ClaimValue = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("C##AML.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "C##AML.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("C##AML.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("C##AML.UserRoles", "UserId", "C##AML.Users");
            DropForeignKey("C##AML.UserLogins", "UserId", "C##AML.Users");
            DropForeignKey("C##AML.UserClaims", "UserId", "C##AML.Users");
            DropForeignKey("C##AML.UserRoles", "RoleId", "C##AML.Roles");
            DropIndex("C##AML.UserLogins", new[] { "UserId" });
            DropIndex("C##AML.UserClaims", new[] { "UserId" });
            DropIndex("C##AML.Users", "UserNameIndex");
            DropIndex("C##AML.UserRoles", new[] { "RoleId" });
            DropIndex("C##AML.UserRoles", new[] { "UserId" });
            DropIndex("C##AML.Roles", "RoleNameIndex");
            DropTable("C##AML.UserLogins");
            DropTable("C##AML.UserClaims");
            DropTable("C##AML.Users");
            DropTable("C##AML.UserRoles");
            DropTable("C##AML.Roles");
        }
    }
}
