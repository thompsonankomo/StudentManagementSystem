using System;
using System.Linq;
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
                Console.WriteLine("4. View Users");
                Console.WriteLine("5. Delete User");
                Console.WriteLine("6. Update User");
                Console.WriteLine("7. Logout");
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
                        ViewUsers();
                        break;
                    case "5":
                        DeleteUser();
                        break;
                    case "6":
                        UpdateUser();
                        break;
                    case "7":
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

        private void ViewUsers()
        {
            var users = _db.Users;
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.ID}, Username: {user.Username}, Role: {user.Role}");
            }
        }

        private void DeleteUser()
        {
            Console.Write("Enter the ID of the user to delete: ");
            if (int.TryParse(Console.ReadLine(), out int userId))
            {
                var user = _db.Users.Find(userId);
                if (user != null)
                {
                    _db.Users.Remove(user);
                    _db.SaveChanges();
                    Console.WriteLine("User deleted successfully.");
                }
                else
                {
                    Console.WriteLine("User not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }

        private void UpdateUser()
        {
            Console.Write("Enter the ID of the user to update: ");
            if (int.TryParse(Console.ReadLine(), out int userId))
            {
                var user = _db.Users.Find(userId);
                if (user != null)
                {
                    Console.Write("Enter new username (leave blank to keep current): ");
                    string newUsername = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newUsername))
                    {
                        user.Username = newUsername;
                    }

                    Console.Write("Enter new password (leave blank to keep current): ");
                    string newPassword = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newPassword))
                    {
                        user.Password = newPassword;
                    }

                    Console.Write("Enter new role (leave blank to keep current): ");
                    string newRole = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newRole))
                    {
                        user.Role = newRole;
                    }

                    _db.SaveChanges();
                    Console.WriteLine("User updated successfully.");
                }
                else
                {
                    Console.WriteLine("User not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }
    }
}
