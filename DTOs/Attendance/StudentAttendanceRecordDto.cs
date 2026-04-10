namespace ERPSystem.DTOs.Attendance
{
    public class StudentAttendanceRecordDto
    {
        public int AttendanceId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
        public int ClassSubjectId { get; set; }
        public int SemesterId { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public string? SubjectCode { get; set; }
        public int TeacherId { get; set; }
    }
}
