using ERPSystem.Data;
using ERPSystem.DTOs.Attendance;
using Microsoft.EntityFrameworkCore;

namespace ERPSystem.Services
{
    public class AttendanceQueryService : IAttendanceQueryService
    {
        private readonly AppDbContext _context;

        public AttendanceQueryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<StudentAttendanceDetailsDto?> GetStudentAttendanceDetailsAsync(
            int studentId,
            int? classId = null,
            int? semesterId = null,
            int? subjectId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            var records = await BuildBaseQuery(studentId, classId, semesterId, subjectId, fromDate, toDate)
                .Select(a => new StudentAttendanceRecordDto
                {
                    AttendanceId = a.Id,
                    Date = a.Date,
                    IsPresent = a.IsPresent,
                    ClassSubjectId = a.ClassSubjectId,
                    SemesterId = a.ClassSubject != null ? a.ClassSubject.SemesterId : 0,
                    SubjectId = a.ClassSubject != null ? a.ClassSubject.SubjectId : 0,
                    SubjectName = a.ClassSubject != null && a.ClassSubject.Subject != null
                        ? a.ClassSubject.Subject.Name
                        : string.Empty,
                    SubjectCode = a.ClassSubject != null && a.ClassSubject.Subject != null
                        ? a.ClassSubject.Subject.Code
                        : null,
                    TeacherId = a.ClassSubject != null ? a.ClassSubject.TeacherId : 0
                })
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            if (!records.Any())
            {
                return null;
            }

            var totalClasses = records.Count;
            var presentClasses = records.Count(r => r.IsPresent);
            var absentClasses = totalClasses - presentClasses;

            var subjectWise = records
                .GroupBy(r => new { r.SubjectId, r.SubjectName, r.SubjectCode })
                .Select(g =>
                {
                    var subjectTotal = g.Count();
                    var subjectPresent = g.Count(x => x.IsPresent);
                    var subjectAbsent = subjectTotal - subjectPresent;

                    return new SubjectAttendanceSummaryDto
                    {
                        SubjectId = g.Key.SubjectId,
                        SubjectName = g.Key.SubjectName,
                        SubjectCode = g.Key.SubjectCode,
                        TotalClasses = subjectTotal,
                        PresentClasses = subjectPresent,
                        AbsentClasses = subjectAbsent,
                        Percentage = subjectTotal == 0 ? 0 : Math.Round((decimal)subjectPresent * 100 / subjectTotal, 2)
                    };
                })
                .OrderBy(x => x.SubjectName)
                .ToList();

            return new StudentAttendanceDetailsDto
            {
                StudentId = studentId,
                TotalClasses = totalClasses,
                PresentClasses = presentClasses,
                AbsentClasses = absentClasses,
                OverallPercentage = totalClasses == 0 ? 0 : Math.Round((decimal)presentClasses * 100 / totalClasses, 2),
                SubjectWise = subjectWise,
                Records = records
            };
        }

        public async Task<IReadOnlyList<StudentAttendanceRecordDto>> GetStudentAttendanceRecordsAsync(
            int studentId,
            int? subjectId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            return await BuildBaseQuery(studentId, null, null, subjectId, fromDate, toDate)
                .Select(a => new StudentAttendanceRecordDto
                {
                    AttendanceId = a.Id,
                    Date = a.Date,
                    IsPresent = a.IsPresent,
                    ClassSubjectId = a.ClassSubjectId,
                    SemesterId = a.ClassSubject != null ? a.ClassSubject.SemesterId : 0,
                    SubjectId = a.ClassSubject != null ? a.ClassSubject.SubjectId : 0,
                    SubjectName = a.ClassSubject != null && a.ClassSubject.Subject != null
                        ? a.ClassSubject.Subject.Name
                        : string.Empty,
                    SubjectCode = a.ClassSubject != null && a.ClassSubject.Subject != null
                        ? a.ClassSubject.Subject.Code
                        : null,
                    TeacherId = a.ClassSubject != null ? a.ClassSubject.TeacherId : 0
                })
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        private IQueryable<Models.Attendance> BuildBaseQuery(
            int studentId,
            int? classId,
            int? semesterId,
            int? subjectId,
            DateTime? fromDate,
            DateTime? toDate)
        {
            var query = _context.Attendances
                .AsNoTracking()
                .Where(a => a.StudentId == studentId);

            if (classId.HasValue)
            {
                query = query.Where(a => a.ClassSubject != null && a.ClassSubject.ClassId == classId.Value);
            }

            if (semesterId.HasValue)
            {
                query = query.Where(a => a.ClassSubject != null && a.ClassSubject.SemesterId == semesterId.Value);
            }

            if (subjectId.HasValue)
            {
                query = query.Where(a => a.ClassSubject != null && a.ClassSubject.SubjectId == subjectId.Value);
            }

            if (fromDate.HasValue)
            {
                var from = fromDate.Value.Date;
                query = query.Where(a => a.Date >= from);
            }

            if (toDate.HasValue)
            {
                var to = toDate.Value.Date;
                query = query.Where(a => a.Date <= to);
            }

            return query;
        }
    }
}
