namespace VotingApp.Domain.Models;

public record Voter(string Name, bool HasVoted)
{
    public int Id { get; init; }
}