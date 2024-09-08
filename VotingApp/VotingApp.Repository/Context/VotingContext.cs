using Microsoft.EntityFrameworkCore;
using VotingApp.Repository.Models;

namespace VotingApp.Repository.Context;

public class VotingContext : DbContext
{
    public DbSet<CandidateEntity> Candidates { get; set; }
    public DbSet<VoterEntity> Voters { get; set; }

    public VotingContext(DbContextOptions<VotingContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}