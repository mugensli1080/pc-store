using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace pcstore.Migrations
{
    public partial class AlteredProductAndSpecPhotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductPhotos_ProductPhotoId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_SpecificationPhotos_SpecificationPhotoId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductPhotoId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SpecificationPhotoId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductPhotoId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SpecificationPhotoId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "SpecificationPhotos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "SpecificationPhotos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Activated",
                table: "ProductPhotos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductPhotos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SpecificationPhotos_ProductId",
                table: "SpecificationPhotos",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPhotos_ProductId",
                table: "ProductPhotos",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPhotos_Products_ProductId",
                table: "ProductPhotos",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpecificationPhotos_Products_ProductId",
                table: "SpecificationPhotos",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPhotos_Products_ProductId",
                table: "ProductPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecificationPhotos_Products_ProductId",
                table: "SpecificationPhotos");

            migrationBuilder.DropIndex(
                name: "IX_SpecificationPhotos_ProductId",
                table: "SpecificationPhotos");

            migrationBuilder.DropIndex(
                name: "IX_ProductPhotos_ProductId",
                table: "ProductPhotos");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "SpecificationPhotos");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "SpecificationPhotos");

            migrationBuilder.DropColumn(
                name: "Activated",
                table: "ProductPhotos");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductPhotos");

            migrationBuilder.AddColumn<int>(
                name: "ProductPhotoId",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpecificationPhotoId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductPhotoId",
                table: "Products",
                column: "ProductPhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SpecificationPhotoId",
                table: "Products",
                column: "SpecificationPhotoId");

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
    }
}
