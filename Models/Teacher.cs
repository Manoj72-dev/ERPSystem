using System.Text.Json.Serialization;

namespace ERPSystem.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        [JsonIgnore]
        public List<ClassSemesterSubject>? ClassSubjects { get; set; }
    }
}
