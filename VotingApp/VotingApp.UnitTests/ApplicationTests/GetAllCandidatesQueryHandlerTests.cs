using AutoMapper;
using FluentAssertions;
using Moq;
using VotingApp.Application.Features.Queries.Candidate;
using VotingApp.Domain.Models;
using VotingApp.Repository.Models;
using VotingApp.Repository.Repositories;

namespace VotingApp.UnitTests.ApplicationTests;

public class GetAllCandidatesQueryHandlerTests
{
    private readonly Mock<IRepository<CandidateEntity>> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetAllCandidatesQueryHandler _handler;

    public GetAllCandidatesQueryHandlerTests()
    {
        _repositoryMock = new Mock<IRepository<CandidateEntity>>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetAllCandidatesQueryHandler(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnListOfCandidates_WhenCandidatesExist()
    {
        // Arrange
        var query = new GetAllCandidatesQuery();
        var candidateEntities = new List<CandidateEntity>
        {
            new() { Name = "John Doe", Votes = 3 }
        };
        var domainCandidates = new List<Candidate>
        {
            new("John Doe", 5)
        };

        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(candidateEntities);
        _mapperMock.Setup(m => m.Map<List<Candidate>>(candidateEntities)).Returns(domainCandidates);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(domainCandidates);
        _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
        _mapperMock.Verify(m => m.Map<List<Candidate>>(candidateEntities), Times.Once);
    }
}