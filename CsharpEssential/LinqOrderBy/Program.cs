
using LinqOrderBy;

DataSource dataSource = new DataSource();
//List<Employee> sortedEmployees = dataSource.GetAllEmployees().OrderBy(emp => emp.EmployeeName).ToList();
//List<Employee> sortedEmployees = dataSource.GetAllEmployees().OrderBy(emp => emp.Salary).ToList();
//List<Employee> sortedEmployees = dataSource.GetAllEmployees().OrderByDescending(emp => emp.Salary).ToList();
/*
List<Employee> sortedEmployees = dataSource.GetAllEmployees()
    .OrderBy(emp => emp.Job)
    .ThenBy(emp => emp.EmployeeName)
    .ToList();
*/
List<Employee> sortedEmployees = dataSource.GetAllEmployees()
    .OrderByDescending(emp => emp.Job)
    .ThenByDescending(emp => emp.Salary)
    .ToList();

foreach (var employee in sortedEmployees)
{
    Console.WriteLine($"{employee.EmployeeID}, {employee.EmployeeName}, {employee.Job}, {employee.Salary}");
}