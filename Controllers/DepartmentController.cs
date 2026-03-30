using ERPSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ERPSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetDepartment()
        {
            var departments = await _context.Departments
                .Select(d => new { d.Id, d.Name }).ToListAsync();
            return Ok(departments);
        }
        
    }
}
