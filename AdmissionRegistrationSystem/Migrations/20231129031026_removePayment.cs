using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdmissionRegistrationSystem.Migrations
{
    /// <inheritdoc />
    public partial class removePayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentInfo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    paymentStatus = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    regId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    transactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentInfo", x => x.Id);
                });
        }
    }
}
