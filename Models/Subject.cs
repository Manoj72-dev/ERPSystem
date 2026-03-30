using System.Text.Json.Serialization;

namespace ERPSystem.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Credits { get; set; }
        [JsonIgnore]
        public List<ClassSemesterSubject>? ClassSubjects { get; set; }

    }
}
