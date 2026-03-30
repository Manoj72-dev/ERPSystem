using ERPSystem.Data;
using ERPSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BCrypt.Net;
namespace ERPSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeedController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SeedController(AppDbContext context)
        {
            _context = context;
        }

        // ===================== DEPARTMENT =====================
        [HttpPost("department")]
        public async Task<IActionResult> AddDepartment([FromBody] Department dept)
        {
            _context.Departments.Add(dept);
            await _context.SaveChangesAsync();
            return Ok(dept);
        }

        // ===================== PROGRAM =====================
        [HttpPost("program")]
        public async Task<IActionResult> AddProgram([FromBody] CourseProgram program)
        {
            _context.Programs.Add(program);
            await _context.SaveChangesAsync();
            return Ok(program);
        }

        // ===================== CLASS =====================
        [HttpPost("class")]
        public async Task<IActionResult> AddClass([FromBody] Class semclass)
        {
            _context.Classes.Add(semclass);
            await _context.SaveChangesAsync();
            return Ok(semclass);
        }

        // ===================== SEMESTER =====================
        [HttpPost("semester")]
        public async Task<IActionResult> AddSemester([FromBody] Semester sem)
        {
            _context.Semesters.Add(sem);
            await _context.SaveChangesAsync();
            return Ok(sem);
        }

        // ===================== SUBJECT =====================
        [HttpPost("subject")]
        public async Task<IActionResult> AddSubject([FromBody] Subject subject)
        {
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
            return Ok(subject);
        }

        // ===================== USER =====================
        [HttpPost("user")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }
        // ===================== TEACHER =====================
        [HttpPost("teacher")]
        public async Task<IActionResult> AddTeacher([FromBody] Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
            return Ok(teacher);
        }
        // ===================== STUDENT =====================
        [HttpPost("student")]
        public async Task<IActionResult> AddStudent([FromBody] StudentDto dto)
        {

            var student = new Student
            {
                UserId = dto.UserId,
                FullName = dto.FullName,
                UniversityRollNo = dto.UniversityRollNo,
                ClassRollNo = dto.ClassRollNo,
                ClassId = dto.ClassId,
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return Ok(student);
        }

        // ===================== ATTENDANCE =====================
        [HttpPost("attendance")]
        public async Task<IActionResult> MarkAttendance([FromBody] Attendance attendance)
        {
            attendance.Date = DateTime.Now;

            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();
            return Ok(attendance);
        }

        // ===================== CLASS SUBJETS =====================
        [HttpPost("class-subject")]
        public async Task<IActionResult> AddClassSubject([FromBody] ClassSemesterSubject classSubject)
        {
            _context.ClassSubjects.Add(classSubject);
            await _context.SaveChangesAsync();
            return Ok(classSubject);
        }
    }
    // ===================== DTO =====================
    public class StudentDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public int UniversityRollNo { get; set; }
        public int ClassRollNo { get; set; }
        public int ClassId { get; set; }
    }
}