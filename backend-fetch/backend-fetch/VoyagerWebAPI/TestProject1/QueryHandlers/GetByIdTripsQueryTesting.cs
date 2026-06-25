using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using VoyagerWebAPI.Application.Queries.GetById;
using VoyagerWebAPI.Application.DTOs;
using VoyagerWebAPI.Application.Interfaces;
using VoyagerWebAPI.Domain;

using Assert = Xunit.Assert;
using TheoryAttribute = Xunit.TheoryAttribute;


namespace TestProject1;

public class GetTripByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_ExistingId()
    {
        // Arrange
        var mockRepository = new Mock<ITripRepository>();
        var query = new GetTripByIdQuery(1);

        var existingTrip = new Trip(
            "Euro Trip", 
            new Destination("Berlin", "Germania"), 
            DateTime.Now, 
            DateTime.Now.AddDays(7), 
            TripStatus.Planning, 
            2500m
        );

        mockRepository.Setup(repo => repo.GetByIdAsync(query.Id))
            .ReturnsAsync(existingTrip);

        var handler = new GetTripByIdQueryHandler(mockRepository.Object);

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(existingTrip.Name, result.Name);
        Assert.Equal(existingTrip.Destination.Name, result.Destination.Name);
        Assert.Equal(existingTrip.Budget, result.Budget);
        
        mockRepository.Verify(repo => repo.GetByIdAsync(query.Id), Times.Once);
    }

    [Fact]
    public async Task Handle_NonExistingId()
    {
        // Arrange
        var mockRepository = new Mock<ITripRepository>();
        var query = new GetTripByIdQuery(99); 

        mockRepository.Setup(repo => repo.GetByIdAsync(query.Id))
            .ReturnsAsync((Trip)null); 

        var handler = new GetTripByIdQueryHandler(mockRepository.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Null(result); 
        mockRepository.Verify(repo => repo.GetByIdAsync(query.Id), Times.Once);
    }
}