using AutoMapper;
using MediatR;
using VotingApp.Repository.Models;
using VotingApp.Repository.Repositories;

namespace VotingApp.Application.Features.Queries.Voter;

public class GetAllVotersQuery : IRequest<List<Domain.Models.Voter>>;

public class GetAllVotersQueryHandler(IRepository<VoterEntity> repository, IMapper mapper)
    : IRequestHandler<GetAllVotersQuery, List<Domain.Models.Voter>>
{
    public async Task<List<Domain.Models.Voter>> Handle(GetAllVotersQuery request, CancellationToken cancellationToken)
    {
        var voters = (await repository.GetAllAsync()).ToList();

        return mapper.Map<List<Domain.Models.Voter>>(voters);
    }
}