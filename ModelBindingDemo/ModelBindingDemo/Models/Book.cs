using Microsoft.AspNetCore.Mvc;

namespace ModelBindingDemo.Models
{
    public class Book
    {
        // Properties

        //[FromQuery]
        //[FromRoute]
        public int? BookId { get; set; }

        public string? Author { get; set; }

        // Methods
        public override string ToString()
        {
            return $"Book object - Book Id: {BookId}, Author: {Author}";
        }
    }
}
