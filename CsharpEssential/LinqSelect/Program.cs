using LinqSelect;

DataSource dataSource = new DataSource();

// Select
IEnumerable<int> resultOne = dataSource.GetAllEmployees().Select(emp => 10);
foreach (int value in resultOne)
{
    Console.WriteLine(value);
}

IEnumerable<string> resultTwo = dataSource.GetAllEmployees().Select(emp => "Hello Youssef Baba");
foreach (string value in resultTwo)
{
    Console.WriteLine(value);
}

List<Person> resultThree = dataSource.GetAllEmployees().Select(emp => new Person() {PersonName = emp.EmployeeName }).ToList();
foreach (Person person in resultThree)
{
    Console.WriteLine(person.PersonName);
}



