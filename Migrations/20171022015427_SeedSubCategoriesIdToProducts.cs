using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace pcstore.Migrations
{
    public partial class SeedSubCategoriesIdToProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE p SET p.SubCategoryId = s.Id FROM Products p INNER JOIN SubCategories s ON p.CategoryId = s.CategoryId;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Products SET SubCategoryId = NULL");
        }
    }
}
