using System.Net.NetworkInformation;

namespace College
{
    public class Student
    {
        // Properties
        public int RollNumber { get; set; }

        public string? StudentName { get; set; }

        public string? Email { get; set; }

        public Branch? Branch { get; set; }  // Reference variable of object of Branch class ( One To One Relation )

    }
}