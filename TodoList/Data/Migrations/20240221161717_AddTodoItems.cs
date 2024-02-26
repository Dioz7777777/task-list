using Microsoft.EntityFrameworkCore.Migrations;
using TodoList.Models;

#nullable disable

namespace TodoList.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTodoItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: "TodoItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    DueDate = table.Column<DateOnly>(nullable: false),
                    IsComplete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table => { table.PrimaryKey("PK_TodoItems", x => x.Id); });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "TodoItems");
        }
    }
}
