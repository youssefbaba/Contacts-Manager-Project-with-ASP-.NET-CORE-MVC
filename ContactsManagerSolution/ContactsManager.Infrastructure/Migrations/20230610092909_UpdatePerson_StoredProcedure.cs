using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactsManager.Infrastructure.DatabaseContext.Migrations
{
    public partial class UpdatePerson_StoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedureUpdatePerson = @"
            CREATE PROCEDURE [dbo].[UpdatePerson] 
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
	        UPDATE [dbo].[Persons]
            SET
		        PersonID = @PersonID,
		        PersonName = @PersonName,
		        Email = @Email,
		        DateOfBirth = @DateOfBirth,
		        Gender = @Gender,
		        CountryID = @CountryID,
		        Address = @Address,
		        ReceiveNewsLetters = @ReceiveNewsLetters
	        WHERE PersonID = @PersonID
            END
            ";
            migrationBuilder.Sql(procedureUpdatePerson);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedureUpdatePerson = @"
            DROP PROCEDURE [dbo].[UpdatePerson] 
            ";
            migrationBuilder.Sql(procedureUpdatePerson);
        }
    }
}
