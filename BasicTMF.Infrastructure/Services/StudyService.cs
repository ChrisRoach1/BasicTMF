using BasicTMF.Application.Interfaces;
using BasicTMF.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasicTMF.Infrastructure.Services
{
    public class StudyService : IStudyService
    {
        private readonly ApplicationDbContext _context;

        public StudyService(ApplicationDbContext context) => _context = context;

        public async Task<Study?> GetStudyAsync(int studyId)
        {
            var study = await _context.Study.FirstOrDefaultAsync(x => x.ID == studyId);

            return study;
        }

        public async Task<List<Study>> GetAllStudiesAsync()
        {

            return await _context.Study.ToListAsync();

        }
    }
}
