using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BookShop.Migrations
{
    public partial class StoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE PROCEDURE FilterBooksByTitlePart
                @titlepart varchar(50)
                AS
                SELECT * FROM Books WHERE Title LIKE '%'+@titlepart+'%'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE FilterBooksByTitlePart");
        }
    }
}
