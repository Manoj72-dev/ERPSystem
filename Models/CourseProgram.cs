using System.Text.Json.Serialization;

namespace ERPSystem.Models
{
    public class CourseProgram
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public int DepartmentId {get; set;}
        public Department? Department { get; set; }
        [JsonIgnore]
        public List<Semester>? Semesters { get; set; }
    }
}
