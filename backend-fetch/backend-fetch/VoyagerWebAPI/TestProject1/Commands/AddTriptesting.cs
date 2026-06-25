using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using VoyagerWebAPI.Application.Commands.AddTrip;
using VoyagerWebAPI.Application.DTOs;
using VoyagerWebAPI.Application.Interfaces;
using VoyagerWebAPI.Domain;

namespace TestProject1;

using Assert = Xunit.Assert;
using TheoryAttribute = Xunit.TheoryAttribute;


public class AddTripTesting
{
    [Fact]
    public async Task Handle_ValidCommand()
    {
        var mockRepository = new Mock<ITripRepository>();
        
        var command = new AddTripCommand(
            "Euro Trip",
            new Destination("Paris", "France"),
            new DateTime(2024, 6, 1),
            new DateTime(2024, 6, 10),
            TripStatus.Planning,
            2000.00m
        );

        var createdTrip = new Trip(
            command.Name, 
            command.Destination, 
            command.StartDate, 
            command.EndDate, 
            command.Status, 
            command.Budget
        );

        mockRepository.Setup(repo => repo.CreateAsync(It.IsAny<Trip>()))
            .ReturnsAsync(createdTrip);

        var handler = new AddTripCommandHandler(mockRepository.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(command.Name, result.Name);
        Assert.Equal(command.Destination.Name, result.Destination.Name);
        Assert.Equal(command.Budget, result.Budget);
        
        mockRepository.Verify(repo => repo.CreateAsync(It.IsAny<Trip>()), Times.Once);
    }
}