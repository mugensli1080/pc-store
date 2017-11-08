using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace pcstore.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("INSERT INTO Categories (Name, Created, Modified) VALUES ('Cables', GETDATE(), GETDATE());");
            migrationBuilder.Sql("INSERT INTO Categories (Name, Created, Modified) VALUES ('Cameras', GETDATE(), GETDATE());");
            migrationBuilder.Sql("INSERT INTO Categories (Name, Created, Modified) VALUES ('CPUs', GETDATE(), GETDATE());");
            migrationBuilder.Sql("INSERT INTO Categories (Name, Created, Modified) VALUES ('Graphics Cards', GETDATE(), GETDATE());");

            migrationBuilder.Sql("INSERT INTO SubCategories (Name, CategoryId, Created, Modified) VALUES ('None', (SELECT Id FROM Categories WHERE Name = 'Cameras'), GETDATE(), GETDATE());");

            migrationBuilder.Sql("INSERT INTO SubCategories (Name, CategoryId, Created, Modified) VALUES ('Display Cables & Adapters', (SELECT Id FROM Categories WHERE Name = 'Cables'), GETDATE(), GETDATE());");
            migrationBuilder.Sql("INSERT INTO SubCategories (Name, CategoryId, Created, Modified) VALUES ('Internal Power Cables', (SELECT Id FROM Categories WHERE Name = 'Cables'), GETDATE(), GETDATE());");
            migrationBuilder.Sql("INSERT INTO SubCategories (Name, CategoryId, Created, Modified) VALUES ('SATA Cables', (SELECT Id FROM Categories WHERE Name = 'Cables'), GETDATE(), GETDATE());");

            migrationBuilder.Sql("INSERT INTO SubCategories (Name, CategoryId, Created, Modified) VALUES ('Intel Socket 1151', (SELECT Id FROM Categories WHERE Name = 'CPUs'), GETDATE(), GETDATE());");
            migrationBuilder.Sql("INSERT INTO SubCategories (Name, CategoryId, Created, Modified) VALUES ('Intel socket 1151 CL', (SELECT Id FROM Categories WHERE Name = 'CPUs'), GETDATE(), GETDATE());");


            migrationBuilder.Sql("INSERT INTO SubCategories (Name, CategoryId, Created, Modified) VALUES ('GeForce GTX 7xx', (SELECT Id FROM Categories WHERE Name = 'Graphics Cards'), GETDATE(), GETDATE());");
            migrationBuilder.Sql("INSERT INTO SubCategories (Name, CategoryId, Created, Modified) VALUES ('Geforce Gtx 1070', (SELECT Id FROM Categories WHERE Name = 'Graphics Cards'), GETDATE(), GETDATE());");
            migrationBuilder.Sql("INSERT INTO SubCategories (Name, CategoryId, Created, Modified) VALUES ('Geforce Gtx 1080', (SELECT Id FROM Categories WHERE Name = 'Graphics Cards'), GETDATE(), GETDATE());");
            migrationBuilder.Sql("INSERT INTO SubCategories (Name, CategoryId, Created, Modified) VALUES ('Geforce Gtx 1080 Ti', (SELECT Id FROM Categories WHERE Name = 'Graphics Cards'), GETDATE(), GETDATE());");
            migrationBuilder.Sql("INSERT INTO SubCategories (Name, CategoryId, Created, Modified) VALUES ('Radeon RX 550', (SELECT Id FROM Categories WHERE Name = 'Graphics Cards'), GETDATE(), GETDATE());");
            migrationBuilder.Sql("INSERT INTO SubCategories (Name, CategoryId, Created, Modified) VALUES ('Radeon RX 560', (SELECT Id FROM Categories WHERE Name = 'Graphics Cards'), GETDATE(), GETDATE());");
            migrationBuilder.Sql("INSERT INTO SubCategories (Name, CategoryId, Created, Modified) VALUES ('Radeon RX 570', (SELECT Id FROM Categories WHERE Name = 'Graphics Cards'), GETDATE(), GETDATE());");
            migrationBuilder.Sql("INSERT INTO SubCategories (Name, CategoryId, Created, Modified) VALUES ('Radeon RX 580', (SELECT Id FROM Categories WHERE Name = 'Graphics Cards'), GETDATE(), GETDATE());");


            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, CategoryId, Created, Modified) VALUES ('Aerocool Display Adapter with VGA, DVI, and 4K HDMI', 'The Aerocool SD0HV10 Display Adapter allows you to connect a display via Mini DisplayPort/Thunderbolt with ease.', 39, (SELECT CategoryId FROM SubCategories WHERE Name = 'Display Cables & Adapters'), GETDATE(), GETDATE());");
            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, CategoryId, Created, Modified) VALUES ('Alogic DisplayPort to HDMI M-M Cable 1m', 'The Alogic Elements DisplayPort to HDMI Cable is designed to connect a PC enabled with a DisplayPort output to a display enabled with an HDMI input.', 15, (SELECT CategoryId FROM SubCategories WHERE Name = 'Display Cables & Adapters'), GETDATE(), GETDATE());");

            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, CategoryId, Created, Modified) VALUES ('ATX 24 Pin Bridging Plug Black', 'Have you ever asked yourself how to fill the system without having the system overheating due to the missing coolant in the loop? This is the easy solution.', 2, (SELECT CategoryId FROM SubCategories WHERE Name = 'Internal Power Cables'), GETDATE(), GETDATE());");
            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, CategoryId, Created, Modified) VALUES ('CableMod 3-Pin Fan Extension Cable Black 30cm', 'The CableMod Basics 3-Pin Fan Extension Cable extends the length of a 3-pin fan cable. 30cm length.', 9, (SELECT CategoryId FROM SubCategories WHERE Name = 'Internal Power Cables'), GETDATE(), GETDATE());");

            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, CategoryId, Created, Modified) VALUES ('CableMod Dual Right Angle SATA 3 Cable Black 45cm', 'CableMod SATA 3 Cables are ideal for dressing up your system’s SATA devices and are covered in black sleeving.', 8, (SELECT CategoryId FROM SubCategories WHERE Name = 'SATA Cables'), GETDATE(), GETDATE());");
            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, CategoryId, Created, Modified) VALUES ('CableMod ModFlex SATA 3 Cable Black/Black 60cm', 'CableMod ModFlex SATA 3 Cables are ideal for dressing up your system’s SATA devices.', 19, (SELECT CategoryId FROM SubCategories WHERE Name = 'SATA Cables'), GETDATE(), GETDATE());");

            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, CategoryId, Created, Modified) VALUES ('Creative 3D Camera BlasterX Senz3D Black', 'Discover new ways to interact with Creative BlasterX Senz3D. With facial recognition, you can login to your PC with your face.', 269, (SELECT ID FROM Categories WHERE Name = 'Cameras'), GETDATE(), GETDATE());");
            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, CategoryId, Created, Modified) VALUES ('FrontRow Wearable Camera by Ubiquiti Black', 'Front Row is a revolutionary wearable camera that allows you to both live in the moment and capture it like no other device.', 529, (SELECT ID FROM Categories WHERE Name = 'Cameras'), GETDATE(), GETDATE());");

            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, CategoryId, Created, Modified) VALUES ('Intel Celeron G3900', 'Intel Celeron Processor G3900 2.8GHz Dual Core CPU, LGA1151, 2MB SmartCache, 51W Max TDP.', 55, (SELECT CategoryId FROM SubCategories WHERE Name = 'Intel Socket 1151'), GETDATE(), GETDATE());");
            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, CategoryId, Created, Modified) VALUES ('Intel Celeron G3950', 'Intel Celeron Processor G3950 3.0GHz Dual Core CPU, LGA1151, 2MB SmartCache, 51W Max TDP.', 65, (SELECT CategoryId FROM SubCategories WHERE Name = 'Intel Socket 1151'), GETDATE(), GETDATE());");


            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, CategoryId, Created, Modified) VALUES ('Intel Core i3 8100', 'intel Core i3 8100 3.6GHz Quad Core, 4 Thread CPU, LGA1151, 6MB Cache, Max TDP 65W, Intel HD Graphics 630, BX80684I38100. Retail box including fan.', 179, (SELECT CategoryId FROM SubCategories WHERE Name = 'Intel socket 1151 CL'), GETDATE(), GETDATE());");
            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, CategoryId, Created, Modified) VALUES ('Intel Core i3 8350k', 'Intel Core i3 8350K 4.0GHz Quad Core, 4 Thread Unlocked CPU, LGA1151, 8MB cache, Max TDP 91W, Intel HD Graphics 630, BX80684I38350K. Does not include fan.', 179, (SELECT CategoryId FROM SubCategories WHERE Name = 'Intel socket 1151 CL'), GETDATE(), GETDATE());");


            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, CategoryId, Created, Modified) VALUES ('ASUS ROG GeForce GTX 1080 Strix Aura 8GB', 'The ASUS ROG GeForce GTX 1080 Strix Gaming graphics card features a 1695MHz base clock with a 1835MHz boost clock in OC mode, 8GB 256-bit GDDR5X memory, PCI-E 3.0.', 899, (SELECT CategoryId FROM SubCategories WHERE Name = 'Geforce Gtx 1080'), GETDATE(), GETDATE());");
            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, CategoryId, Created, Modified) VALUES ('Galax GeForce GTX 1080 EXOC Sniper White 8GB', 'On sale! (normally $799) The Galax GeForce GTX 1080 EXOC Sniper White graphics card features a 1657MHz base clock with a 1797MHz boost clock.', 729, (SELECT CategoryId FROM SubCategories WHERE Name = 'Geforce Gtx 1080'), GETDATE(), GETDATE());");


            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, CategoryId, Created, Modified) VALUES ('ASUS Radeon RX 550 2GB', 'The ASUS Radeon RX 550 graphics card features a 1183MHz engine clock, 2GB 128-bit GDDR5 memory, PCI-E 3.0, 7000MHz memory clock.', 129, (SELECT CategoryId FROM SubCategories WHERE Name = 'Radeon RX 550'), GETDATE(), GETDATE());");
            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, CategoryId, Created, Modified) VALUES ('XFX Radeon RX 580 GTS XXX Edition 4GB', 'The XFX Radeon RX 580 GTS XXX Edition features a 1257MHz core clock with a 1386MHz OC+ boost clock, 4GB 256-bit GDDR5 memory, PCI-E 3.0, OpenGL 4.5 support, DirectX 12.', 179, (SELECT CategoryId FROM SubCategories WHERE Name = 'Radeon RX 580'), GETDATE(), GETDATE());");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categories WHERE Name IN ('Cables', 'Cameras', 'CPUs', 'Graphics Cards', 'None');");
        }
    }
}
