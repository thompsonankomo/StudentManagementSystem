namespace StudentManagementApp.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty; // Default value
        public List<Course> Courses { get; set; } = new List<Course>(); // Initialize list
        public List<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
