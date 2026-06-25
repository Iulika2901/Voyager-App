using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using VoyagerWebAPI.Application.Commands.CreateTrip;
using VoyagerWebAPI.Application.Interfaces;
using VoyagerWebAPI.Domain;

using Assert = Xunit.Assert;
using TheoryAttribute = Xunit.TheoryAttribute;


namespace TestProject1;

public class CreateTripCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidCommand()
    {
        // Arrange
        var mockRepository = new Mock<ITripRepository>();
        
        var command = new CreateTripCommand(
            "Vacanta Roma", 
            "Roma", 
            "Italia", 
            new DateTime(2024, 8, 1), 
            new DateTime(2024, 8, 10), 
            TripStatus.Planning, 
            1500m
        );

        var destination = new Destination(command.DestinationName, command.DestinationCountry);
        var createdTrip = new Trip(
            command.Name, 
            destination, 
            command.StartDate, 
            command.EndDate, 
            command.Status, 
            command.Budget
        );

        mockRepository.Setup(repo => repo.CreateAsync(It.IsAny<Trip>()))
            .ReturnsAsync(createdTrip);

        var handler = new CreateTripCommandHandler(mockRepository.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(command.Name, result.Name);
        Assert.Equal(command.DestinationName, result.Destination.Name);
        Assert.Equal(command.DestinationCountry, result.Destination.Country);
        Assert.Equal(command.Budget, result.Budget);
        
        mockRepository.Verify(repo => repo.CreateAsync(It.IsAny<Trip>()), Times.Once);
    }
}