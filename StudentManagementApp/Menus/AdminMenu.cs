using System;
using StudentManagementApp.Data;
using StudentManagementApp.Models;

namespace StudentManagementApp.Menus
{
    public class AdminMenu
    {
        private readonly AppDbContext _db;

        public AdminMenu(AppDbContext db)
        {
            _db = db;
        }

        public void Show()
        {
            while (true)
            {
                Console.WriteLine("\n--- Admin Menu ---");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. View Students");
                Console.WriteLine("3. Add User");
                Console.WriteLine("4. Logout");
                Console.Write("Choose an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        ViewStudents();
                        break;
                    case "3":
                        AddUser();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        private void AddStudent()
        {
            Console.Write("Enter Student Name: ");
            string name = Console.ReadLine();

            var student = new Student { Name = name };
            _db.Students.Add(student);
            _db.SaveChanges();

            Console.WriteLine("Student added successfully.");
        }

        private void ViewStudents()
        {
            var students = _db.Students;
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.ID}, Name: {student.Name}");
            }
        }

        private void AddUser()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            Console.Write("Enter role (admin/teacher/student): ");
            string role = Console.ReadLine();

            var user = new User { Username = username, Password = password, Role = role };
            _db.Users.Add(user);
            _db.SaveChanges();

            Console.WriteLine("User added successfully.");
        }
    }
}
