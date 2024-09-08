using MediatR;
using Microsoft.AspNetCore.Mvc;
using VotingApp.Application.Features.Commands.Candidate;
using VotingApp.Application.Features.Queries.Candidate;
using VotingApp.Domain.Models;

namespace VotingApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CandidateController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Candidate>>> GetAllCandidates()
    {
        var candidates = await mediator.Send(new GetAllCandidatesQuery());
        return Ok(candidates);
    }

    [HttpPost]
    public async Task<IActionResult> AddCandidate([FromBody] CreateCandidateCommand command)
    {
        var candidateId = await mediator.Send(command);

        return Ok(new { Id = candidateId });
    }
}