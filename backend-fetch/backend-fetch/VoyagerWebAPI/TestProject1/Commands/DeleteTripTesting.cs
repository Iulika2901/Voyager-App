using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using VoyagerWebAPI.Application.Commands.DeleteTrip;
using VoyagerWebAPI.Application.Interfaces;

using Assert = Xunit.Assert;
using TheoryAttribute = Xunit.TheoryAttribute;


namespace TestProject1;

public class DeleteTripCommandHandlerTests
{
    [Fact]
    public async Task Handle_ExistingTripId()
    {
        // Arrange
        var mockRepository = new Mock<ITripRepository>();
        var command = new DeleteTripCommand(1); 

        mockRepository.Setup(repo => repo.DeleteAsync(command.Id))
            .ReturnsAsync(true);

        var handler = new DeleteTripCommandHandler(mockRepository.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result);
        mockRepository.Verify(repo => repo.DeleteAsync(command.Id), Times.Once);
    }

    [Fact]
    public async Task Handle_NonExistingTripId()
    {
        // Arrange
        var mockRepository = new Mock<ITripRepository>();
        var command = new DeleteTripCommand(99); 

        mockRepository.Setup(repo => repo.DeleteAsync(command.Id))
            .ReturnsAsync(false);

        var handler = new DeleteTripCommandHandler(mockRepository.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result);
        mockRepository.Verify(repo => repo.DeleteAsync(command.Id), Times.Once);
    }
}