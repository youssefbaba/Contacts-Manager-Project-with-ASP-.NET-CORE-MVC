using LinqElementAt_AlementAtOrDefault;

DataSource dataSource = new DataSource();

// ElementAt 
Employee secondManager = dataSource.GetAllEmployees().Where(emp => emp.Job == "Manager").ElementAt(1);
Console.WriteLine($"{secondManager.EmployeeID}, {secondManager.EmployeeName}, {secondManager.Job}, {secondManager.Salary}");

try
{
    Employee fourthManager = dataSource.GetAllEmployees().Where(emp => emp.Job == "Manager").ElementAt(4);
}
catch (ArgumentOutOfRangeException exp)
{
    Console.WriteLine(exp.Message);
}

// ElementAtOrDefault
Employee? thirdManager = dataSource.GetAllEmployees().Where(emp => emp.Job == "Manager").ElementAtOrDefault(2);
if (thirdManager != null)
{
    Console.WriteLine($"{thirdManager.EmployeeID}, {thirdManager.EmployeeName}, {thirdManager.Job}, {thirdManager.Salary}");
}
else
{
    Console.WriteLine("No manager at index 2.");
}

Employee? fifthManager = dataSource.GetAllEmployees().Where(emp => emp.Job == "Manager").ElementAtOrDefault(6);
if (fifthManager != null)
{
    Console.WriteLine($"{fifthManager.EmployeeID}, {fifthManager.EmployeeName}, {fifthManager.Job}, {fifthManager.Salary}");
}
else
{
    Console.WriteLine("No manager at index 6.");
}


