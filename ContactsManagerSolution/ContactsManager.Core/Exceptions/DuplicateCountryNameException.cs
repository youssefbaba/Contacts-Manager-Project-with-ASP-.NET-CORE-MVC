
namespace ContactsManager.Core.Exceptions
{
    public class DuplicateCountryNameException : ArgumentException
    {
        public DuplicateCountryNameException() : base()
        {
        }

        public DuplicateCountryNameException(string? message) : base(message)
        {
        }

        public DuplicateCountryNameException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }
}
