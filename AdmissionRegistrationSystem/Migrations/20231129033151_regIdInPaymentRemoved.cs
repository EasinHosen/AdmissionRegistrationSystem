using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdmissionRegistrationSystem.Migrations
{
    /// <inheritdoc />
    public partial class regIdInPaymentRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "regId",
                table: "PaymentInfos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "regId",
                table: "PaymentInfos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
