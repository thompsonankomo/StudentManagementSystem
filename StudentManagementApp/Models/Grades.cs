namespace StudentManagementApp.Models
{
    public class Grade
    {
        public int ID { get; set; }
        public int AssignmentID { get; set; }
        public Assignment Assignment { get; set; } = null!; // Null-forgiving operator
        public double Score { get; set; }
        public string Feedback { get; set; } = string.Empty; // Default value
        public int StudentID { get; set; }
        public Student Student { get; set; } = null!;
    }
}
