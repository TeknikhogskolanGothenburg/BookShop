using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BookShop.Migrations
{
    public partial class StoredProcedure2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
               @"CREATE PROCEDURE BooksWithQuotes
                @quotepart varchar(50)
                @date1 datetime
                @date2 datetime

                AS
                SELECT * FROM Books JOIN Quotes ON Books.Id = Quotes.BookId 
                WHERE Quotes.Text LIKE '%'+@quotepart+'%' AND Books.ReleaseDate BETWEEN '%'+@date1+'%' AND '%'+@date2+'%'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE BooksWithQuotes");
        }
    }
}
