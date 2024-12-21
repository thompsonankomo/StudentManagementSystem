namespace StudentManagementApp.Models
{
    public class Course
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Grade { get; set; }
        public int StudentID { get; set; } // Foreign Key
    }
}
