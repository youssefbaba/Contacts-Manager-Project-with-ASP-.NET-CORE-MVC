using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Entities
{
    public class ApplicationDbContext : DbContext
    {
        // Properties: 

#nullable disable 
        public virtual DbSet<Country> Countries { get; set; }

        public virtual DbSet<Person> Persons { get; set; }

#nullable restore 

        // Constructors
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        // Methods:

        // In OnModelCreating method we can configure the tables or primarykeys or realations between tables or Seed data or .....
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // To map model class with database table
            modelBuilder.Entity<Country>().ToTable(nameof(Countries));
            modelBuilder.Entity<Person>().ToTable(nameof(Persons));

            // To add Seed Data for Countries table when the database is newly created : 
            string countriesJson = File.ReadAllText("countries.json");
            List<Country>? countries = JsonSerializer.Deserialize<List<Country>>(countriesJson);
            if (countries != null)
            {
                foreach (Country country in countries)
                {
                    modelBuilder.Entity<Country>().HasData(country);
                }
            }

            // Fluent API to configure an existing property
            modelBuilder.Entity<Person>().Property(temp => temp.TIN)
                .HasColumnName("TaxIdentificationNumber")
                .HasColumnType("varchar(8)")
                .HasDefaultValue("ABCD1234");

            /*
            // To Specify the TaxIdentificationNumber column should be unique
            modelBuilder.Entity<Person>().HasIndex("TaxIdentificationNumber")
                .IsUnique();
            */

            // To add CheckConstraint to a specific property while insertion or updation
            modelBuilder.Entity<Person>().HasCheckConstraint("CHK_TIN", "len([TaxIdentificationNumber]) = 8");

            /*
            // Table relations with Fluent API but not commonly used
            modelBuilder.Entity<Person>()
                 .HasOne<Country>(child => child.Country)
                 .WithMany(parent => parent.Persons)
                 .HasForeignKey(child => child.CountryID);
            */

            // To add Seed Data for Persons table when the database is newly created : 
            string personsJson = File.ReadAllText("persons.json");
            List<Person>? persons = JsonSerializer.Deserialize<List<Person>>(personsJson);
            if (persons != null)
            {
                foreach (Person person in persons)
                {
                    modelBuilder.Entity<Person>().HasData(person);
                }
            }
        }

        public List<Person> ProcedureGetAllPersons()
        {
            return Persons.FromSqlRaw("EXECUTE [dbo].[GetAllPersons]").ToList();
        }

        public int ProcedureInsertPerson(Person person)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                    new SqlParameter("@PersonID", person.PersonID),
                    new SqlParameter("@PersonName", person.PersonName),
                    new SqlParameter("@Email", person.Email),
                    new SqlParameter("@DateOfBirth", person.DateOfBirth),
                    new SqlParameter("@Gender", person.Gender),
                    new SqlParameter("@CountryID", person.CountryID),
                    new SqlParameter("@Address", person.Address),
                    new SqlParameter("@ReceiveNewsLetters", person.ReceiveNewsLetters)
            };
            return Database.ExecuteSqlRaw("EXECUTE [dbo].[InsertPerson] @PersonID, @PersonName, @Email, @DateOfBirth, @Gender, @CountryID, @Address, @ReceiveNewsLetters", parameters);
        }

        public Person? ProcedureGetPersonByPersonID(Guid personID)
        {
            SqlParameter parameter = new SqlParameter("@PersonID", personID);

            List<Person> persons = Persons.FromSqlRaw("EXECUTE [dbo].[GetPersonByPersonID] @PersonID", parameter).ToList();

            return persons.FirstOrDefault();
        }

        public int ProcedureUpdatePerson(Person person)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                    new SqlParameter("@PersonID", person.PersonID),
                    new SqlParameter("@PersonName", person.PersonName),
                    new SqlParameter("@Email", person.Email),
                    new SqlParameter("@DateOfBirth", person.DateOfBirth),
                    new SqlParameter("@Gender", person.Gender),
                    new SqlParameter("@CountryID", person.CountryID),
                    new SqlParameter("@Address", person.Address),
                    new SqlParameter("@ReceiveNewsLetters", person.ReceiveNewsLetters)
            };

            return Database.ExecuteSqlRaw("EXECUTE [dbo].[UpdatePerson] @PersonID, @PersonName, @Email, @DateOfBirth, @Gender, @CountryID, @Address, @ReceiveNewsLetters", parameters);
        }

        public int ProcedureDeletePerson(Guid personID)
        {
            SqlParameter parameter = new SqlParameter("@PersonID", personID);
            return Database.ExecuteSqlRaw("EXECUTE [dbo].[DeletePerson] @PersonID", parameter);
        }
    }
}
