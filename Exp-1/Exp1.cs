using System;

namespace StudentAdmissionManagement
{
    class Student
    {
        // Private Data Members
        private int studentId;
        private string studentName;
        private int age;
        private string course;

        // Constructor
        public Student(int id, string name, int age, string course)
        {
            studentId = id;
            studentName = name;
            this.age = age;
            this.course = course;
        }

        // Display Student Details
        public void DisplayStudent()
        {
            Console.WriteLine("\n===== Student Admission Details =====");
            Console.WriteLine("Student ID   : " + studentId);
            Console.WriteLine("Student Name : " + studentName);
            Console.WriteLine("Age          : " + age);
            Console.WriteLine("Course       : " + course);
        }

        // Update Course
        public void UpdateCourse(string newCourse)
        {
            course = newCourse;
            Console.WriteLine("\nCourse Updated Successfully!");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int id, age;
            string name, course, newCourse;

            // Taking Input Dynamically
            Console.WriteLine("===== Student Admission Management System =====");

            Console.Write("Enter Student ID: ");
            id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Student Name: ");
            name = Console.ReadLine();

            Console.Write("Enter Age: ");
            age = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Course: ");
            course = Console.ReadLine();

            // Creating Object
            Student student = new Student(id, name, age, course);

            // Display Details
            student.DisplayStudent();

            // Update Course
            Console.Write("\nEnter New Course: ");
            newCourse = Console.ReadLine();

            student.UpdateCourse(newCourse);

            // Display Updated Details
            student.DisplayStudent();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
