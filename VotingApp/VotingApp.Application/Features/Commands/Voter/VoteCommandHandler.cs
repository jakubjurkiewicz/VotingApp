using MediatR;
using VotingApp.Repository.Models;
using VotingApp.Repository.Repositories;

namespace VotingApp.Application.Features.Commands.Voter
{
    public class VoteCommand : IRequest<Unit>
    {
        public int VoterId { get; set; }
        public int CandidateId { get; set; }

        public VoteCommand(int voterId, int candidateId)
        {
            VoterId = voterId;
            CandidateId = candidateId;
        }
    }

    public class VoteCommandHandler : IRequestHandler<VoteCommand, Unit>
    {
        private readonly IRepository<VoterEntity> _voterRepository;
        private readonly IRepository<CandidateEntity> _candidateRepository;

        public VoteCommandHandler(IRepository<VoterEntity> voterRepository, IRepository<CandidateEntity> candidateRepository)
        {
            _voterRepository = voterRepository;
            _candidateRepository = candidateRepository;
        }

        public async Task<Unit> Handle(VoteCommand command, CancellationToken cancellationToken)
        {
            var voter = await _voterRepository.GetByIdAsync(command.VoterId);
            if (voter == null)
            {
                throw new Exception("Voter either does not exist or has already voted.");
            }

            var candidate = await _candidateRepository.GetByIdAsync(command.CandidateId);
            if (candidate == null)
            {
                throw new Exception("Candidate does not exist.");
            }

            voter.HasVoted = true;
            _voterRepository.Update(voter);

            candidate.Votes += 1;
            _candidateRepository.Update(candidate);

            await _voterRepository.SaveChangesAsync();
            await _candidateRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
