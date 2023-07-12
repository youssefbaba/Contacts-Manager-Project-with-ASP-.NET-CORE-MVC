

using ClassLibraryEight;

// Create object of Manager class
Manager manager = new Manager(1001, "Scott", "New York", "Marketing");
Console.WriteLine(manager.EmployeeID);
Console.WriteLine(manager.EmployeeName);
Console.WriteLine(manager.Location);
Console.WriteLine(manager.DepartementName);
Console.WriteLine(manager.GetHealthInssuranceAmount());

// Create object of SalesMan class
SalesMan salesMan = new SalesMan(600, "Adam", "New York", "East");
Console.WriteLine(salesMan.EmployeeID);
Console.WriteLine(salesMan.EmployeeName);
Console.WriteLine(salesMan.Location);
Console.WriteLine(salesMan.Region);
Console.WriteLine(salesMan.GetHealthInssuranceAmount());


