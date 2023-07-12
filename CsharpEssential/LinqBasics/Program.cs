using LinqBasics;

DataSource dataSource = new DataSource();
//List<Employee> managers = dataSource.GetAllEmployees().Where(emp => emp.Job == "Manager").ToList();
//List<Employee> managers = dataSource.GetAllEmployees().Where(emp => emp.City == "New York").ToList();
List<Employee> managers = dataSource.GetAllEmployees().Where(emp => emp.City == "London").ToList(); // Empty List
foreach (Employee employee in managers)
{
    Console.WriteLine($"{employee.EmployeeID}, {employee.EmployeeName}, {employee.Job}, {employee.City}");
}
