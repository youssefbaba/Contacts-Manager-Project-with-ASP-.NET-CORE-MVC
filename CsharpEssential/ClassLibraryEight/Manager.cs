
using System.Reflection.Metadata.Ecma335;

namespace ClassLibraryEight
{
    public class Manager : IEmployee
    {
        // Fields
        private int _employeeID;
        private string? _employeeName;
        private string? _location;
        private string? _departementName;


        // Properties
        public int EmployeeID
        {
            get => _employeeID;

            set
            {
                if (value >= 1000 && value <= 2000)
                {
                    _employeeID = value;
                }
            }
        }
        public string? EmployeeName
        {
            get => _employeeName;
            set => _employeeName = value;
        }
        public string? Location
        {
            get => _location;
            set => _location = value;
        }
        public string? DepartementName
        {
            get => _departementName;
            set => _departementName = value;
        }

        // Constructor
        public Manager(int employeeID, string? employeeName, string? location, string? departementName)
        {
            _employeeID = employeeID;
            _employeeName = employeeName;
            _location = location;
            _departementName = departementName;
        }

        // Implementation of the interface method which is mandatory
        public string GetHealthInssuranceAmount()
        {
            return "Additional Health Insurrance premium amount is : 1000";
        }
    }
}
