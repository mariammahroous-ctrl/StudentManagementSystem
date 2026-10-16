using System;
using System.Collections.Generic;

public class Student
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public List<Course> Courses { get; set; }

    public Student()
    {
        Courses = new List<Course>();
    }

    public Student(int studentId, string name, int age)
    {
        StudentId = studentId;
        Name = name;
        Age = age;
        Courses = new List<Course>();
    }

    public bool Enroll(Course course)
    {
        if (course != null && !Courses.Contains(course))
        {
            Courses.Add(course);
            return true;
        }
        return false;
    }

    public string PrintDetails()
    {
        string courseNames = "";
        if (Courses.Count > 0)
        {
            foreach (var c in Courses)
            {
                courseNames += c.Title + ", ";
            }
            courseNames = courseNames.TrimEnd(',', ' ');
        }
        else
        {
            courseNames = "No courses enrolled";
        }

        return $"ID: {StudentId}, Name: {Name}, Age: {Age}, Courses: [{courseNames}]";
    }
}

public class Instructor
{
    public int InstructorId { get; set; }
    public string Name { get; set; }
    public string Specialization { get; set; }

    public Instructor() { }

    public Instructor(int instructorId, string name, string specialization)
    {
        InstructorId = instructorId;
        Name = name;
        Specialization = specialization;
    }

    public string PrintDetails()
    {
        return $"ID: {InstructorId}, Name: {Name}, Specialization: {Specialization}";
    }
}

public class Course
{
    public int CourseId { get; set; }
    public string Title { get; set; }
    public Instructor Instructor { get; set; }

    public Course() { }

    public Course(int courseId, string title, Instructor instructor)
    {
        CourseId = courseId;
        Title = title;
        Instructor = instructor;
    }

    public string PrintDetails()
    {
        string instructorName = Instructor != null ? Instructor.Name : "No Instructor";
        return $"Course ID: {CourseId}, Title: {Title}, Instructor: {instructorName}";
    }
}

public class StudentManager
{
    public List<Student> Students { get; set; }
    public List<Course> Courses { get; set; }
    public List<Instructor> Instructors { get; set; }

    public StudentManager()
    {
        Students = new List<Student>();
        Courses = new List<Course>();
        Instructors = new List<Instructor>();
    }

    public bool AddStudent(Student student)
    {
        if (student != null && FindStudent(student.StudentId) == null)
        {
            Students.Add(student);
            return true;
        }
        return false;
    }

    public bool AddCourse(Course course)
    {
        if (course != null && FindCourse(course.CourseId) == null)
        {
            Courses.Add(course);
            return true;
        }
        return false;
    }

    public bool AddInstructor(Instructor instructor)
    {
        if (instructor != null && FindInstructor(instructor.InstructorId) == null)
        {
            Instructors.Add(instructor);
            return true;
        }
        return false;
    }

    public Student FindStudent(int studentId)
    {
        foreach (var student in Students)
        {
            if (student.StudentId == studentId)
                return student;
        }
        return null;
    }

    public Course FindCourse(int courseId)
    {
        foreach (var course in Courses)
        {
            if (course.CourseId == courseId)
                return course;
        }
        return null;
    }

    public Instructor FindInstructor(int instructorId)
    {
        foreach (var instructor in Instructors)
        {
            if (instructor.InstructorId == instructorId)
                return instructor;
        }
        return null;
    }

    public bool EnrollStudentInCourse(int studentId, int courseId)
    {
        Student student = FindStudent(studentId);
        Course course = FindCourse(courseId);

        if (student != null && course != null)
        {
            return student.Enroll(course);
        }
        return false;
    }

    public bool IsStudentEnrolledInCourse(int studentId, int courseId)
    {
        Student student = FindStudent(studentId);
        if (student != null)
        {
            foreach (var course in student.Courses)
            {
                if (course.CourseId == courseId)
                    return true;
            }
        }
        return false;
    }

    public string GetInstructorNameByCourseName(string courseTitle)
    {
        foreach (var course in Courses)
        {
            if (course.Title.Equals(courseTitle, StringComparison.OrdinalIgnoreCase))
            {
                if (course.Instructor != null)
                    return course.Instructor.Name;
            }
        }
        return "Instructor not found";
    }
}

public class HelloWorld
{
    public static void Main(string[] args)
    {
        StudentManager manager = new StudentManager();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n========== Student Management System ==========");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Add Instructor");
            Console.WriteLine("3. Add Course");
            Console.WriteLine("4. Enroll Student in Course");
            Console.WriteLine("5. Show All Students");
            Console.WriteLine("6. Show All Courses");
            Console.WriteLine("7. Show All Instructors");
            Console.WriteLine("8. Find Student by ID");
            Console.WriteLine("9. Find Course by ID");
            Console.WriteLine("10. Check if student enrolled in specific course (Bonus)");
            Console.WriteLine("11. Return instructor name by course name (Bonus)");
            Console.WriteLine("12. Exit");
            Console.Write("Enter choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter Student ID: ");
                    int sId = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Student Name: ");
                    string sName = Console.ReadLine();
                    Console.Write("Enter Student Age: ");
                    int sAge = Convert.ToInt32(Console.ReadLine());

                    if (manager.AddStudent(new Student(sId, sName, sAge)))
                        Console.WriteLine("Student added successfully!");
                    else
                        Console.WriteLine("Failed to add student.");
                    break;

                case "2":
                    Console.Write("Enter Instructor ID: ");
                    int instId = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Instructor Name: ");
                    string instName = Console.ReadLine();
                    Console.Write("Enter Specialization: ");
                    string spec = Console.ReadLine();

                    if (manager.AddInstructor(new Instructor(instId, instName, spec)))
                        Console.WriteLine("Instructor added successfully!");
                    else
                        Console.WriteLine("Failed to add instructor.");
                    break;

                case "3":
                    Console.Write("Enter Course ID: ");
                    int cId = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Course Title: ");
                    string cTitle = Console.ReadLine();
                    Console.Write("Enter Instructor ID: ");
                    int cInstId = Convert.ToInt32(Console.ReadLine());

                    Instructor inst = manager.FindInstructor(cInstId);
                    if (inst != null)
                    {
                        if (manager.AddCourse(new Course(cId, cTitle, inst)))
                            Console.WriteLine("Course added successfully!");
                        else
                            Console.WriteLine("Failed to add course.");
                    }
                    else
                    {
                        Console.WriteLine("Instructor not found!");
                    }
                    break;

                case "4":
                    Console.Write("Enter Student ID: ");
                    int stId = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Course ID: ");
                    int crsId = Convert.ToInt32(Console.ReadLine());

                    if (manager.EnrollStudentInCourse(stId, crsId))
                        Console.WriteLine("Enrolled successfully!");
                    else
                        Console.WriteLine("Enrollment failed.");
                    break;

                case "5":
                    Console.WriteLine("\n--- Students List ---");
                    if (manager.Students.Count == 0)
                        Console.WriteLine("No students.");
                    else
                        foreach (var s in manager.Students) Console.WriteLine(s.PrintDetails());
                    break;

                case "6":
                    Console.WriteLine("\n--- Courses List ---");
                    if (manager.Courses.Count == 0)
                        Console.WriteLine("No courses.");
                    else
                        foreach (var c in manager.Courses) Console.WriteLine(c.PrintDetails());
                    break;

                case "7":
                    Console.WriteLine("\n--- Instructors List ---");
                    if (manager.Instructors.Count == 0)
                        Console.WriteLine("No instructors.");
                    else
                        foreach (var i in manager.Instructors) Console.WriteLine(i.PrintDetails());
                    break;

                case "8":
                    Console.Write("Enter Student ID: ");
                    int findStId = Convert.ToInt32(Console.ReadLine());
                    Student stFound = manager.FindStudent(findStId);
                    if (stFound != null) Console.WriteLine(stFound.PrintDetails());
                    else Console.WriteLine("Student not found.");
                    break;

                case "9":
                    Console.Write("Enter Course ID: ");
                    int findCrsId = Convert.ToInt32(Console.ReadLine());
                    Course crsFound = manager.FindCourse(findCrsId);
                    if (crsFound != null) Console.WriteLine(crsFound.PrintDetails());
                    else Console.WriteLine("Course not found.");
                    break;

                case "10":
                    Console.Write("Enter Student ID: ");
                    int chkSt = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Course ID: ");
                    int chkCrs = Convert.ToInt32(Console.ReadLine());
                    if (manager.IsStudentEnrolledInCourse(chkSt, chkCrs))
                        Console.WriteLine("Yes, enrolled.");
                    else
                        Console.WriteLine("No, not enrolled.");
                    break;

                case "11":
                    Console.Write("Enter Course Title: ");
                    string cName = Console.ReadLine();
                    Console.WriteLine("Instructor: " + manager.GetInstructorNameByCourseName(cName));
                    break;

                case "12":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}
