using ERPSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERPSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemesterController : ControllerBase
    {
        private readonly AppDbContext _context;
        public SemesterController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("by-program/{programid}")]
        public async Task<IActionResult> GetSemester(int programid)
        {
            var semesters = await _context.Semesters
                .Where(s => s.ProgramId == programid).ToListAsync();
            return Ok(semesters);
        }

    }
}
