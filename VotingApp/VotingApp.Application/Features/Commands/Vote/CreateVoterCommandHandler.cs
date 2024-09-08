using AutoMapper;
using MediatR;
using VotingApp.Repository.Models;
using VotingApp.Repository.Repositories;

namespace VotingApp.Application.Features.Commands.Vote;

public class CreateVoterCommand(string name) : IRequest<int>
{
    public string Name { get; init; } = name;
}

public class CreateVoterCommandHandler(IRepository<VoterEntity> repository, IMapper mapper)
    : IRequestHandler<CreateVoterCommand, int>
{
    public async Task<int> Handle(CreateVoterCommand command, CancellationToken cancellationToken)
    {
        var voter = mapper.Map<VoterEntity>(command);

        var id = await repository.AddAsync(voter);
        await repository.SaveChangesAsync();
        return id;
    }
}