using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class InsertPerson_StoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedureInsertPerson = @"
            CREATE PROCEDURE [dbo].[InsertPerson] 
            (
            @PersonID uniqueidentifier,
            @PersonName nvarchar(40),
            @Email nvarchar(40),
            @DateOfBirth datetime2(7),
            @Gender nvarchar(10),
            @CountryID uniqueidentifier,
            @Address nvarchar(200),
            @ReceiveNewsLetters bit
            )
            AS BEGIN
	            INSERT INTO [dbo].[Persons]
		            (
                    PersonID,
                    PersonName,
                    Email,
                    DateOfBirth,
                    Gender,
                    CountryID,
                    Address,
                    ReceiveNewsLetters
		            )
                VALUES
		            (
                    @PersonID,
                    @PersonName,
                    @Email,
                    @DateOfBirth,
                    @Gender,
                    @CountryID,
                    @Address,
                    @ReceiveNewsLetters
		            )
            END
            ";
            migrationBuilder.Sql(procedureInsertPerson);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedureInsertPerson = @"
            DROP PROCEDURE [dbo].[InsertPerson] 
            ";
            migrationBuilder.Sql(procedureInsertPerson);
        }
    }
}
