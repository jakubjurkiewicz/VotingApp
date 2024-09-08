namespace VotingApp.Repository.Models;

public class CandidateEntity : Entity
{
    public int Votes { get; set; }
    public string Name { get; set; } = string.Empty;
}