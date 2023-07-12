
namespace ClassLibraryEight
{

    // Interface
    public interface IEmployee
    {
        // Auto-Implemented Properties
        int EmployeeID { get; set; }

        string? EmployeeName { get; set; }

        string? Location { get; set; }



        // Abstract Methods
        string GetHealthInssuranceAmount();

    }
}