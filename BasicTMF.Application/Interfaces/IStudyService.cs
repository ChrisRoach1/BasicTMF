using BasicTMF.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BasicTMF.Application.Interfaces
{
    public interface IStudyService
    {
        Task<BasicTMF.Domain.Entities.Study?> GetStudyAsync(int studyId);
        Task<List<BasicTMF.Domain.Entities.Study>> GetAllStudiesAsync();
    }
}
