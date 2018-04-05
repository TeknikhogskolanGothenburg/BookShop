using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BookShop.Migrations
{
    public partial class checkConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER TABLE dbo.Ratings ADD CONSTRAINT CK_Ratings_Points CHECK (Points BETWEEN 1 AND 5);");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE dbo.Ratings DROP CONSTRAINT CK_Ratings_Points");
        }
    }
}
