using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdmissionRegistrationSystem.Migrations
{
    /// <inheritdoc />
    public partial class regToPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegistrationId",
                table: "PaymentInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentInfos_RegistrationId",
                table: "PaymentInfos",
                column: "RegistrationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentInfos_Registrations_RegistrationId",
                table: "PaymentInfos",
                column: "RegistrationId",
                principalTable: "Registrations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentInfos_Registrations_RegistrationId",
                table: "PaymentInfos");

            migrationBuilder.DropIndex(
                name: "IX_PaymentInfos_RegistrationId",
                table: "PaymentInfos");

            migrationBuilder.DropColumn(
                name: "RegistrationId",
                table: "PaymentInfos");
        }
    }
}
