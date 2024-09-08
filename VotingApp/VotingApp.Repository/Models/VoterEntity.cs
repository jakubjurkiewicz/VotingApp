namespace VotingApp.Repository.Models
{
    public class VoterEntity : Entity
    {
        public string Name { get; set; } = string.Empty;
        public bool HasVoted { get; set; }
    }
}
