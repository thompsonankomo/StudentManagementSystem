using System;
using System.Linq;
using StudentManagementApp.Data;
using StudentManagementApp.Models;

namespace StudentManagementApp.Menus
{
    public class TeacherMenu
    {
        private readonly AppDbContext _db;

        public TeacherMenu(AppDbContext db)
        {
            _db = db;
        }

        public void Show()
        {
            while (true)
            {
                Console.WriteLine("\n--- Teacher Menu ---");
                Console.WriteLine("1. View Students");
                Console.WriteLine("2. Grade Assignment");
                Console.WriteLine("3. Add Notification");
                Console.WriteLine("4. Add Assignment");
                Console.WriteLine("5. Logout");
                Console.Write("Choose an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        ViewStudents();
                        break;
                    case "2":
                        GradeAssignment();
                        break;
                    case "3":
                        AddNotification();
                        break;
                    case "4":
                        AddAssignment();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        private void ViewStudents()
        {
            var students = _db.Students;
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.ID}, Name: {student.Name}");
            }
        }

        private void GradeAssignment()
        {
            Console.Write("Enter Assignment ID: ");
            int assignmentId = int.Parse(Console.ReadLine());

            var assignment = _db.Assignments.Find(assignmentId);
            if (assignment == null)
            {
                Console.WriteLine("Assignment not found.");
                return;
            }

            Console.Write("Enter Student ID: ");
            int studentId = int.Parse(Console.ReadLine());

            var student = _db.Students.Find(studentId);
            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            Console.Write("Enter Grade: ");
            double grade = double.Parse(Console.ReadLine());

            Console.Write("Enter Feedback: ");
            string feedback = Console.ReadLine();

            var gradeEntry = new Grade
            {
                AssignmentID = assignmentId,
                StudentID = studentId,
                Score = grade,
                Feedback = feedback
            };

            _db.Grades.Add(gradeEntry);
            _db.SaveChanges();

            Console.WriteLine("Grade and feedback added successfully.");
        }

        private void AddNotification()
        {
            Console.Write("Enter Student ID: ");
            int studentId = int.Parse(Console.ReadLine());

            var student = _db.Students.Find(studentId);
            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            Console.Write("Enter Notification: ");
            string message = Console.ReadLine();

            student.Notifications.Add(new Notification { Message = message });
            _db.SaveChanges();

            Console.WriteLine("Notification added successfully.");
        }

        private void AddAssignment()
        {
            Console.Write("Enter Course ID: ");
            int courseId = int.Parse(Console.ReadLine());

            var course = _db.Courses.Find(courseId);
            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            Console.Write("Enter Assignment Title: ");
            string title = Console.ReadLine();

            Console.Write("Enter Assignment Description: ");
            string description = Console.ReadLine();

            Console.Write("Enter Due Date (yyyy-MM-dd): ");
            DateTime dueDate = DateTime.Parse(Console.ReadLine());

            var assignment = new Assignment
            {
                Title = title,
                Description = description,
                DueDate = dueDate,
                CourseID = courseId
            };

            _db.Assignments.Add(assignment);
            _db.SaveChanges();

            Console.WriteLine("Assignment added successfully.");
        }
    }
}
