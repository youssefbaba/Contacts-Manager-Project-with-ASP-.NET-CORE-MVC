using ClassLibraryTwo;

// Create object of Publisher class
Publisher publisher = new Publisher();

// Subscribe to event with anonymous method
publisher.myEvent += delegate (int number1, int number2)
{
    int sum = number1 + number2;
    Console.WriteLine(sum);
};

// Invoke the event (Executes the anonymous method)
publisher.RaiseEvent(10, 20); // 30
publisher.RaiseEvent(100, 200); // 300



