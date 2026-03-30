using ERPSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ERPSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
       private readonly AppDbContext _context;
        public ProgramController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var programs = _context.Programs.ToList();
            return Ok(programs);
        }
        [HttpGet("by-department/{Departmentid}")]
        public async Task<IActionResult> GetPrgramByDepartmentId(int Departmentid) 
        {
            var programs = await _context.Programs.Where(p => p.DepartmentId == Departmentid).ToListAsync();
            return Ok(programs);
        }

    }
}
