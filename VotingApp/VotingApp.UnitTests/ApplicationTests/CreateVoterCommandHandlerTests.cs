using AutoMapper;
using FluentAssertions;
using Moq;
using VotingApp.Application.Features.Commands.Vote;
using VotingApp.Repository.Models;
using VotingApp.Repository.Repositories;

namespace VotingApp.UnitTests.ApplicationTests;

public class CreateVoterCommandHandlerTests
{
    private readonly Mock<IRepository<VoterEntity>> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateVoterCommandHandler _handler;

    public CreateVoterCommandHandlerTests()
    {
        _repositoryMock = new Mock<IRepository<VoterEntity>>();
        _mapperMock = new Mock<IMapper>();
        _handler = new CreateVoterCommandHandler(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnVoterId_WhenVoterIsCreated()
    {
        // Arrange
        var command = new CreateVoterCommand("Jane Doe");
        var voterEntity = new VoterEntity { Name = command.Name };
        var expectedId = 2;

        _mapperMock.Setup(m => m.Map<VoterEntity>(command)).Returns(voterEntity);
        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<VoterEntity>())).ReturnsAsync(expectedId);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(expectedId);
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<VoterEntity>()), Times.Once);
        _mapperMock.Verify(m => m.Map<VoterEntity>(command), Times.Once);
    }
}