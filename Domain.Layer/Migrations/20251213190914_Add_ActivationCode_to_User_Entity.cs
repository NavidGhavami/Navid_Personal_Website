using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Layer.Migrations
{
    /// <inheritdoc />
    public partial class Add_ActivationCode_to_User_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActivationCode",
                table: "Users",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivationCode",
                table: "Users");
        }
    }
}
