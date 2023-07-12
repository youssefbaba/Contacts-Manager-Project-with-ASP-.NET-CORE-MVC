namespace ViewComponentsDemo.Models
{
    public class PersonGridModel
    {
        public string GridTitle { get; set; } = string.Empty;

        public List<Person> People { get; set; } = new List<Person>();

    }
}
