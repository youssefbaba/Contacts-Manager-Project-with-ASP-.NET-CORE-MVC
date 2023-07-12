
namespace LinqSelect
{
    public class DataSource
    {
        public List<Employee> GetAllEmployees()
        {
            return new List<Employee>()
            {
                new Employee(){EmployeeID = 101, EmployeeName = "Henry", Job = "Designer" , Salary = 5548},
                new Employee(){EmployeeID = 102, EmployeeName = "Jack", Job = "Developer" , Salary = 5854},
                new Employee(){EmployeeID = 103, EmployeeName = "Gabriel", Job = "Analyst" , Salary = 4548},
                new Employee(){EmployeeID = 104, EmployeeName = "William", Job = "Manager" , Salary = 7147},
                new Employee(){EmployeeID = 105, EmployeeName = "Alexa", Job = "Manager" , Salary = 7351},
                new Employee(){EmployeeID = 106, EmployeeName = "Adam", Job = "Developer" , Salary = 5547},
                new Employee(){EmployeeID = 107, EmployeeName = "Jessica", Job = "Manager" , Salary = 7145},
            };
        }
    }
}
