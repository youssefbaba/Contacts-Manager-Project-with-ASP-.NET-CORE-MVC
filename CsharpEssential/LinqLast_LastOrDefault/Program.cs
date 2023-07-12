using LinqLast_LastOrDefault;

DataSource dataSource = new DataSource();

// Where
Console.WriteLine("Where : ");
List<Employee> filteredEmployees = dataSource.GetAllEmployees().Where(emp => emp.Job == "Manager").ToList();
Employee manager = filteredEmployees[filteredEmployees.Count - 1];
Console.WriteLine($"{manager.EmployeeID}, {manager.EmployeeName}, {manager.Job}, {manager.Salary}");


// Last
Console.WriteLine("Last : ");
Employee lastManager = dataSource.GetAllEmployees().Last(emp => emp.Job == "Manager");
Console.WriteLine($"{lastManager.EmployeeID}, {lastManager.EmployeeName}, {lastManager.Job}, {lastManager.Salary}");

try
{
    Employee lastClerk = dataSource.GetAllEmployees().Last(emp => emp.Job == "Clerk");
    Console.WriteLine($"{lastClerk.EmployeeID}, {lastClerk.EmployeeName}, {lastClerk.Job}, {lastClerk.Salary}");

}
catch (InvalidOperationException exp)
{
    Console.WriteLine(exp.Message);
}



// LastOrDefault most recommended
Console.WriteLine("LastOrDefault : ");
Employee? lastDeveloper = dataSource.GetAllEmployees().LastOrDefault(emp => emp.Job == "Developer");
if (lastDeveloper != null)
{
    Console.WriteLine($"{lastDeveloper.EmployeeID}, {lastDeveloper.EmployeeName}, {lastDeveloper.Job}, {lastDeveloper.Salary}");

}
else
{
    Console.WriteLine("There is no Developer here");
}

Employee? lastSupport = dataSource.GetAllEmployees().LastOrDefault(emp => emp.Job == "Support");
if (lastSupport != null)
{
    Console.WriteLine($"{lastSupport.EmployeeID}, {lastSupport.EmployeeName}, {lastSupport.Job}, {lastSupport.Salary}");
}
else
{
    Console.WriteLine("There is no Support here");
}





