using LinqFirst_FirstOrDefault;

DataSource dataSource = new DataSource();

// Where
Console.WriteLine("Where : ");
List<Employee> filteredEmployee = dataSource.GetAllEmployees().Where(emp => emp.Job == "Manager").ToList();
Employee manager = filteredEmployee[0];
Console.WriteLine($"{manager.EmployeeID}, {manager.EmployeeName}, {manager.Job}, {manager.Salary}");

// First 
Console.WriteLine("First : ");
Employee firstManager = dataSource.GetAllEmployees().First(emp => emp.Job == "Manager");
Console.WriteLine($"{firstManager.EmployeeID}, {firstManager.EmployeeName}, {firstManager.Job}, {firstManager.Salary}");

try
{
    Employee firstClerk = dataSource.GetAllEmployees().First(emp => emp.Job == "Clerk");
    Console.WriteLine($"{firstClerk.EmployeeID}, {firstClerk.EmployeeName}, {firstClerk.Job}, {firstClerk.Salary}");
}
catch (InvalidOperationException exp)
{
    Console.WriteLine(exp.Message);  // Sequence contains no matching element
}

// FirstOrDefault most recommended
Console.WriteLine("FirstOrDefault : ");
Employee? firstDeveloper = dataSource.GetAllEmployees().FirstOrDefault(emp => emp.Job == "Developer");
if (firstDeveloper != null)
{
    Console.WriteLine($"{firstDeveloper.EmployeeID}, {firstDeveloper.EmployeeName}, {firstDeveloper.Job}, {firstDeveloper.Salary}");
}
else
{
    Console.WriteLine("Data source is empty or no element passes this test.");
}

Employee? firstSupport = dataSource.GetAllEmployees().FirstOrDefault(emp => emp.Job == "Support");
if (firstSupport != null)
{
    Console.WriteLine($"{firstSupport.EmployeeID}, {firstSupport.EmployeeName}, {firstSupport.Job}, {firstSupport.Salary}");
}
else
{
    Console.WriteLine("Data source is empty or no element passes this test.");
}






