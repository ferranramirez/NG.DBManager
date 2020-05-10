using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NG.DBManager.Infrastructure.Contracts.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Audio",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coordinates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(9, 6)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(9, 6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordinates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Featured",
                columns: table => new
                {
                    TourId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Featured", x => x.TourId);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Restaurant",
                columns: table => new
                {
                    CoordinatesId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurant", x => x.CoordinatesId);
                    table.ForeignKey(
                        name: "FK_Restaurant_Coordinates_CoordinatesId",
                        column: x => x.CoordinatesId,
                        principalTable: "Coordinates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AudioImage",
                columns: table => new
                {
                    AudioId = table.Column<Guid>(nullable: false),
                    ImageId = table.Column<Guid>(nullable: false),
                    StartTime = table.Column<TimeSpan>(nullable: false),
                    EndTime = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioImage", x => new { x.AudioId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_AudioImage_Audio_AudioId",
                        column: x => x.AudioId,
                        principalTable: "Audio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AudioImage_Image_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tour",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Duration = table.Column<int>(nullable: false),
                    IsPremium = table.Column<bool>(nullable: false),
                    TourId = table.Column<Guid>(nullable: true),
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
                    table.ForeignKey(
                        name: "FK_Tour_Featured_TourId",
                        column: x => x.TourId,
                        principalTable: "Featured",
                        principalColumn: "TourId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(70)", nullable: true),
                    Birthdate = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(type: "nvarchar(254)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(40)", nullable: true),
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
                name: "Node",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Order = table.Column<int>(nullable: false),
                    CoordinatesId = table.Column<Guid>(nullable: false),
                    TourId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Node", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Node_Coordinates_CoordinatesId",
                        column: x => x.CoordinatesId,
                        principalTable: "Coordinates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Node_Tour_TourId",
                        column: x => x.TourId,
                        principalTable: "Tour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "NodeAudio",
                columns: table => new
                {
                    NodeId = table.Column<Guid>(nullable: false),
                    AudioId = table.Column<Guid>(nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeAudio", x => new { x.NodeId, x.AudioId });
                    table.ForeignKey(
                        name: "FK_NodeAudio_Audio_AudioId",
                        column: x => x.AudioId,
                        principalTable: "Audio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NodeAudio_Node_NodeId",
                        column: x => x.NodeId,
                        principalTable: "Node",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NodeImage",
                columns: table => new
                {
                    NodeId = table.Column<Guid>(nullable: false),
                    ImageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeImage", x => new { x.NodeId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_NodeImage_Image_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NodeImage_Node_NodeId",
                        column: x => x.NodeId,
                        principalTable: "Node",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AudioImage_ImageId",
                table: "AudioImage",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Node_CoordinatesId",
                table: "Node",
                column: "CoordinatesId");

            migrationBuilder.CreateIndex(
                name: "IX_Node_TourId",
                table: "Node",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_NodeAudio_AudioId",
                table: "NodeAudio",
                column: "AudioId");

            migrationBuilder.CreateIndex(
                name: "IX_NodeImage_ImageId",
                table: "NodeImage",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_TourId",
                table: "Review",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Tour_ImageId",
                table: "Tour",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Tour_TourId",
                table: "Tour",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_TourTag_TagId",
                table: "TourTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_User_ImageId",
                table: "User",
                column: "ImageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AudioImage");

            migrationBuilder.DropTable(
                name: "NodeAudio");

            migrationBuilder.DropTable(
                name: "NodeImage");

            migrationBuilder.DropTable(
                name: "Restaurant");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "TourTag");

            migrationBuilder.DropTable(
                name: "Audio");

            migrationBuilder.DropTable(
                name: "Node");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Coordinates");

            migrationBuilder.DropTable(
                name: "Tour");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Featured");
        }
    }
}
