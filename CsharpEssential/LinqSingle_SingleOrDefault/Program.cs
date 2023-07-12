using LinqSingle_SingleOrDefault;

DataSource dataSource = new DataSource();

// Single
Employee firstAnalyst = dataSource.GetAllEmployees().Single(emp => emp.Job == "Analyst");
Console.WriteLine($"{firstAnalyst.EmployeeID}, {firstAnalyst.EmployeeName}, {firstAnalyst.Job}, {firstAnalyst.Salary}");

try
{
    Employee firstManager = dataSource.GetAllEmployees().Single(emp => emp.Job == "Manager");
}
catch (InvalidOperationException exp)
{
    Console.WriteLine(exp.Message);  // Sequence contains more than one matching element.

}

try
{
    Employee firstClerk = dataSource.GetAllEmployees().Single(emp => emp.Job == "Clerk");
}
catch (InvalidOperationException exp)
{
    Console.WriteLine(exp.Message);  // Sequence contains no one matching element.
}

// SingleOrDefault
Employee? firstDesigner = dataSource.GetAllEmployees().SingleOrDefault(emp => emp.Job == "Designer");
if (firstDesigner != null)
{
    Console.WriteLine($"{firstDesigner.EmployeeID}, {firstDesigner.EmployeeName}, {firstDesigner.Job}, {firstDesigner.Salary}");
}
else
{
    Console.WriteLine("No element is found");
}

try
{
    Employee? firstDeveloper = dataSource.GetAllEmployees().SingleOrDefault(emp => emp.Job == "Developer");

}
catch (InvalidOperationException exp)
{
    Console.WriteLine(exp.Message); // Sequence contains more than one mathcing element
}

Employee? firstSupport = dataSource.GetAllEmployees().SingleOrDefault(emp => emp.Job == "Support");
if (firstSupport != null)
{
    Console.WriteLine($"{firstSupport.EmployeeID}, {firstSupport.EmployeeName}, {firstSupport.Job}, {firstSupport.Salary}");
}
else
{
    Console.WriteLine("No matching employee"); // Sequence contains no matching element
}


