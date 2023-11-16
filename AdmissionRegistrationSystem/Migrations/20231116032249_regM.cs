using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdmissionRegistrationSystem.Migrations
{
    /// <inheritdoc />
    public partial class regM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressType = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Provience = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Area = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamType = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Section = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Roll = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Result = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Board = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    PassingYear = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    regId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    School = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    College = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PermAddressId = table.Column<int>(type: "int", nullable: false),
                    PresAddressId = table.Column<int>(type: "int", nullable: false),
                    DOB = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    NID = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    SSCId = table.Column<int>(type: "int", nullable: false),
                    HSCId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registrations_Addresses_PermAddressId",
                        column: x => x.PermAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Registrations_Addresses_PresAddressId",
                        column: x => x.PresAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Registrations_ExamInfos_HSCId",
                        column: x => x.HSCId,
                        principalTable: "ExamInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Registrations_ExamInfos_SSCId",
                        column: x => x.SSCId,
                        principalTable: "ExamInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_HSCId",
                table: "Registrations",
                column: "HSCId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_PermAddressId",
                table: "Registrations",
                column: "PermAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_PresAddressId",
                table: "Registrations",
                column: "PresAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_SSCId",
                table: "Registrations",
                column: "SSCId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "ExamInfos");
        }
    }
}
