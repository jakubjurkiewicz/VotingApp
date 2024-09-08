namespace VotingApp.Domain.Models;

public record Candidate(string Name, int Votes)
{
    public int Id { get; init; }
}