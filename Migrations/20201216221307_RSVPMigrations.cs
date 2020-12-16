using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingPlanner.Migrations
{
    public partial class RSVPMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RSVP_Users_UserID",
                table: "RSVP");

            migrationBuilder.DropForeignKey(
                name: "FK_RSVP_Weddings_WeddingID",
                table: "RSVP");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RSVP",
                table: "RSVP");

            migrationBuilder.RenameTable(
                name: "RSVP",
                newName: "RSVPs");

            migrationBuilder.RenameIndex(
                name: "IX_RSVP_WeddingID",
                table: "RSVPs",
                newName: "IX_RSVPs_WeddingID");

            migrationBuilder.RenameIndex(
                name: "IX_RSVP_UserID",
                table: "RSVPs",
                newName: "IX_RSVPs_UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RSVPs",
                table: "RSVPs",
                column: "RSVPID");

            migrationBuilder.AddForeignKey(
                name: "FK_RSVPs_Users_UserID",
                table: "RSVPs",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RSVPs_Weddings_WeddingID",
                table: "RSVPs",
                column: "WeddingID",
                principalTable: "Weddings",
                principalColumn: "WeddingID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RSVPs_Users_UserID",
                table: "RSVPs");

            migrationBuilder.DropForeignKey(
                name: "FK_RSVPs_Weddings_WeddingID",
                table: "RSVPs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RSVPs",
                table: "RSVPs");

            migrationBuilder.RenameTable(
                name: "RSVPs",
                newName: "RSVP");

            migrationBuilder.RenameIndex(
                name: "IX_RSVPs_WeddingID",
                table: "RSVP",
                newName: "IX_RSVP_WeddingID");

            migrationBuilder.RenameIndex(
                name: "IX_RSVPs_UserID",
                table: "RSVP",
                newName: "IX_RSVP_UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RSVP",
                table: "RSVP",
                column: "RSVPID");

            migrationBuilder.AddForeignKey(
                name: "FK_RSVP_Users_UserID",
                table: "RSVP",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RSVP_Weddings_WeddingID",
                table: "RSVP",
                column: "WeddingID",
                principalTable: "Weddings",
                principalColumn: "WeddingID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
