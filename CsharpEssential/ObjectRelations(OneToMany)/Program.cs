using College;

// Create object of student class
Student student = new Student();
student.RollNumber = 1;
student.StudentName= "Adam";
student.Email = "Adam@gmail.com";

// One-to-many relation
student.Examinations = new List<Examination>();
student.Examinations.Add(new Examination()
{
    ExaminationName = "Module Test 1",
    Month = 05,
    Year = 2022,
    MaxMarks = 100,
    SecuredMarks = 87
});
student.Examinations.Add(new Examination()
{
    ExaminationName = "Module Test 2",
    Month = 07,
    Year = 2022,
    MaxMarks = 100,
    SecuredMarks = 80
});
student.Examinations.Add(new Examination()
{
    ExaminationName = "Final Test",
    Month = 09,
    Year = 2022,
    MaxMarks = 100,
    SecuredMarks = 85
});

// Display
Console.WriteLine($"Roll Number : {student.RollNumber}");
Console.WriteLine($"Student Number : {student.StudentName}");
Console.WriteLine($"Email : {student.Email}");
Console.WriteLine("Examinations : ");
foreach (var examination in student.Examinations)
{
    Console.WriteLine($"{examination.ExaminationName}, {examination.Month}, {examination.Year}, {examination.MaxMarks}, {examination.SecuredMarks}");
}


