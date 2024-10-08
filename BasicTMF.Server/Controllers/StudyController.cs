using BasicTMF.Application.Study.queries.getAllStudies;
using BasicTMF.Application.Study.queries.getStudyById;
using BasicTMF.Application.Study.responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace BasicTMF.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize("study")]
    public class StudyController : ControllerBase
    {
        private readonly ILogger<StudyController> _logger;
        private readonly ISender _sender;

        public StudyController(ILogger<StudyController> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        /// <summary>
        /// Gets the study with the specified identifier, if it exists.
        /// </summary>
        /// <param name="studyId">The study identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The study with the specified identifier, if it exists.</returns>
        [HttpGet("{studyId:int}")]
        [ProducesResponseType(typeof(StudyResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStudy(int studyId, CancellationToken cancellationToken)
        {
            var query = new GetStudyByIdQuery(studyId);
            var study = await _sender.Send(query, cancellationToken);

            if (study == null)
            {
                return NotFound();
            }

            return Ok(study);
        }

        /// <summary>
        /// Gets all studies.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The study with the specified identifier, if it exists.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<StudyResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllStudies(CancellationToken cancellationToken)
        {
            var query = new GetAllStudiesQuery();
            var studies = await _sender.Send(query, cancellationToken);

            return Ok(studies);
        }
    }
}
