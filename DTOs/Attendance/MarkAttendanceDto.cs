namespace ERPSystem.DTOs.Attendance
{
    public class MarkAttendanceDto
    {
        public int ClassSubjectId { get; set; }
        public DateTime Date { get; set; }
        public List<StudentAttendanceDto> Students { get; set; } = new();
    }
}
