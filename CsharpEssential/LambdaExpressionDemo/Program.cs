using ClassLibraryThree;

// Create object of Publisher class
Publisher publisher = new Publisher();

// Subscribe to event by using Lambda Expression
publisher.myEvent += (number1, number2) =>
{
    var sum = number1 + number2;
    return sum;
};

// Invoke the event (Executes the Lambda Expression)
Console.WriteLine(publisher.RaiseEvent(10, 20));
Console.WriteLine(publisher.RaiseEvent(100, 200));