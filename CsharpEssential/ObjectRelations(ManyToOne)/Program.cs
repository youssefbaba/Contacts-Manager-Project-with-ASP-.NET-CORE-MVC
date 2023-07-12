

// Three employees in the same departement
using Company;

Employee employeeOne = new Employee()
{
    EmployeeID = 1,
    EmployeeName = "Scott",
    Email = "Scott@gmail.com"
};

Employee employeeTwo = new Employee()
{
    EmployeeID = 2,
    EmployeeName = "Adam",
    Email = "Adam@gmail.com"
};

Employee employeeThree = new Employee()
{
    EmployeeID = 3,
    EmployeeName = "Jim",
    Email = "Jim@gmail.com"
};

// Create object of departement class
Departement departement = new Departement()
{
    DepartementID = 1,
    DepartementName = "Accounting"
};

employeeOne.Departement = departement;
employeeTwo.Departement = departement;
employeeThree.Departement = departement;


// Display
Console.WriteLine("- First Employee : ");
Console.WriteLine($"Employee ID : {employeeOne.EmployeeID}");
Console.WriteLine($"Employee Name : {employeeOne.EmployeeName}");
Console.WriteLine($"Email : {employeeOne.Email}");
Console.WriteLine($"Departement ID : {employeeOne.Departement.DepartementID}");
Console.WriteLine($"Departement Name : {employeeOne.Departement.DepartementName}");

Console.WriteLine("- Second Employee : ");
Console.WriteLine($"Employee ID : {employeeTwo.EmployeeID}");
Console.WriteLine($"Employee Name : {employeeTwo.EmployeeName}");
Console.WriteLine($"Email : {employeeTwo.Email}");
Console.WriteLine($"Departement ID : {employeeTwo.Departement.DepartementID}");
Console.WriteLine($"Departement Name : {employeeTwo.Departement.DepartementName}");

Console.WriteLine("- Third Employee : ");
Console.WriteLine($"Employee ID : {employeeThree.EmployeeID}");
Console.WriteLine($"Employee Name : {employeeThree.EmployeeName}");
Console.WriteLine($"Email : {employeeThree.Email}");
Console.WriteLine($"Departement ID : {employeeThree.Departement.DepartementID}");
Console.WriteLine($"Departement Name : {employeeThree.Departement.DepartementName}");



