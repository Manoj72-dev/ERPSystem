using ERPSystem.DTOs.Attendance;

namespace ERPSystem.Services
{
    public interface IAttendanceQueryService
    {
        Task<StudentAttendanceDetailsDto?> GetStudentAttendanceDetailsAsync(
            int studentId,
            int? classId = null,
            int? semesterId = null,
            int? subjectId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null);

        Task<IReadOnlyList<StudentAttendanceRecordDto>> GetStudentAttendanceRecordsAsync(
            int studentId,
            int? subjectId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null);
    }
}
