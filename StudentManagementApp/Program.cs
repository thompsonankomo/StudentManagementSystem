using System;
using System.Linq;
using StudentManagementApp.Data;
using StudentManagementApp.Models;

namespace StudentManagementApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new AppDbContext();
            Console.WriteLine("Welcome to Student Management App");

            User loggedInUser = Login(db);
            if (loggedInUser == null) return;

            while (true)
            {
                if (loggedInUser.Role == "admin")
                    AdminMenu(db);
                else if (loggedInUser.Role == "teacher")
                    TeacherMenu(db);
                else if (loggedInUser.Role == "student")
                    StudentMenu(db, loggedInUser);
            }
        }

        static User Login(AppDbContext db)
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                Console.WriteLine($"Login successful! Role: {user.Role}");
                return user;
            }

            Console.WriteLine("Login failed.");
            return null;
        }

        static void AdminMenu(AppDbContext db)
        {
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. View Students");
            Console.WriteLine("3. Add User");
            Console.WriteLine("4. Logout");
            Console.Write("Choose an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    AddStudent(db);
                    break;
                case "2":
                    ViewStudents(db);
                    break;
                case "3":
                    AddUser(db);
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }

        static void TeacherMenu(AppDbContext db)
        {
            Console.WriteLine("1. View Students");
            Console.WriteLine("2. Update Grades");
            Console.WriteLine("3. Add Notification");
            Console.WriteLine("4. Logout");
            Console.Write("Choose an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    ViewStudents(db);
                    break;
                case "2":
                    UpdateGrades(db);
                    break;
                case "3":
                    AddNotification(db);
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }

        static void StudentMenu(AppDbContext db, User user)
        {
            Console.WriteLine("1. View My Data");
            Console.WriteLine("2. Logout");
            Console.Write("Choose an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    ViewStudentData(db, user.Username);
                    break;
                case "2":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }

        static void AddStudent(AppDbContext db)
        {
            Console.Write("Enter Student Name: ");
            string name = Console.ReadLine();

            var student = new Student { Name = name };
            db.Students.Add(student);
            db.SaveChanges();
            Console.WriteLine("Student added successfully.");
        }

        static void ViewStudents(AppDbContext db)
        {
            var students = db.Students.ToList();
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.ID}, Name: {student.Name}");
            }
        }

        static void AddUser(AppDbContext db)
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            Console.Write("Enter role (admin/teacher/student): ");
            string role = Console.ReadLine();

            var user = new User { Username = username, Password = password, Role = role };
            db.Users.Add(user);
            db.SaveChanges();
            Console.WriteLine("User added successfully.");
        }

        static void UpdateGrades(AppDbContext db)
        {
            Console.Write("Enter Student ID: ");
            int studentId = int.Parse(Console.ReadLine());
            var student = db.Students.Find(studentId);

            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            Console.Write("Enter Course Name: ");
            string courseName = Console.ReadLine();
            Console.Write("Enter Grade: ");
            double grade = double.Parse(Console.ReadLine());

            student.Courses.Add(new Course { Name = courseName, Grade = grade });
            db.SaveChanges();
            Console.WriteLine("Grade updated successfully.");
        }

        static void AddNotification(AppDbContext db)
        {
            Console.Write("Enter Student ID: ");
            int studentId = int.Parse(Console.ReadLine());
            var student = db.Students.Find(studentId);

            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            Console.Write("Enter Notification: ");
            string message = Console.ReadLine();

            student.Notifications.Add(new Notification { Message = message });
            db.SaveChanges();
            Console.WriteLine("Notification added successfully.");
        }

        static void ViewStudentData(AppDbContext db, string username)
        {
            var student = db.Students.FirstOrDefault(s => s.Name == username);
            if (student == null)
            {
                Console.WriteLine("Student data not found.");
                return;
            }

            Console.WriteLine($"ID: {student.ID}, Name: {student.Name}");
            Console.WriteLine("Courses: " + string.Join(", ", student.Courses.Select(c => $"{c.Name} ({c.Grade})")));
            Console.WriteLine("Notifications: " + string.Join(", ", student.Notifications.Select(n => n.Message)));
        }
    }
}
