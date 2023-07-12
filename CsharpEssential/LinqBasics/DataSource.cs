
namespace LinqBasics
{
    public class DataSource
    {
        // Methods
        public List<Employee> GetAllEmployees()
        {
            return new List<Employee>()
            {
                new Employee(){EmployeeID = 101, EmployeeName = "Henry", Job ="Designer" , City="Tokyo"},
                new Employee(){EmployeeID = 102, EmployeeName = "Jack", Job ="Developer" , City="New York"},
                new Employee(){EmployeeID = 103, EmployeeName = "Gabriel", Job ="Analyst" , City="Tokyo"},
                new Employee(){EmployeeID = 104, EmployeeName = "William", Job ="Manager" , City="Tokyo"},
                new Employee(){EmployeeID = 105, EmployeeName = "Alexa", Job ="Manager" , City="New York"}
            };
        }
    }
}
