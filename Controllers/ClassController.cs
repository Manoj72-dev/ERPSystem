using ERPSystem.Data;
using ERPSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERPSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ISubjectQueryService _subjectQueryService;

        public ClassController(AppDbContext context, ISubjectQueryService subjectQueryService)
        {
            _context = context;
            _subjectQueryService = subjectQueryService;
        }

        [HttpGet("{classId}/subjects")]
        public async Task<IActionResult> GetSubjectsByClass(int classId)
        {
            var subjects = await _subjectQueryService.GetSubjectsByClassIdAsync(classId);
            if (!subjects.Any())
                return NotFound("No subjects found for this class.");

            return Ok(subjects);
        }
    }
}
