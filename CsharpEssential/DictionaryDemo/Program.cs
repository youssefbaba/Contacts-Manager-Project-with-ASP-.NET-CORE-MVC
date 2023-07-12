
public class Program
{
    private static void Main(string[] args)
    {
        Dictionary<int, string> employees = new Dictionary<int, string>()
        {
            { 101, "Smith"},
            { 102, "John"},
            { 103, "David"}
        };

        // Foreach loop for dictionary
        foreach (KeyValuePair<int, string> item in employees)
        {
            Console.WriteLine($"{item.Key} => {item.Value}");
        }

        // Get value based on the key
        string employeeName = employees[101];
        Console.WriteLine($"Value at 1 : {employeeName}");

        // Keys
        //Dictionary<int, string>.KeyCollection keys = employees.Keys;
        var keys = employees.Keys;
        Console.WriteLine("Keys : ");
        foreach (var key in keys)
        {
            Console.WriteLine(key);
        }

        // Values
        //Dictionary<int, string>.ValueCollection values = employees.Values;
        var values = employees.Values;
        Console.WriteLine("Values : ");
        foreach (var value in values)
        {
            Console.WriteLine(value);
        }

        // Duplicate key exception
        try
        {
            employees.Add(101, "Adam");
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Duplicate key exception!");
        }

        // Remove 
        employees.Remove(102);
        foreach (KeyValuePair<int, string> item in employees)
        {
            Console.WriteLine($"{item.Key} => {item.Value}");
        }

        // ContainsKey
        bool checkKeyOne = employees.ContainsKey(101);
        Console.WriteLine($"ContainsKey : {checkKeyOne}"); // True
        bool checkKeyTwo = employees.ContainsKey(100);
        Console.WriteLine($"ContainsKey : {checkKeyTwo}"); // False

        // ContainsValue
        var checkValueOne = employees.ContainsValue("David");
        Console.WriteLine($"ContainsValue : {checkValueOne}"); // True
        var checkValueTwo = employees.ContainsValue("Adam");
        Console.WriteLine($"ContainsValue : {checkValueTwo}"); // False

        // Clear
        Console.WriteLine(employees.Count);
        employees.Clear();
        Console.WriteLine(employees.Count);
    }
}