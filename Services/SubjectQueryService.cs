using ERPSystem.Data;
using ERPSystem.DTOs.ClassSubject;
using Microsoft.EntityFrameworkCore;

namespace ERPSystem.Services
{
    public class SubjectQueryService : ISubjectQueryService
    {
        private readonly AppDbContext _context;

        public SubjectQueryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<ClassSubjectItemDto>> GetSubjectsByClassIdAsync(int classId)
        {
            return await _context.ClassSubjects
                .AsNoTracking()
                .Where(cs => cs.ClassId == classId)
                .Select(cs => new ClassSubjectItemDto
                {
                    ClassSubjectId = cs.Id,
                    ClassId = cs.ClassId,
                    SemesterId = cs.SemesterId,
                    SubjectId = cs.SubjectId,
                    SubjectName = cs.Subject != null ? cs.Subject.Name : string.Empty,
                    SubjectCode = cs.Subject != null ? cs.Subject.Code : null,
                    TeacherId = cs.TeacherId
                })
                .OrderBy(x => x.SemesterId)
                .ThenBy(x => x.SubjectName)
                .ToListAsync();
        }
    }
}
