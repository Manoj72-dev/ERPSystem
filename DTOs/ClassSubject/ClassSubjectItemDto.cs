namespace ERPSystem.DTOs.ClassSubject
{
    public class ClassSubjectItemDto
    {
        public int ClassSubjectId { get; set; }
        public int ClassId { get; set; }
        public int SemesterId { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public string? SubjectCode { get; set; }
        public int TeacherId { get; set; }
    }
}
