using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactsManager.Infrastructure.DatabaseContext.Migrations
{
    public partial class AddSSNIsChangedColumnToPersonsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SSNIsChanged",
                table: "Persons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.Sql("UPDATE Persons SET SSN = 'AAAA1111',SSNIsChanged = 'TRUE' WHERE SSN IS NULL AND SSNIsChanged = 'FALSE'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Persons SET SSN = NULL,SSNIsChanged = 'FALSE' WHERE SSN = 'AAAA1111' AND SSNIsChanged ='TRUE'");

            migrationBuilder.DropColumn(
                name: "SSNIsChanged",
                table: "Persons");
        }
    }
}
