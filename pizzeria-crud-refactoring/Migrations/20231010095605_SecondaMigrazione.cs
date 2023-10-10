using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pizzeria_crud_refactoring.Migrations
{
    /// <inheritdoc />
    public partial class SecondaMigrazione : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                table: "Pizza",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pizza_CategoryId",
                table: "Pizza",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizza_Category_CategoryId",
                table: "Pizza",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizza_Category_CategoryId",
                table: "Pizza");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Pizza_CategoryId",
                table: "Pizza");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Pizza");
        }
    }
}
