using BasicTMF.Application.Interfaces;
using BasicTMF.Application.Study.responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
namespace BasicTMF.Application.Study.queries.getStudyById
{
    internal sealed class GetStudyByIdHandler(IStudyService studyService) : IRequestHandler<GetStudyByIdQuery, StudyResponse?>
    {

        public async Task<StudyResponse?> Handle(GetStudyByIdQuery request, CancellationToken cancellationToken)
        {
            var study = await studyService.GetStudyAsync(request.StudyId);

            var studyResponse = study.Adapt<StudyResponse>();

            return studyResponse;
        }
    }
}
