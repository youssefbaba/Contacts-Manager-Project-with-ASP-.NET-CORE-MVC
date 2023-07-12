
namespace ClassLibraryEight
{
    public class SalesMan : IEmployee
    {
        // Fields
        private int _employeeID;
        private string? _employeeName;
        private string? _location;
        private string? _region;


        // Properties
        public int EmployeeID
        {
            get => _employeeID;
            set
            {
                if (value >= 500 && value < 1000)
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
        public string? Region
        {
            get => _region;
            set => _region = value;
        }

        // Constructor
        public SalesMan(int employeeID, string? employeeName, string? location, string? region)
        {
            _employeeID = employeeID;
            _employeeName = employeeName;
            _location = location;
            _region = region;
        }

        // Implementation of the interface method which is mandatory
        public string GetHealthInssuranceAmount()
        {
            return "Additional Health Insurrance premium amount is : 500";
        }
    }
}
