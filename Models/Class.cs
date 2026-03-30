using System.Text.Json.Serialization;

namespace ERPSystem.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public int Capacity { get; set; }
        public int SemesterId { get; set; }
        public Semester? Semester { get; set; }
        [JsonIgnore]
        public List<Student>? Students { get; set; }
        [JsonIgnore]
        public List<ClassSemesterSubject>? ClassSubjects { get; set; }

    }
}
