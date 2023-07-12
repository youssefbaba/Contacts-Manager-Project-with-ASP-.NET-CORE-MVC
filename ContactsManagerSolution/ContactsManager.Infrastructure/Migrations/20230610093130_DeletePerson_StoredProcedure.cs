using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactsManager.Infrastructure.DatabaseContext.Migrations
{
    public partial class DeletePerson_StoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedureDeletePerson = @"
            CREATE PROCEDURE [dbo].[DeletePerson]
            @PersonID uniqueidentifier
            AS
            BEGIN
	            DELETE FROM [dbo].[Persons]
                WHERE PersonID = @PersonID
            END    
            ";
            migrationBuilder.Sql(procedureDeletePerson);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedureDeletePerson = @"
            DROP PROCEDURE [dbo].[DeletePerson]   
            ";
            migrationBuilder.Sql(procedureDeletePerson);
        }
    }
}
