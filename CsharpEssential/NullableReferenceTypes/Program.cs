using NullableReferenceTypes;

EmployeeBusinessLogic employeeBusinessLogic = new EmployeeBusinessLogic();
Employee? employee = employeeBusinessLogic.GetEmployee();
/*
if (employee != null)
{
    Console.WriteLine($"Employee Name : {employee.EmployeeName}");
}
*/
Console.WriteLine(employee?.EmployeeName);

// Employee : non-nullable type (null values are not accepted)
// Employee? : nullable type (null values are accepted)