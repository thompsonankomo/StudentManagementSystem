using System;
using System.Linq;
using StudentManagementApp.Data;
using StudentManagementApp.Models;

namespace StudentManagementApp.Menus
{
    public class StudentMenu
    {
        private readonly AppDbContext _db;
        private readonly User _user;

        public StudentMenu(AppDbContext db, User user)
        {
            _db = db;
            _user = user;
        }

        public void Show()
        {
            while (true)
            {
                Console.WriteLine("\n--- Student Menu ---");
                Console.WriteLine("1. View My Data");
                Console.WriteLine("2. Add My Data");
                Console.WriteLine("3. Delete My Data");
                Console.WriteLine("4. Submit Assignment");
                Console.WriteLine("5. View Grades");
                Console.WriteLine("6. Logout");
                Console.Write("Choose an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        ViewMyData();
                        break;
                    case "2":
                        AddMyData();
                        break;
                    case "3":
                        DeleteMyData();
                        break;
                    case "4":
                        SubmitAssignment();
                        break;
                    case "5":
                        ViewGrades();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        private void ViewMyData()
        {
            var student = _db.Students.FirstOrDefault(s => s.Name == _user.Username);
            if (student == null)
            {
                Console.WriteLine("No data found.");
                return;
            }

            Console.WriteLine($"ID: {student.ID}, Name: {student.Name}");
            Console.WriteLine("Courses: " + string.Join(", ", student.Courses.Select(c => c.Name)));
            Console.WriteLine("Notifications: " + string.Join(", ", student.Notifications.Select(n => n.Message)));
        }

        private void AddMyData()
        {
            Console.Write("Enter Student Name: ");
            string name = Console.ReadLine();

            var student = new Student { Name = name };
            _db.Students.Add(student);
            _db.SaveChanges();

            Console.WriteLine("Student added successfully.");
        }

        private void DeleteMyData()
        {
            var student = _db.Students.FirstOrDefault(s => s.Name == _user.Username);
            if (student == null)
            {
                Console.WriteLine("No data found to delete.");
                return;
            }

            _db.Students.Remove(student);
            _db.SaveChanges();

            Console.WriteLine("Student data deleted successfully.");
        }

        private void SubmitAssignment()
        {
            Console.Write("Enter Assignment ID: ");
            int assignmentId = int.Parse(Console.ReadLine());

            var assignment = _db.Assignments.Find(assignmentId);
            if (assignment == null)
            {
                Console.WriteLine("Assignment not found.");
                return;
            }

            Console.Write("Enter Score: ");
            double score = double.Parse(Console.ReadLine());

            var grade = new Grade
            {
                AssignmentID = assignmentId,
                StudentID = _db.Students.First(s => s.Name == _user.Username).ID,
                Score = score
            };

            _db.Grades.Add(grade);
            _db.SaveChanges();

            Console.WriteLine("Assignment submitted successfully.");
        }

        private void ViewGrades()
        {
            var student = _db.Students.FirstOrDefault(s => s.Name == _user.Username);
            if (student == null)
            {
                Console.WriteLine("No data found.");
                return;
            }

            var grades = _db.Grades.Where(g => g.StudentID == student.ID).ToList();
            foreach (var grade in grades)
            {
                Console.WriteLine($"Assignment: {grade.Assignment.Title}, Score: {grade.Score}, Feedback: {grade.Feedback}");
            }
        }
    }
}
