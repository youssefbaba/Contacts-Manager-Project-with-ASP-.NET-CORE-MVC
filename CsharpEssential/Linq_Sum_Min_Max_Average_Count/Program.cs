using Linq_Sum_Min_Max_Average_Count;

DataSource dataSource = new DataSource();

// Sum, Min, Max, Average, Count
double min = dataSource.GetAllEmployees().Min(emp => emp.Salary);
double max = dataSource.GetAllEmployees().Max(emp => emp.Salary);
double average = dataSource.GetAllEmployees().Average(emp => emp.Salary);
double sum = dataSource.GetAllEmployees().Sum(emp => emp.Salary);
int count = dataSource.GetAllEmployees().Count();
Console.WriteLine($"Min = {min}");
Console.WriteLine($"Max = {max}");
Console.WriteLine($"Average = {average}");
Console.WriteLine($"Sum = {sum}");
Console.WriteLine($"Count = {count}");




