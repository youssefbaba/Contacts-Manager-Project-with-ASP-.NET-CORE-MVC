using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{

    public partial class GetPersonByPersonID_StoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedureGetPersonByPersonID = @"
            CREATE PROCEDURE [dbo].[GetPersonByPersonID] 
            (@PersonID uniqueidentifier)
            AS BEGIN
	            SELECT * FROM [dbo].[Persons]
                WHERE PersonID = @PersonID
            END
            ";
            migrationBuilder.Sql(procedureGetPersonByPersonID);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedureGetPersonByPersonID = @"
            DROP PROCEDURE [dbo].[GetPersonByPersonIDss] 
            ";
            migrationBuilder.Sql(procedureGetPersonByPersonID);
        }
    }
}

