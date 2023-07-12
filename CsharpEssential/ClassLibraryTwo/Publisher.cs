namespace ClassLibraryTwo
{
    // Create delegate
    public delegate void MyDelegateType(int value1, int value2);

    public class Publisher
    {
        // Step 1 : Create event
        public event MyDelegateType myEvent;

        public void RaiseEvent(int a, int b)
        {
            // Step 2 : raise event
            if (myEvent != null)
            {
                myEvent(a, b);
            }
        }
    }
}