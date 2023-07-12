using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class GetPersons_StoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedureGetAllPersons = @"
            CREATE PROCEDURE [dbo].[GetAllPersons]
            AS BEGIN
               SELECT * FROM [dbo].[Persons]
            END
            ";
            migrationBuilder.Sql(procedureGetAllPersons);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedureGetAllPersons = @"
            DROP PROCEDURE [dbo].[GetAllPersons]
            ";
            migrationBuilder.Sql(procedureGetAllPersons);
        }
    }
}
