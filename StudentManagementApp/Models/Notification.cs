
namespace StudentManagementApp.Models
{
    public class Notification
    {
        public int ID { get; set; }
        public string Message { get; set; }
        public int StudentID { get; set; } // Foreign Key
    }
}
