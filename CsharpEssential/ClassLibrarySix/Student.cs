
namespace College
{
    /// <summary>
    /// Represents Student
    /// </summary>
    public class Student
    {
        // Properties
        public int RollNumber { get; set; }

        public string? StudentName { get; set; }

        public string? Email { get; set; }

        public List<Examination>? Examinations { get; set; } // Stores reference of collection of Objects (One To Many Relation)
    }
}