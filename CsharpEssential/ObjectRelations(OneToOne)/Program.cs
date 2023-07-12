using College;


// Student class's object
Student student = new Student();
student.RollNumber = 123;
student.StudentName = "Scott";
student.Email = "Scott@gmail.com";


// One-to-one relation
student.Branch = new Branch();
student.Branch.BranchName = "Computer Science";
student.Branch.NumberOfSemesters = 8;

// Display
Console.WriteLine(student.RollNumber);
Console.WriteLine(student.StudentName);
Console.WriteLine(student.Email);
Console.WriteLine(student.Branch.BranchName);
Console.WriteLine(student.Branch.NumberOfSemesters);
