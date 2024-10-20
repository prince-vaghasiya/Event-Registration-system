using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventRegistrationSystem.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Events_OrganizerUserID",
                table: "Events",
                column: "OrganizerUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_OrganizerUserID",
                table: "Events",
                column: "OrganizerUserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_OrganizerUserID",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_OrganizerUserID",
                table: "Events");
        }
    }
}
