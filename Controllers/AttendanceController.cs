using ERPSystem.Data;
using ERPSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ERPSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAttendanceQueryService _attendanceQueryService;
        private readonly ISubjectQueryService _subjectQueryService;

        public AttendanceController(
            AppDbContext context,
            IAttendanceQueryService attendanceQueryService,
            ISubjectQueryService subjectQueryService)
        {
            _context = context;
            _attendanceQueryService = attendanceQueryService;
            _subjectQueryService = subjectQueryService;
        }

        [HttpGet("student/{studentId}/details")]
        [Authorize(Roles = "Teacher,Admin,Student")]
        public async Task<IActionResult> GetSingleStudentAttendanceDetails(
            int studentId,
            [FromQuery] int? classId = null,
            [FromQuery] int? semesterId = null,
            [FromQuery] int? subjectId = null,
            [FromQuery] DateTime? fromDate = null,
            [FromQuery] DateTime? toDate = null)
        {
            var authorizedStudentId = await ResolveAuthorizedStudentIdAsync(studentId);
            if (authorizedStudentId == null)
                return Forbid();

            var details = await _attendanceQueryService.GetStudentAttendanceDetailsAsync(
                authorizedStudentId.Value,
                classId,
                semesterId,
                subjectId,
                fromDate,
                toDate);

            if (details == null)
                return NotFound("No attendance data found for this student.");

            return Ok(details);
        }

        [HttpGet("student/{studentId}/class/{classId}/overview")]
        [Authorize(Roles = "Teacher,Admin,Student")]
        public async Task<IActionResult> GetStudentAcademicOverview(
            int studentId,
            int classId,
            [FromQuery] int? semesterId = null,
            [FromQuery] int? subjectId = null,
            [FromQuery] DateTime? fromDate = null,
            [FromQuery] DateTime? toDate = null)
        {
            var authorizedStudentId = await ResolveAuthorizedStudentIdAsync(studentId);
            if (authorizedStudentId == null)
                return Forbid();

            var student = await _context.Students
                .AsNoTracking()
                .Where(s => s.Id == authorizedStudentId.Value)
                .Select(s => new { s.Id, s.FullName, s.ClassId })
                .FirstOrDefaultAsync();

            if (student == null)
                return NotFound("Student not found.");

            if (student.ClassId != classId)
                return BadRequest("The given student does not belong to the provided class.");

            var subjects = await _subjectQueryService.GetSubjectsByClassIdAsync(classId);
            var attendance = await _attendanceQueryService.GetStudentAttendanceDetailsAsync(
                authorizedStudentId.Value,
                classId,
                semesterId,
                subjectId,
                fromDate,
                toDate);

            return Ok(new
            {
                studentId = student.Id,
                studentName = student.FullName,
                classId,
                subjects,
                attendance
            });
        }

        [HttpGet("student/attendance")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetLoggedInStudentAttendance(
            [FromQuery] int? semesterId = null,
            [FromQuery] int? subjectId = null,
            [FromQuery] DateTime? fromDate = null,
            [FromQuery] DateTime? toDate = null)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("User ID not found in token.");

            var userId = int.Parse(userIdClaim.Value);
            var student = await _context.Students
                .AsNoTracking()
                .Where(s => s.UserId == userId)
                .Select(s => new { s.Id, s.ClassId })
                .FirstOrDefaultAsync();

            if (student == null)
                return NotFound("Student profile not found for this user.");

            var details = await _attendanceQueryService.GetStudentAttendanceDetailsAsync(
                student.Id,
                student.ClassId,
                semesterId,
                subjectId,
                fromDate,
                toDate);

            if (details == null)
                return NotFound("No attendance data found for this student.");

            return Ok(details);
        }

        private async Task<int?> ResolveAuthorizedStudentIdAsync(int requestedStudentId)
        {
            if (!User.IsInRole("Student"))
                return requestedStudentId;

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return null;

            var userId = int.Parse(userIdClaim.Value);
            var loggedInStudentId = await _context.Students
                .AsNoTracking()
                .Where(s => s.UserId == userId)
                .Select(s => s.Id)
                .FirstOrDefaultAsync();

            if (loggedInStudentId == 0 || loggedInStudentId != requestedStudentId)
                return null;

            return loggedInStudentId;
        }
    }
}
