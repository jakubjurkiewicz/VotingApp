using AutoMapper;
using VotingApp.Application.Features.Commands.Candidate;
using VotingApp.Application.Features.Commands.Vote;
using VotingApp.Domain.Models;
using VotingApp.Repository.Models;

namespace VotingApp.Infrastructure.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap<CreateCandidateCommand, CandidateEntity>();
        CreateMap<CreateVoterCommand, VoterEntity>();

        CreateMap<CandidateEntity, Candidate>();
        CreateMap<VoterEntity, Voter>();
    }
}