using AutoMapper;
using FluentAssertions;
using Moq;
using VotingApp.Application.Features.Commands.Candidate;
using VotingApp.Repository.Models;
using VotingApp.Repository.Repositories;

namespace VotingApp.UnitTests.ApplicationTests;

public class CreateCandidateCommandHandlerTests
{
    private readonly Mock<IRepository<CandidateEntity>> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateCandidateCommandHandler _handler;

    public CreateCandidateCommandHandlerTests()
    {
        _repositoryMock = new Mock<IRepository<CandidateEntity>>();
        _mapperMock = new Mock<IMapper>();
        _handler = new CreateCandidateCommandHandler(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnCandidateId_WhenCandidateIsCreated()
    {
        // Arrange
        var command = new CreateCandidateCommand("John Doe");
        var candidateEntity = new CandidateEntity { Name = command.Name };
        var expectedId = 1;

        _mapperMock.Setup(m => m.Map<CandidateEntity>(command)).Returns(candidateEntity);
        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<CandidateEntity>())).ReturnsAsync(expectedId);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(expectedId);
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<CandidateEntity>()), Times.Once);
        _mapperMock.Verify(m => m.Map<CandidateEntity>(command), Times.Once);
    }
}