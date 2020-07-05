using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NG.DBManager.Infrastructure.Contracts.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deal",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Latitude = table.Column<decimal>(nullable: false),
                    Longitude = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommerceDeal",
                columns: table => new
                {
                    CommerceId = table.Column<Guid>(nullable: false),
                    DealId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommerceDeal", x => new { x.CommerceId, x.DealId });
                    table.ForeignKey(
                        name: "FK_CommerceDeal_Deal_DealId",
                        column: x => x.DealId,
                        principalTable: "Deal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Restaurant",
                columns: table => new
                {
                    CommerceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurant", x => x.CommerceId);
                });

            migrationBuilder.CreateTable(
                name: "Node",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    TourId = table.Column<Guid>(nullable: false),
                    LocationId = table.Column<Guid>(nullable: false),
                    DealId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Node", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Node_Deal_DealId",
                        column: x => x.DealId,
                        principalTable: "Deal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Node_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Audio",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    NodeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Audio_Node_NodeId",
                        column: x => x.NodeId,
                        principalTable: "Node",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NodeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_Node_NodeId",
                        column: x => x.NodeId,
                        principalTable: "Node",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tour",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    GeoJson = table.Column<string>(nullable: true),
                    Duration = table.Column<int>(nullable: false),
                    IsPremium = table.Column<bool>(nullable: false),
                    IsFeatured = table.Column<bool>(nullable: false),
                    ImageId = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tour", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tour_Image_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Surname = table.Column<string>(nullable: false),
                    Birthdate = table.Column<DateTime>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Role = table.Column<int>(nullable: false),
                    ImageId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Image_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TourTag",
                columns: table => new
                {
                    TourId = table.Column<Guid>(nullable: false),
                    TagId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourTag", x => new { x.TourId, x.TagId });
                    table.ForeignKey(
                        name: "FK_TourTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourTag_Tour_TourId",
                        column: x => x.TourId,
                        principalTable: "Tour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Commerce",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    LocationId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commerce", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commerce_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Commerce_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Coupon",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    ValidationDate = table.Column<DateTime>(nullable: false),
                    GenerationDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    NodeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coupon_Node_NodeId",
                        column: x => x.NodeId,
                        principalTable: "Node",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Coupon_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    TourId = table.Column<Guid>(nullable: false),
                    Score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => new { x.UserId, x.TourId });
                    table.ForeignKey(
                        name: "FK_Review_Tour_TourId",
                        column: x => x.TourId,
                        principalTable: "Tour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Review_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Location",
                columns: new[] { "Id", "Latitude", "Longitude", "Name" },
                values: new object[] { new Guid("0013a98e-32f6-494d-b055-c9fb4dafc3e8"), 33.842185m, -40.707753m, "Test Location" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Birthdate", "Email", "ImageId", "Name", "Password", "PhoneNumber", "Role", "Surname" },
                values: new object[,]
                {
                    { new Guid("b0f2451e-5820-4eca-a797-46a01693a3b2"), new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "basic@test.org", null, "Basic", "10000.+2PnZrnAWQRgqlMx+l8kyA==.ALiUC3pHYJJ7cr8Xqnn1y16XROosvjHNTDmf+Em+pMM=", "+222222222", 2, "QA User" },
                    { new Guid("0ac2c4c5-ebff-445e-85d4-1db76d65ce0a"), new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@test.org", null, "Admin", "10000.r1m2AhgohtRKaAYihSdiFQ==.9jOF0O4zo3WoBYq+H1f3XTPG9An8LZfEJd1uwB66N0s=", "+000000000", 0, "QA User" },
                    { new Guid("440edb6b-342e-4d5f-a233-62aef964cbfa"), new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "commerce@test.org", null, "Commerce", "10000.NcEE328o58z2KLy1cIiKMA==.5+Mwrqw7XVP2dE+RtcMorXI/Ri6daF4nCRZB4+xJUAY=", "+111111111", 1, "QA User" },
                    { new Guid("73b7b257-41f7-4b22-9a10-93fb91238fd9"), new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "fullcommerce@test.org", null, "FullCommerce", "10000./LphyV3IUSMjgcllhGg/HA==.ZeBKs4MVq3+BKEQw9ejzr/HbAwI7/KOGr10FqkuGSmE=", "+0111111111", 1, "QA User" }
                });

            migrationBuilder.InsertData(
                table: "Commerce",
                columns: new[] { "Id", "LocationId", "Name", "UserId" },
                values: new object[] { new Guid("a4506bf8-9cca-4413-b0d4-4247c61b1231"), new Guid("0013a98e-32f6-494d-b055-c9fb4dafc3e8"), "Test Commerce", new Guid("73b7b257-41f7-4b22-9a10-93fb91238fd9") });

            migrationBuilder.CreateIndex(
                name: "IX_Audio_NodeId",
                table: "Audio",
                column: "NodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Commerce_LocationId",
                table: "Commerce",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Commerce_UserId",
                table: "Commerce",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommerceDeal_DealId",
                table: "CommerceDeal",
                column: "DealId");

            migrationBuilder.CreateIndex(
                name: "IX_Coupon_NodeId",
                table: "Coupon",
                column: "NodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Coupon_UserId",
                table: "Coupon",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_NodeId",
                table: "Image",
                column: "NodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_Latitude_Longitude",
                table: "Location",
                columns: new[] { "Latitude", "Longitude" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Node_DealId",
                table: "Node",
                column: "DealId");

            migrationBuilder.CreateIndex(
                name: "IX_Node_LocationId",
                table: "Node",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Node_TourId",
                table: "Node",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_TourId",
                table: "Review",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Tour_ImageId",
                table: "Tour",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_TourTag_TagId",
                table: "TourTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_ImageId",
                table: "User",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_User_PhoneNumber",
                table: "User",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CommerceDeal_Commerce_CommerceId",
                table: "CommerceDeal",
                column: "CommerceId",
                principalTable: "Commerce",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurant_Commerce_CommerceId",
                table: "Restaurant",
                column: "CommerceId",
                principalTable: "Commerce",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Node_Tour_TourId",
                table: "Node",
                column: "TourId",
                principalTable: "Tour",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Node_NodeId",
                table: "Image");

            migrationBuilder.DropTable(
                name: "Audio");

            migrationBuilder.DropTable(
                name: "CommerceDeal");

            migrationBuilder.DropTable(
                name: "Coupon");

            migrationBuilder.DropTable(
                name: "Restaurant");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "TourTag");

            migrationBuilder.DropTable(
                name: "Commerce");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Node");

            migrationBuilder.DropTable(
                name: "Deal");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Tour");

            migrationBuilder.DropTable(
                name: "Image");
        }
    }
}
