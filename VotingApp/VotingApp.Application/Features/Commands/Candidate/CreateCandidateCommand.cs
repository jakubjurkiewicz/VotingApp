using AutoMapper;
using MediatR;
using VotingApp.Repository.Models;
using VotingApp.Repository.Repositories;

namespace VotingApp.Application.Features.Commands.Candidate;

public class CreateCandidateCommand(string name) : IRequest<int>
{
    public string Name { get; init; } = name;
}
public class CreateCandidateCommandHandler(IRepository<CandidateEntity> repository, IMapper mapper) 
    : IRequestHandler<CreateCandidateCommand, int>
{
    public async Task<int> Handle(CreateCandidateCommand command, CancellationToken cancellationToken)
    {
        var candidate = mapper.Map<CandidateEntity>(command);

        var id = await repository.AddAsync(candidate);
        await repository.SaveChangesAsync();
        return id;
    }
}