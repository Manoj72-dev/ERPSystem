using System.Text.Json.Serialization;

namespace ERPSystem.Models
{
    public class Semester
    {
        public int Id { get; set; }
        public string SemesterName { get; set; }
        public int ProgramId { get; set; }
        public CourseProgram? Program { get; set; }
        [JsonIgnore]
        public List<Class>? Class { get; set; }
    }
}
