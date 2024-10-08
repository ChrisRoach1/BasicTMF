using BasicTMF.Application.Study.responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTMF.Application.Study.queries.getStudyById
{
    public sealed record GetStudyByIdQuery(int StudyId) : IRequest<StudyResponse?>;
}
