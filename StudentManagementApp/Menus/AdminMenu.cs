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

        public void ShowLogin()
        {
            Console.WriteLine("\n--- Login ---");
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            var user = _db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                Console.WriteLine($"Welcome, {user.Role} {user.Username}!");
                if (user.Role == "admin")
                {
                    Show();
                }
                else if (user.Role == "student")
                {
                    ShowStudentMenu();
                }
                else
                {
                    Console.WriteLine("Unknown role.");
                }
            }
            else
            {
                Console.WriteLine("Invalid username or password.");
            }
        }

        public void Show()
        {
            while (true)
            {
                Console.WriteLine("\n--- Admin Menu ---");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. View Students");
                Console.WriteLine("3. Search Student");
                Console.WriteLine("4. Add Course");
                Console.WriteLine("5. View Courses");
                Console.WriteLine("6. Search Course");
                Console.WriteLine("7. Add User");
                Console.WriteLine("8. View Users");
                Console.WriteLine("9. Delete User");
                Console.WriteLine("10. Update User");
                Console.WriteLine("11. Logout");
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
                        SearchStudent();
                        break;
                    case "4":
                        AddCourse();
                        break;
                    case "5":
                        ViewCourses();
                        break;
                    case "6":
                        SearchCourse();
                        break;
                    case "7":
                        AddUser();
                        break;
                    case "8":
                        ViewUsers();
                        break;
                    case "9":
                        DeleteUser();
                        break;
                    case "10":
                        UpdateUser();
                        break;
                    case "11":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        private void ShowStudentMenu()
        {
            Console.WriteLine("\n--- Student Menu ---");
            Console.WriteLine("This feature is under development.");
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

        
       private void SearchStudent()
{
    Console.Write("Enter Student Name to search: ");
    string searchName = Console.ReadLine();

    // Use AsEnumerable() to switch to client-side evaluation for the Contains method
    var students = _db.Students
                      .AsEnumerable()  // Forces client-side evaluation
                      .Where(s => s.Name.Contains(searchName, StringComparison.OrdinalIgnoreCase))
                      .ToList();

    if (students.Any())
    {
        Console.WriteLine("Students found:");
        foreach (var student in students)
        {
            Console.WriteLine($"ID: {student.ID}, Name: {student.Name}");
        }
    }
    else
    {
        Console.WriteLine("No students found with that name.");
    }
}

        private void AddCourse()
        {
            Console.Write("Enter Course Name: ");
            string courseName = Console.ReadLine();

            var course = new Course { Name = courseName };
            _db.Courses.Add(course);
            _db.SaveChanges();

            Console.WriteLine("Course added successfully.");
        }

        private void ViewCourses()
        {
            var courses = _db.Courses;
            foreach (var course in courses)
            {
                Console.WriteLine($"ID: {course.ID}, Name: {course.Name}");
            }
        }

      

private void SearchCourse()
{
    Console.Write("Enter Course Name to search: ");
    string searchCourse = Console.ReadLine();

    var courses = _db.Courses
                     .Where(c => c.Name.Contains(searchCourse, StringComparison.OrdinalIgnoreCase))
                     .ToList(); // Force evaluation in memory

    if (courses.Any())
    {
        Console.WriteLine("Courses found:");
        foreach (var course in courses)
        {
            Console.WriteLine($"ID: {course.ID}, Name: {course.Name}");
        }
    }
    else
    {
        Console.WriteLine("No courses found with that name.");
    }
}

        private void AddUser()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            Console.Write("Enter role (admin/student): ");
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
