using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryData.Migrations
{
    public partial class addinitialentitymodelsupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DewyIndex",
                table: "LibraryAssets",
                newName: "DeweyIndex");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeweyIndex",
                table: "LibraryAssets",
                newName: "DewyIndex");
        }
    }
}
