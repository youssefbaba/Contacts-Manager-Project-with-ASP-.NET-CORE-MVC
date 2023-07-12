namespace ClassLibraryThree
{
    // Create delegate
    public delegate int MyDelegateType(int value1, int value2);

    public class Publisher
    {
        // Step 1 : Create event
        public event MyDelegateType myEvent;

        public int RaiseEvent(int a, int b)
        {
            // Step 2 : raise event
            if (myEvent != null)
            {
               return myEvent(a, b);
            }
            return 0;
        }
    }
}