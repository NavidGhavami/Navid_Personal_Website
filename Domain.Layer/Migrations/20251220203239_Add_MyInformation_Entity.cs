using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Layer.Migrations
{
    /// <inheritdoc />
    public partial class Add_MyInformation_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyInformations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeaderTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MainTitle = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(550)", maxLength: 550, nullable: false),
                    ProfileImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    License = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CompletedProject = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CooperatingCompany = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    WorkExperience = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyInformations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyInformations");
        }
    }
}
