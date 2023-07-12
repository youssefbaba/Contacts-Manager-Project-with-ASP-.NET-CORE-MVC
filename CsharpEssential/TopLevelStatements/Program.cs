using System;
using System.IO;

// Top level statements

string fullName = "Youssef Baba";  // local variable
bool check = true;
void MethodOne()  // local function
{
    if (check)
    {
        Console.WriteLine($"Good morning {fullName}");
    }
    else
    {
        Console.WriteLine($"Good afternoon {fullName}");
    }
}
MethodOne();

namespace namespaceOne
{
	class Sample
	{
        // Method
        public void MethodTwo()
        {
            //string str = fullName;  // Error : we can't use local variable or local function that is declared in this context (because it's scope is local) 
            //MethodOne(); // Error : we can't use local variable or local function that is declared in this context (because it's scope is local) 
        }
    }
}


//Console.WriteLine("Hello World");  // Error : Top level statements must precede namespace and type declarations => so you can't write namespaces or type declartions above top level statements

//public class Program // reserved keyword
public class Program1
{
    static void Main()
    {
        Console.WriteLine("My own Main method");
    }
}


