using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace CodeFirstApproach.Migrations
{
    /// <inheritdoc />
    public partial class Examples5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Examples5",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Value1 = table.Column<string>(type: "longtext", nullable: false),
                    Value2 = table.Column<string>(type: "longtext", nullable: false),
                    Value3 = table.Column<string>(type: "longtext", nullable: false),
                    Value4 = table.Column<string>(type: "longtext", nullable: false),
                    Value5 = table.Column<string>(type: "longtext", nullable: false),
                    Value6 = table.Column<string>(type: "longtext", nullable: false),
                    Value7 = table.Column<string>(type: "longtext", nullable: false),
                    Value8 = table.Column<string>(type: "longtext", nullable: false),
                    Value9 = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examples5", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Examples5");
        }
    }
}
