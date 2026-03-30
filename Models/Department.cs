using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace ERPSystem.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore] 
        public List<CourseProgram>? Programs { get; set; }
    }
}
