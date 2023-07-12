
namespace Company
{
    /// <summary>
    /// Represents an employee of the organization
    /// </summary>
    public class Employee
    {
        // Properties
        public int EmployeeID { get; set; }

        public  string? EmployeeName { get; set; }

        public string? Email { get; set; }

        public Departement? Departement { get; set; }
    }
}