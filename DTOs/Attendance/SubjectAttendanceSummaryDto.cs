namespace ERPSystem.DTOs.Attendance
{
    public class SubjectAttendanceSummaryDto
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public string? SubjectCode { get; set; }
        public int TotalClasses { get; set; }
        public int PresentClasses { get; set; }
        public int AbsentClasses { get; set; }
        public decimal Percentage { get; set; }
    }
}
