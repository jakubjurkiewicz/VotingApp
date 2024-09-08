using AutoMapper;
using MediatR;
using VotingApp.Repository.Models;
using VotingApp.Repository.Repositories;

namespace VotingApp.Application.Features.Queries.Candidate;

public class GetAllCandidatesQuery : IRequest<List<Domain.Models.Candidate>>;

public class GetAllCandidatesQueryHandler(IRepository<CandidateEntity> repository, IMapper mapper)
    : IRequestHandler<GetAllCandidatesQuery, List<Domain.Models.Candidate>>
{
    public async Task<List<Domain.Models.Candidate>> Handle(GetAllCandidatesQuery query, CancellationToken cancellationToken)
    {
        var candidates = (await repository.GetAllAsync()).ToList();

        return mapper.Map<List<Domain.Models.Candidate>>(candidates);
    }
}