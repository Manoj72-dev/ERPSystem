namespace ERPSystem.DTOs.Attendance
{
    public class StudentAttendanceDetailsDto
    {
        public int StudentId { get; set; }
        public int TotalClasses { get; set; }
        public int PresentClasses { get; set; }
        public int AbsentClasses { get; set; }
        public decimal OverallPercentage { get; set; }
        public List<SubjectAttendanceSummaryDto> SubjectWise { get; set; } = new();
        public List<StudentAttendanceRecordDto> Records { get; set; } = new();
    }
}
