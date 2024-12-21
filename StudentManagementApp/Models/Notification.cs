namespace StudentManagementApp.Models
{
    public class Notification
    {
        public int ID { get; set; }
        public string Message { get; set; } = string.Empty; // Default value
        public int StudentID { get; set; }
    }
}
