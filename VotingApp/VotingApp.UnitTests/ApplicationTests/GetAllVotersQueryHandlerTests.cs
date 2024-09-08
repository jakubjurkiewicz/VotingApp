using AutoMapper;
using FluentAssertions;
using Moq;
using VotingApp.Application.Features.Queries.Voter;
using VotingApp.Domain.Models;
using VotingApp.Repository.Models;
using VotingApp.Repository.Repositories;

namespace VotingApp.UnitTests.ApplicationTests;

public class GetAllVotersQueryHandlerTests
{
    private readonly Mock<IRepository<VoterEntity>> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetAllVotersQueryHandler _handler;

    public GetAllVotersQueryHandlerTests()
    {
        _repositoryMock = new Mock<IRepository<VoterEntity>>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetAllVotersQueryHandler(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnListOfVoters_WhenVotersExist()
    {
        // Arrange
        var query = new GetAllVotersQuery();
        var voterEntities = new List<VoterEntity>
        {
            new() { Name = "Jane Doe", HasVoted = true }
        };
        var domainVoters = new List<Voter>
        {
            new("Jane Doe", true)
        };

        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(voterEntities);
        _mapperMock.Setup(m => m.Map<List<Voter>>(voterEntities)).Returns(domainVoters);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(domainVoters);
        _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
        _mapperMock.Verify(m => m.Map<List<Voter>>(voterEntities), Times.Once);
    }
}