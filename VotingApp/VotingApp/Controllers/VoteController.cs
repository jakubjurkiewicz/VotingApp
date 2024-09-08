using MediatR;
using Microsoft.AspNetCore.Mvc;
using VotingApp.Application.Features.Commands.Voter;

namespace VotingApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VoteController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CastVote([FromBody] VoteCommand command)
    {
        try
        {
            await mediator.Send(command);
            return Ok(new { message = "Vote successfully cast." });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}