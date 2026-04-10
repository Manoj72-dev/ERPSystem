using ERPSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERPSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;
        public StudentController(AppDbContext context)
        {
            _context = context;
        }
        /*[HttpGet("by-class/{classId}")]
        public async Task<IActionResult> GetStudentByClass(int classId)
        {
            var students = await _context.Students.Where(s  => s.Id == classId)
                .OrderBy(s => s.ClassRollNo)
                .Select( s=> new
                {
                    id = s.Id,
                    name = s.FullName,
                    rollno = s.ClassRollNo
                }).ToListAsync();
            if (!students.Any())
                return NotFound("No students found");
            return Ok(students);
        }

        [HttpGet("dashboard")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetStudentDashboard()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized("User ID not found in token");

            int userId = int.Parse(userIdClaim.Value);
            
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.UserId == userId);

            if (student == null)
                return NotFound("Student not found");

            return Ok(student);
        }
        */
    }
}
