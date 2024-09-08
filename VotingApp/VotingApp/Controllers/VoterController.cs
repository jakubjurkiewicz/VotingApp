using MediatR;
using Microsoft.AspNetCore.Mvc;
using VotingApp.Application.Features.Commands.Vote;
using VotingApp.Application.Features.Queries.Voter;
using VotingApp.Domain.Models;

namespace VotingApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VoterController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Voter>>> GetAllVoters()
    {
        var voters = await mediator.Send(new GetAllVotersQuery());
        return Ok(voters);
    }

    [HttpPost]
    public async Task<IActionResult> AddVoter([FromBody] CreateVoterCommand command)
    {
        var voterId = await mediator.Send(command);

        return Ok(new { Id = voterId });
    }
}