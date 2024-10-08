using BasicTMF.Application.Interfaces;
using BasicTMF.Application.Study.queries.getStudyById;
using BasicTMF.Application.Study.responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;

namespace BasicTMF.Application.Study.queries.getAllStudies
{
    internal sealed class GetAllStudiesHandler(IStudyService studyService) : IRequestHandler<GetAllStudiesQuery, List<StudyResponse>>
    {

        public async Task<List<StudyResponse>> Handle(GetAllStudiesQuery request, CancellationToken cancellationToken)
        {
            var studies = await studyService.GetAllStudiesAsync();

            var studiesResponse = studies.Adapt<List<StudyResponse>>();

            return studiesResponse;
        }
    }
}
