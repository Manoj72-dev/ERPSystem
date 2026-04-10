using ERPSystem.DTOs.ClassSubject;

namespace ERPSystem.Services
{
    public interface ISubjectQueryService
    {
        Task<IReadOnlyList<ClassSubjectItemDto>> GetSubjectsByClassIdAsync(int classId);
    }
}
