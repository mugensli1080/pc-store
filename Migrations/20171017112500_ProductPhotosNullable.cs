using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace pcstore.Migrations
{
    public partial class ProductPhotosNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductPhotos_ProductPhotoId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_SpecificationPhotos_SpecificationPhotoId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "SpecificationPhotoId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ProductPhotoId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductPhotos_ProductPhotoId",
                table: "Products",
                column: "ProductPhotoId",
                principalTable: "ProductPhotos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SpecificationPhotos_SpecificationPhotoId",
                table: "Products",
                column: "SpecificationPhotoId",
                principalTable: "SpecificationPhotos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductPhotos_ProductPhotoId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_SpecificationPhotos_SpecificationPhotoId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "SpecificationPhotoId",
                table: "Products",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductPhotoId",
                table: "Products",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductPhotos_ProductPhotoId",
                table: "Products",
                column: "ProductPhotoId",
                principalTable: "ProductPhotos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SpecificationPhotos_SpecificationPhotoId",
                table: "Products",
                column: "SpecificationPhotoId",
                principalTable: "SpecificationPhotos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
