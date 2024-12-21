using System;
using StudentManagementApp.Data;
using StudentManagementApp.Models;
using StudentManagementApp.Menus;

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

            if (loggedInUser.Role == "admin")
            {
                var adminMenu = new AdminMenu(db);
                adminMenu.Show();
            }
            else if (loggedInUser.Role == "teacher")
            {
                var teacherMenu = new TeacherMenu(db);
                teacherMenu.Show();
            }
            else if (loggedInUser.Role == "student")
            {
                var studentMenu = new StudentMenu(db, loggedInUser);
                studentMenu.Show();
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
    }
}
