using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpenseTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CategoryAndRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Expenses");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "RowId" },
                values: new object[,]
                {
                    { 1, "Food & Dining", "52491cf6-4415-4352-b7f4-503d4738ea52" },
                    { 2, "Transportation", "43d715ce-f180-4132-a62a-c411cfa33f76" },
                    { 3, "Groceries", "6556e545-ce90-4b66-8aed-0be34b7aeb6c" },
                    { 4, "Utilities", "0d460124-ddcd-4521-bde7-7e769b220bad" },
                    { 5, "Entertainment", "57a3b8e7-c89a-4037-a2b7-ae38b9df6654" },
                    { 6, "Health & Fitness", "04a486ef-e348-466b-a835-16a504ca7ffd" },
                    { 7, "Education", "879dec83-9594-4a8e-ae1f-2fe315aef88f" },
                    { 8, "Shopping", "cfa3ee41-41fe-4e6a-8ea3-54ff2db343b7" },
                    { 9, "Rent", "71948dd0-c32b-44c4-b8df-dc72d7fc824e" },
                    { 10, "Travel", "6f1409f3-a93e-4031-bcb1-d3d3c9c1203c" },
                    { 11, "Bills", "8ce3e2f4-303d-4883-8ab2-9a2112ff3903" },
                    { 12, "Insurance", "58b37051-bed9-4cb0-9898-260740a71265" },
                    { 13, "Miscellaneous", "c16d5c12-13a9-43fc-b8c7-01425c896e9d" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CategoryId",
                table: "Expenses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_RowId",
                table: "Categories",
                column: "RowId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Categories_CategoryId",
                table: "Expenses",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Categories_CategoryId",
                table: "Expenses");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_CategoryId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Expenses");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Expenses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
