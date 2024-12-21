using StudentManagementApp.Models;

public class Assignment
{
    public int ID { get; set; }
    public string Title { get; set; } = string.Empty; // Default value
    public string Description { get; set; } = string.Empty; // Default value
    public DateTime DueDate { get; set; }
    public int CourseID { get; set; }
    public Course Course { get; set; } = null!; // Null-forgiving operator
}
