using System.Collections.Generic;

namespace StudentManagementApp.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();
        public List<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
